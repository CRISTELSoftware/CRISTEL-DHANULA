using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BackUps : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackUps"/> class.
        /// </summary>
        public BackUps()
        {
            InitializeComponent();
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form f = (Form)Application.OpenForms["DHNAULA"];
            f.Enabled = true;
        }

       


        public bool get_back_up()
        {

            saveFileDialog1.Title = ("Set Back Up Path");
            saveFileDialog1.InitialDirectory = "C:\\";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "All File|*.*";
            saveFileDialog1.ShowDialog();
            string path = saveFileDialog1.FileName;
            if (path != "")
            {
                bak_up.Backup("root", "123", "localhost", "accountingsystem", path);
                return true;
            }
            else
            {
                return false;
            }

        }




        /// <summary>
        /// Restors this instance.
        /// </summary>
        /// <returns></returns>
        public bool restor()
        {
            openFileDialog1.Title = ("Restor");
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "SQL File|*.sql";
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            if (path != "")
            {
                bak_up.restor("root", "123", "localhost", "accountingsystem", path);
                return true;
            }
            else
            {
                return false;
            }

        }

        private void radButton7_Click_1(object sender, EventArgs e)
        {
            bool status = get_back_up();
            if (status == true)
            {
                MessageBox.Show("Backuping Process Completed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            bool status = restor();
            if (status == true)
            {
                MessageBox.Show("Restoring Process Completed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
