using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperShopProject
{
    public partial class T_AdminHomePage : Form
    {
        int uid;
        public T_AdminHomePage(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void btnMSR_Click(object sender, EventArgs e)
        {
            M_MonthlySalesReport msr =new M_MonthlySalesReport(uid);
            msr.Show();
            this.Hide();
        }

        private void btnAO_Click(object sender, EventArgs e)
        {
            T_AdminOperation ao=new T_AdminOperation(uid);
            ao.Show();
            this.Hide();
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            T_ManagerOperation mo = new T_ManagerOperation(uid);
            mo.Show();
            this.Hide();
        }

        private void btnCo_Click(object sender, EventArgs e)
        {
            T_CategoryOperation Co = new T_CategoryOperation(uid);
            Co.Show();
            this.Hide();
        }

        private void btnLo_Click(object sender, EventArgs e)
        {
            M_LoginForm lf = new M_LoginForm();
            lf.Show();
            this.Hide();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            M_UpdatePassWord upw = new M_UpdatePassWord(uid);
            upw.Show();
            this.Hide();
        }

        private void btnSr_Click(object sender, EventArgs e)
        {
            M_SalesReport sr = new M_SalesReport(uid);
            sr.Show();
            this.Hide();
        }
    }
}
