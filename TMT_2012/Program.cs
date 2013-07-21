using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TMT_2012
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           // Application.Run(new Search_Customer());
            //Application.Run(new AdvancedSearch());
            Application.Run(new Login());
           // Application.Run(new SearchCustomerGrid());
         //   Application.Run(new Invoice());

            //Application.Run(new batary());
            //Application.Run(new frmSettings());
            //Application.Run(new invoice_repot());
        }
    }
}
