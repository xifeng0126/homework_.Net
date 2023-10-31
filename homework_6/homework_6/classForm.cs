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
    public partial class classForm : UserControl
    {
        public classForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string classInfo = textBox1.Text;
            string[] parts = classInfo.Split(',');
            if (parts.Length != 2)
            {
                MessageBox.Show("请输入正确的班级信息格式：班级名,学校ID");
                return;
            }

            string className = parts[0].Trim();
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("班级名不能为空");
                return;
            }

            if (!int.TryParse(parts[1], out int schoolId))
            {
                MessageBox.Show("学校ID必须是整数");
                return;
            }

            // 创建班级对象
            Class newClass = new Class
            {
                Name = className,
                SchoolId = schoolId
            };

            // 插入学生信息到数据库
            DataManager.AddClass(newClass);
            button3_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Class> classes = DataManager.GetClasses();
            listView1.Items.Clear();
            foreach (Class clas in classes)
            {
                ListViewItem item = new ListViewItem(clas.Id.ToString());
                item.SubItems.Add(clas.Name);
                item.SubItems.Add(clas.SchoolId.ToString());
                listView1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int classId))
            {
                MessageBox.Show("请输入班级整数ID");
                return;
            }
            DataManager.DeleteClass(classId);
            button3_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string classInfo = textBox1.Text;
            string[] parts = classInfo.Split(',');
            if (parts.Length != 3)
            {
                MessageBox.Show("请输入正确的班级信息格式：班级ID,班级名,学校ID");
                return;
            }
            if (!int.TryParse(parts[0], out int classId))
            {
                MessageBox.Show("班级ID必须是整数");
                return;
            }
            string className = parts[1].Trim();
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("班级名不能为空");
                return;
            }

            if (!int.TryParse(parts[2], out int schoolId))
            {
                MessageBox.Show("学校ID必须是整数");
                return;
            }

            // 创建班级对象
            Class newClass = new Class
            {
                Id = classId,
                Name = className,
                SchoolId = schoolId
            };

            // 插入学生信息到数据库
            DataManager.UpdateClass(newClass);
            button3_Click(sender, e);
        }
    }
}
