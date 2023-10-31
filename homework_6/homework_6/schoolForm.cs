using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework_6
{
    public partial class schoolForm : UserControl
    {
        public schoolForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string schoolInfo = textBox1.Text;
            if (string.IsNullOrEmpty(schoolInfo))
            {
                MessageBox.Show("学校名不能为空");
                return;
            }
            School school = new School
            {
                Name = schoolInfo
            };
            DataManager.AddSchool(school);
            button3_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<School> schools = DataManager.GetSchools();
            listView1.Items.Clear();
            foreach (School school in schools)
            {
                ListViewItem item = new ListViewItem(school.Id.ToString());
                item.SubItems.Add(school.Name);
                listView1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int schoolId))
            {
                MessageBox.Show("学校ID必须是整数");
                return;
            }
            DataManager.DeleteSchool(schoolId);
            button3_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string schoolInfo = textBox1.Text;
            string[] parts = schoolInfo.Split(',');
            if (parts.Length != 2)
            {
                MessageBox.Show("请输入正确的学校信息格式：学校ID,学校名");
                return;
            }

            string schoolName = parts[1].Trim();
            if (string.IsNullOrEmpty(schoolName))
            {
                MessageBox.Show("学校名不能为空");
                return;
            }

            if (!int.TryParse(parts[0], out int schoolId))
            {
                MessageBox.Show("学校ID必须是整数");
                return;
            }
            School school = new School
            {
                Id = schoolId,
                Name = schoolName
            };
            DataManager.UpdateSchool(school);
            button3_Click(sender, e);
        }
    }
}
