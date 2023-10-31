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
    public partial class logForm : UserControl
    {
        public logForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Log> logs = DataManager.GetLogs();
            listView1.Items.Clear();
            foreach (Log log in logs)
            {
                ListViewItem item = new ListViewItem(log.Id.ToString());
                item.SubItems.Add(log.Action.ToString());
                item.SubItems.Add(log.Timestamp.ToString());
                listView1.Items.Add(item);
            }
        }
    }
}
