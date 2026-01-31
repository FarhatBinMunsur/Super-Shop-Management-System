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
    public partial class S_ManagerHomePage : Form
    {
        int uid;
        public S_ManagerHomePage(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void btnViewProducts_Click(object sender, EventArgs e)
        {
            S_ProductOperation po = new S_ProductOperation(uid);  // Object create
            po.Show();  
            this.Hide();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            S_OrderOperation oo = new S_OrderOperation(uid);
            oo.Show();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            M_LoginForm lf =new M_LoginForm();
            lf.Show();
            this.Hide();
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            M_UpdatePassWord up = new M_UpdatePassWord(uid);
            up.Show();
            this.Hide();
        }

        private void S_ManagerHomePage_Load(object sender, EventArgs e)
        {

        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            M_Customer order = new M_Customer(uid);
            order.Show();
            this.Hide();
        }

        private void btnSR_Click(object sender, EventArgs e)
        {
            M_ManagerSalesReport msr = new M_ManagerSalesReport(uid);
            msr.Show();
            this.Hide();
        }
    }
}
