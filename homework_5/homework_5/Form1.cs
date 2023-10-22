using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace homework_5
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> phoneNumbersDictionary = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
        }

        private async Task ContinueCrawlAsync(string keyword, int phoneCount)
        {
            if (phoneCount >= 100)
            {
                return; // 如果已经收集了100个电话号码，停止继续搜索
            }

            List<string> searchEngines = new List<string>
    {
        "https://www.google.com/search?q=" + keyword,
        "https://www.bing.com/search?q=" + keyword,
        // 添加更多搜索引擎
    };

            var tasks = searchEngines.Select(CrawlSearchEngineAsync);

            await Task.WhenAll(tasks);

            if (phoneNumbersDictionary.Count < 100)
            {
                // 继续搜索，增加关键字的变化
                await ContinueCrawlAsync(keyword + "phonenumber", phoneCount);
            }
        }

        private async Task CrawlAsync(string keyword)
        {
            // 清空之前的结果
            listView1.Items.Clear();
            phoneNumbersDictionary.Clear(); // 清空电话号码字典

            await ContinueCrawlAsync(keyword, phoneNumbersDictionary.Count);

            // 显示结果
            int i = 0;
            foreach (var kvp in phoneNumbersDictionary.Take(100))
            {
                i++;
                var item = new ListViewItem(new[] {i.ToString(), kvp.Key, kvp.Value });
                listView1.Items.Add(item);
            }
        }

        private async Task CrawlSearchEngineAsync(string searchEngineUrl)
        {
            try
            {
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(searchEngineUrl);

                var phoneNumbers = ExtractPhoneNumbersFromPage(html);

                // 添加到电话号码字典
                foreach (var phoneNumber in phoneNumbers)
                {
                    if (!phoneNumbersDictionary.ContainsKey(phoneNumber))
                    {
                        phoneNumbersDictionary[phoneNumber] = searchEngineUrl;
                    }
                }

                // 如果电话号码数量已经达到100，可以返回，不再继续搜索
                if (phoneNumbersDictionary.Count >= 100)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                // 处理错误
            }
        }


        private List<string> ExtractPhoneNumbersFromPage(string html)
        {
            List<string> phoneNumbers = new List<string>();

            // 定义电话号码的正则表达式模式
            string pattern = @"(\d{3}[-\.\s]??\d{3}[-\.\s]??\d{4}|\(\d{3}\)\s*\d{3}[-\.\s]??\d{4}|\d{11})";

            MatchCollection matches = Regex.Matches(html, pattern);

            foreach (Match match in matches)
            {
                string phoneNumber = match.Value;

                // 去除重复的电话号码
                if (!phoneNumbers.Contains(phoneNumber))
                {
                    phoneNumbers.Add(phoneNumber);
                }
            }

            return phoneNumbers;
        }


        private async void SearchButton_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text;
            await CrawlAsync(keyword);
        }
    }
}

