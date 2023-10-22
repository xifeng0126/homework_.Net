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
                return; // ����Ѿ��ռ���100���绰���룬ֹͣ��������
            }

            List<string> searchEngines = new List<string>
    {
        "https://www.google.com/search?q=" + keyword,
        "https://www.bing.com/search?q=" + keyword,
        // ��Ӹ�����������
    };

            var tasks = searchEngines.Select(CrawlSearchEngineAsync);

            await Task.WhenAll(tasks);

            if (phoneNumbersDictionary.Count < 100)
            {
                // �������������ӹؼ��ֵı仯
                await ContinueCrawlAsync(keyword + "phonenumber", phoneCount);
            }
        }

        private async Task CrawlAsync(string keyword)
        {
            // ���֮ǰ�Ľ��
            listView1.Items.Clear();
            phoneNumbersDictionary.Clear(); // ��յ绰�����ֵ�

            await ContinueCrawlAsync(keyword, phoneNumbersDictionary.Count);

            // ��ʾ���
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

                // ��ӵ��绰�����ֵ�
                foreach (var phoneNumber in phoneNumbers)
                {
                    if (!phoneNumbersDictionary.ContainsKey(phoneNumber))
                    {
                        phoneNumbersDictionary[phoneNumber] = searchEngineUrl;
                    }
                }

                // ����绰���������Ѿ��ﵽ100�����Է��أ����ټ�������
                if (phoneNumbersDictionary.Count >= 100)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                // �������
            }
        }


        private List<string> ExtractPhoneNumbersFromPage(string html)
        {
            List<string> phoneNumbers = new List<string>();

            // ����绰�����������ʽģʽ
            string pattern = @"(\d{3}[-\.\s]??\d{3}[-\.\s]??\d{4}|\(\d{3}\)\s*\d{3}[-\.\s]??\d{4}|\d{11})";

            MatchCollection matches = Regex.Matches(html, pattern);

            foreach (Match match in matches)
            {
                string phoneNumber = match.Value;

                // ȥ���ظ��ĵ绰����
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

