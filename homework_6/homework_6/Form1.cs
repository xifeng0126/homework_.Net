using System.Windows.Forms;

namespace homework_6
{
    public partial class Form1 : Form
    {
        public studentForm f1; //�����û��ؼ�һ����
        public classForm f2; //�����û��ؼ�������
        public schoolForm f3; //�����û��ؼ�������
        public logForm f4; //�����û��ؼ��ı���

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
            f1 = new studentForm(); //ʵ�����û��ؼ�һ
            f2 = new classForm(); //ʵ�����û��ؼ���
            f3 = new schoolForm(); //ʵ�����û��ؼ���
            f4 = new logForm(); //ʵ�����û��ؼ���
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