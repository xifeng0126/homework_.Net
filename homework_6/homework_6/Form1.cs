using System.Windows.Forms;

namespace homework_6
{
    public partial class Form1 : Form
    {
        public studentForm f1; //创建用户控件一变量
        public classForm f2; //创建用户控件二变量
        public schoolForm f3; //创建用户控件三变量
        public logForm f4; //创建用户控件四变量

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.Show();
            panel2.Controls.Clear();
            panel2.Controls.Add(f1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            f1 = new studentForm(); //实例化用户控件一
            f2 = new classForm(); //实例化用户控件二
            f3 = new schoolForm(); //实例化用户控件三
            f4 = new logForm(); //实例化用户控件四
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2.Show();
            panel2.Controls.Clear();
            panel2.Controls.Add(f2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f3.Show();
            panel2.Controls.Clear();
            panel2.Controls.Add(f3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            f4.Show();
            panel2.Controls.Clear();
            panel2.Controls.Add(f4);
        }
    }
}