using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework_1b
{
    public partial class Form1 : Form
    {

        private Random random = new Random();
        private int num1, num2, correctAnswer;
        private int score = 0;
        private int quesnum = 5;
        private int timeleft = 10;//每题最多十秒
        public Form1()
        {
            InitializeComponent();
            startButton.Click += new EventHandler(OnStart);
            timetext.Text = $"倒计时: {countdown} 秒";
        }
        private void GenerateQuestion()
        {
            num1 = random.Next(1, 11); // 生成1到10的随机数
            num2 = random.Next(1, 11);

            // 随机选择是加法还是减法题目
            if (random.Next(0, 2) == 0)
            {
                correctAnswer = num1 + num2;
                labelQuestion.Text = $"{num1} + {num2} = ?";
            }
            else
            {
                correctAnswer = num1 - num2;
                labelQuestion.Text = $"{num1} - {num2} = ?";
            }
        }
        public void OnStart(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello World!");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            countdown = 30;
            timer.Start();
            GenerateQuestion();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"每答对一题加一分，10秒不作答视作超时自动跳到下一题，答题时间共30秒");
        }

        private void textBoxAnswer_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {
                countdown--;
                timetext.Text = $"倒计时: {countdown} 秒";
                scoreText.Text = $"当前得分: {score} 分";
            }
            else
            {
                timer.Stop(); // 当倒计时达到 0 秒时停止计时器
                MessageBox.Show($"倒计时结束！你共获得{score}分");
                return;
            }
            if(timeleft>0)
            {
                timeleft--;
            }
            else
            {
                timeleft = 10;
                MessageBox.Show("回答超时,自动下一题");
                GenerateQuestion();
                textBoxAnswer.Clear();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            timeleft = 10;
            int userAnswer;
            if (int.TryParse(textBoxAnswer.Text, out userAnswer))
            {
                if (userAnswer == correctAnswer)
                {
                    MessageBox.Show("回答正确, 得一分！");
                    score++;
                }
                else
                {
                    MessageBox.Show("回答错误。");
                }
                GenerateQuestion();
                textBoxAnswer.Clear();
            }
            else
            {
                MessageBox.Show("请输入一个整数。");
            }
        }
    }

   
}
