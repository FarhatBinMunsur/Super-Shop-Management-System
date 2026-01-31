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
    public partial class M_InventoryManager : Form
    {
        int uid;
        public M_InventoryManager(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M_ProductOperation po = new M_ProductOperation(uid);
            po.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M_UpdatePassWord up=new M_UpdatePassWord(uid);
            up.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M_LoginForm lf = new M_LoginForm();
            lf.Show();
            this.Hide();
        }
    }
}
