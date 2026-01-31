using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperShopProject
{
    public partial class M_ManagerSalesReport : Form
    {
        int uid;

        public M_ManagerSalesReport(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void M_ManagerSalesReport_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime dtm = DateTime.Now;
                string day = dtm.Date.ToString();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection= con;

                cmd.CommandText = $"select Sum([Total Amount]) from orders where [Manager ID]={uid} and [Confirmation Date]='{day}'";
                var result1=cmd.ExecuteScalar().ToString();
                labelTSA.Text += result1;

                cmd.CommandText = $"select count([order id]) from orders where [Manager id]={uid} and [Confirmation Date]='{day}'";
                var result2=cmd.ExecuteScalar().ToString();
                labelTNO.Text += result2;

                cmd.CommandText =$"select sum([order details].quantity) from orders inner join [order Details] on orders.[Order ID]=[order details].[Order ID] where Orders.[Manager ID]={uid} and Orders.[Confirmation Date]='{day}'";
                var result3=cmd.ExecuteScalar().ToString();
                labelTIS.Text += result3;



                con.Close();


            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtpDay_ValueChanged(object sender, EventArgs e)
        {
            
            try
            {
                DateTime dt = dtpDay.Value;
                string day = dt.ToString();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"select Sum([Total Amount]) from orders where [Manager ID]={uid} and [Confirmation Date]='{day}'";
                var result1 = cmd.ExecuteScalar().ToString();
                labelTSA.Text = result1;

                cmd.CommandText = $"select count([order id]) from orders where [Manager id]={uid} and [Confirmation Date]='{day}'";
                var result2 = cmd.ExecuteScalar().ToString();
                labelTNO.Text = result2;

                cmd.CommandText = $"select sum([order details].quantity) from orders inner join [order Details] on orders.[Order ID]=[order details].[Order ID] where Orders.[Manager ID]={uid} and Orders.[Confirmation Date]='{day}'";
                var result3 = cmd.ExecuteScalar().ToString();
                labelTIS.Text = result3;



                con.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            S_ManagerHomePage mhp = new S_ManagerHomePage(uid);
            mhp.Show();
            this.Hide();
        }
    }
}
