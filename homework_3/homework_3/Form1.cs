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

                // ͳ��ԭʼ�����͵�����
                int originalLineCount = fileContent.Split('\n').Length;
                int originalWordCount = CountWords(fileContent);

                // ɾ��ע�ͺͿ���
                string cleanedContent = RemoveCommentsAndEmptyLines(fileContent);

                // ͳ�Ƹ�ʽ����������͵�����
                int formattedLineCount = cleanedContent.Split('\n').Length;
                int formattedWordCount = CountWords(cleanedContent);

                // ��ʾͳ�ƽ��
                LineCountLabel.Text = $"ԭʼ������{originalLineCount}";
                WordCountLabel.Text = $"ԭʼ��������{originalWordCount}";
                fLineCountLabel.Text = $"��ʽ����������{formattedLineCount}";
                fWordCountLabel.Text = $"��ʽ���󵥴�����{formattedWordCount}";

                // ͳ�Ƶ��ʳ��ִ�������ʾ���б���
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
            // �Ƴ����к�ע��
            text = Regex.Replace(text, @"//.*?$", string.Empty, RegexOptions.Multiline);
            // �Ƴ�����
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