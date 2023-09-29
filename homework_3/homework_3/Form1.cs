using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace homework_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C# Source Files (*.cs)|*.cs";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);

                // 统计原始行数和单词数
                int originalLineCount = fileContent.Split('\n').Length;
                int originalWordCount = CountWords(fileContent);

                // 删除注释和空行
                string cleanedContent = RemoveCommentsAndEmptyLines(fileContent);

                // 统计格式化后的行数和单词数
                int formattedLineCount = cleanedContent.Split('\n').Length;
                int formattedWordCount = CountWords(cleanedContent);

                // 显示统计结果
                LineCountLabel.Text = $"原始行数：{originalLineCount}";
                WordCountLabel.Text = $"原始单词数：{originalWordCount}";
                fLineCountLabel.Text = $"格式化后行数：{formattedLineCount}";
                fWordCountLabel.Text = $"格式化后单词数：{formattedWordCount}";

                // 统计单词出现次数并显示在列表中
                string[] wordList = GetWordList(cleanedContent);
                DisplayWordFrequency(wordList);
            }
        }

        private int CountWords(string text)
        {
            string[] words = text.Split(' ', '\t', '\n', '\r')
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .ToArray();
            return words.Length;
        }

        private string RemoveCommentsAndEmptyLines(string text)
        {
            // 移除单行和注释
            text = Regex.Replace(text, @"//.*?$", string.Empty, RegexOptions.Multiline);
            // 移除空行
            text = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            return text;
        }

        private string[] GetWordList(string text)
        {
            string[] words = text.Split(' ', '\t', '\n', '\r', '.', ',', ';', ':', '(', ')', '{', '}', '[', ']')
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Select(word => word.Trim())
                .ToArray();
            return words;
        }

        private void DisplayWordFrequency(string[] wordList)
        {
            Dictionary<string, int> wordFrequency = wordList
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());

            wordListView.Items.Clear();
            foreach (var kvp in wordFrequency)
            {
                ListViewItem item = new ListViewItem(new string[] { kvp.Key, kvp.Value.ToString() });
                wordListView.Items.Add(item);
            }
        }
    }
}