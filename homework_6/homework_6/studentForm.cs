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
    public partial class studentForm : UserControl
    {
        public studentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取TextBox中的学生信息
            string studentInfo = textBox1.Text;

            // 将学生信息分割成姓名和班级ID
            string[] parts = studentInfo.Split(',');
            if (parts.Length != 2)
            {
                MessageBox.Show("请输入正确的学生信息格式：姓名,班级ID");
                return;
            }

            string studentName = parts[0].Trim();
            if (string.IsNullOrEmpty(studentName))
            {
                MessageBox.Show("学生姓名不能为空");
                return;
            }

            if (!int.TryParse(parts[1], out int classId))
            {
                MessageBox.Show("班级ID必须是整数");
                return;
            }

            // 创建学生对象
            Student newStudent = new Student
            {
                Name = studentName,
                ClassId = classId
            };

            // 插入学生信息到数据库
            DataManager.AddStudent(newStudent);
            button3_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Student> students = DataManager.GetStudents();
            listView1.Items.Clear();
            foreach (Student student in students)
            {
                ListViewItem item = new ListViewItem(student.Id.ToString());
                item.SubItems.Add(student.Name);
                item.SubItems.Add(student.ClassId.ToString());
                listView1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int studentId))
            {
                MessageBox.Show("请输入学生整数ID");
                return;
            }
            DataManager.DeleteStudent(studentId);
            button3_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 获取TextBox中的学生信息
            string studentInfo = textBox1.Text;

            // 将学生信息分割成ID,姓名和班级ID
            string[] parts = studentInfo.Split(',');
            if (parts.Length != 3)
            {
                MessageBox.Show("请输入正确的学生信息格式：ID,姓名,班级ID");
                return;
            }
            if (!int.TryParse(parts[0], out int studentId))
            {
                MessageBox.Show("学生ID必须是整数");
                return;
            }

            string studentName = parts[1].Trim();
            if (string.IsNullOrEmpty(studentName))
            {
                MessageBox.Show("学生姓名不能为空");
                return;
            }

            if (!int.TryParse(parts[2], out int classId))
            {
                MessageBox.Show("班级ID必须是整数");
                return;
            }

            // 创建学生对象
            Student newStudent = new Student
            {
                Id = studentId,
                Name = studentName,
                ClassId = classId
            };

            // 插入学生信息到数据库
            DataManager.UpdateStudent(newStudent);
            button3_Click(sender, e);
        }
    }
}
