using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace homework_4
{
    public partial class Form1 : Form
    {
        int x = 0;
        TreeNode selectedNode;
        bool invisible = false; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // TreeNode rootNode = new TreeNode("C:\\");
            // rootNode.Tag = "C:\\";
            // treeView.Nodes.Add(rootNode);
            //// LoadDirectories(rootNode);
            this.listView1.View = View.Details;
            this.treeView.ImageList = this.imageList1;
            string[] drives = Environment.GetLogicalDrives();

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed)
                {
                    //加载当前目录
                    this.treeView.Nodes.Add(drive.Name, drive.Name);
                    foreach (var dir in Directory.GetDirectories(drive.Name))
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden ||invisible) 
                        {
                            TreeNode node = new TreeNode() { Tag = dir, Text = Path.GetFileName(dir) };
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                            node.Nodes.Add("");
                            //node = ListDirectories(node);
                            this.treeView.Nodes[drive.Name].Nodes.Add(node);
                        }
                    }

                    //加载当前目录文件
                    string[] files = Directory.GetFiles(drive.Name);
                    foreach (var file in files)
                    {
                        TreeNode node = new TreeNode() { Tag = file, Text = Path.GetFileName(file) };
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                        this.treeView.Nodes[drive.Name].Nodes.Add(node);
                    }
                }
            }
        }

        // 递归加载子文件夹
        private void LoadDirectories(TreeNode parentNode)
        {
            string path = parentNode.Tag as string;
            if (string.IsNullOrEmpty(path))
                return;

            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    TreeNode node = new TreeNode(Path.GetFileName(directory));
                    node.Tag = directory;
                    parentNode.Nodes.Add(node);
                    LoadDirectories(node);
                }
            }
            catch (Exception ex)
            {
                // 处理异常
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                e.Node.Expand();
                this.listView1.Items.Clear();
                if (Directory.Exists(e.Node.Tag != null ? e.Node.Tag.ToString() : ""))
                {
                    e.Node.Nodes.Clear();
                    foreach (var dir in Directory.GetDirectories(e.Node.Tag.ToString()))
                    {
                        TreeNode t = new TreeNode()
                        {
                            Tag = dir,
                            Text = Path.GetFileName(dir),
                        };
                        t.Nodes.Add("");
                        e.Node.Nodes.Add(t);
                    }

                    foreach (var file in Directory.GetFiles(e.Node.Tag.ToString(), "*.*", SearchOption.TopDirectoryOnly))
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        ListViewItem item = new ListViewItem(new string[] {
                            fileInfo.Name,
                            (fileInfo.Length / 1024+1) + "kb",
                            fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                        })
                        { Tag = fileInfo.FullName };
                        this.listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 获取选择的项
            ListViewItem selectedItem = listView1.SelectedItems[0];

            // 判断选择的是文件还是文件夹
            if (Directory.Exists(selectedItem.Tag.ToString()))
            {
                // 如果是文件夹，打开该文件夹并显示其中的子文件夹和文件
                DirectoryInfo directoryInfo = new DirectoryInfo(selectedItem.Tag.ToString());
                TreeNode node = new TreeNode(directoryInfo.Name);
                node.Tag = directoryInfo.FullName;
                selectedNode.Nodes.Add(node);

                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name, 0);
                    item.Tag = dir.FullName;
                    listView1.Items.Add(item);
                }

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name, 1);
                    item.Tag = file.FullName;
                    listView1.Items.Add(item);
                }
            }
            else
            {
                // 如果是文件，判断文件类型并打开相应程序
                string extension = Path.GetExtension(selectedItem.Tag.ToString()).ToLower();
                switch (extension)
                {
                    case ".txt":
                        System.Diagnostics.Process.Start("notepad.exe", selectedItem.Tag.ToString());
                        break;
                    case ".exe":
                        System.Diagnostics.Process.Start(selectedItem.Tag.ToString());
                        break;
                    default:
                        MessageBox.Show("不支持打开该类型的文件。");
                        break;
                }
            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                this.listView1.Items.Clear();
                if (Directory.Exists(e.Node.Tag != null ? e.Node.Tag.ToString() : ""))
                {
                    e.Node.Nodes.Clear();
                    foreach (var dir in Directory.GetDirectories(e.Node.Tag.ToString()))
                    {
                        TreeNode t = new TreeNode()
                        {
                            Tag = dir,
                            Text = Path.GetFileName(dir),
                        };
                        t.Nodes.Add("");
                        e.Node.Nodes.Add(t);
                    }

                    foreach (var file in Directory.GetFiles(e.Node.Tag.ToString(), "*.*", SearchOption.TopDirectoryOnly))
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        ListViewItem item = new ListViewItem(new string[] {
                            fileInfo.Name,
                            (fileInfo.Length / 1024+1) + "kb",
                            fileInfo.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")
                        })
                        { Tag = fileInfo.FullName };
                        this.listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public class ListViewItemComparer : IComparer
        {
            private int _x;
            private string _type;

            public ListViewItemComparer(int x, string type)
            {
                this._x = x;
                this._type = type;
            }

            public int Compare(object x, object y)
            {
                ListViewItem lv1 = (ListViewItem)x;
                ListViewItem lv2 = (ListViewItem)y;

                FileInfo file1 = new FileInfo(lv1.Tag.ToString());
                FileInfo file2 = new FileInfo(lv2.Tag.ToString());

                if (_type.Equals("文件名"))
                {
                    if (_x % 2 == 0)
                    {
                        return file1.Name.CompareTo(file2.Name);
                    }
                    else
                    {
                        return file2.Name.CompareTo(file1.Name);
                    }
                }
                if (_type.Equals("文件大小"))
                {
                    if (_x % 2 == 0)
                    {
                        return file1.Length.CompareTo(file2.Length);
                    }
                    else
                    {
                        return file2.Length.CompareTo(file1.Length);
                    }
                }
                if (_type.Equals("创建时间"))
                {
                    if (_x % 2 == 0)
                    {
                        return file1.CreationTime.CompareTo(file2.CreationTime);
                    }
                    else
                    {
                        return file2.CreationTime.CompareTo(file1.CreationTime);
                    }
                }
                return 0;
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.listView1.ListViewItemSorter = new ListViewItemComparer(x++, this.listView1.Columns[e.Column].Text);
            this.listView1.Sort();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            listView1.Items.Clear(); // 清除原有的项
            Form1_Load(sender,e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            invisible = !invisible;
            toolStripButton3_Click(sender,null);
        }

        private void 退出呈现出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //退出程序
            Application.Exit();
        }

        private void 使用帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1、左侧TreeView显示当前系统的盘符和文件夹的路径（能够一级一级的点击下去，这里没有一次性递归所有文件夹及其子文件夹，而是实现了按需加载）和右侧ListView（显示左侧选中文件夹下的文件），中间用了splitContainer实现两者的分割（能够左右拖动）。\n2、右侧ListView显示了文件的三项内容（包括文件名、文件大小、创建时间等）\n3、点击右侧ListView的Head部分，能够实现自定义排序（从大到小和从小到大排序，交替进行），双击可打开特定类型文件。\n4、菜单、工具栏实现“新建文件”、“删除文件”、“查看隐藏文件”、“刷新”、“打开文件夹”、“搜索（TODO）”、“退出程序”等功能", "关于软件", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("您确定要删除当前文件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                string filePath = this.listView1.SelectedItems[0].Tag.ToString();
                string fileName = this.listView1.SelectedItems[0].Text.ToString();
                File.Delete(filePath);
                this.listView1.Items.Remove(this.listView1.Items.OfType<ListViewItem>().FirstOrDefault(x => x.Text == fileName));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string filePath = null;
            string treeFilePath = this.treeView.SelectedNode.Tag.ToString()
                + "\\" + this.treeView.SelectedNode.Text.ToString();
            if (this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0)
            {
                filePath = this.listView1.SelectedItems[0].Tag.ToString();
            }
            Process.Start(Path.GetDirectoryName(filePath ?? treeFilePath));
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultFileName = "NewFile.txt";

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string directoryPath = dialog.SelectedPath;
                    InputDialog inputDialog = new InputDialog("请输入文件名：", defaultFileName);

                    if (inputDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = inputDialog.InputText;
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            fileName = Path.Combine(directoryPath, fileName);
                            if (!File.Exists(fileName))
                            {
                                File.Create(fileName).Close();
                                toolStripButton3_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("文件已存在，请输入不同的文件名。", "错误提示");
                            }
                        }
                    }
                }
            }
        }
    }
}
