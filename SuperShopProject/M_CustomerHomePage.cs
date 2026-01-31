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
    public partial class M_CustomerHomePage : Form
    {
        int uid;
        public M_CustomerHomePage(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_Customer ord=new M_Customer(uid);
            ord.Show();
            this.Hide();
        }

        private void btnOH_Click(object sender, EventArgs e)
        {
            M_OrderHistory oh=new M_OrderHistory(uid);
            oh.Show();
            this.Hide();
        }

        private void M_CustomerHomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            M_LoginForm lf = new M_LoginForm();
            lf.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_UpdatePassWord up = new M_UpdatePassWord(uid);
            up.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            M_LoginForm lf = new M_LoginForm();
            lf.Show();
            this.Hide();
        }
    }
}
