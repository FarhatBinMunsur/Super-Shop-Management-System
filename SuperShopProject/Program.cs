using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperShopProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new M_LoginForm());

            //Application.Run(new M_ProductOperation(1));
            //Application.Run(new M_Customer());
            //Application.Run(new M_OrderHistory());
            //Application.Run(new M_OrderDetails());
            //Application.Run(new M_SignUpForm());
            //Application.Run(new M_MonthlySalesReport());


            //Application.Run(new M_SalesReport());


        }
    }
}
