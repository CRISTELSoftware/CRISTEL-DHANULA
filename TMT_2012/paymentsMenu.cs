using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class paymentsMenu : Telerik.WinControls.UI.RadForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="paymentsMenu"/> class.
        /// </summary>
        public paymentsMenu()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            AddPayments adp = new AddPayments();
            adp.Show();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            managePayments mgp = new managePayments();
            mgp.Show();
        }
    }
}
