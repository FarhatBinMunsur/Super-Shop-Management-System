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
    public partial class M_SalesReport : Form
    {
        int uid;
        public M_SalesReport(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void M_SalesReport_Load(object sender, EventArgs e)
        {
            DateTime dt = dtpDay.Value;
            string day = dt.Date.ToString();

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"Select sum([Total amount]) from orders where [Order Date] = '{day}'";
                var resultDay = cmd.ExecuteScalar().ToString();
                //MessageBox.Show();
                labelTSA.Text += resultDay;

                cmd.CommandText = $"Select Count([Order ID]) from orders where  [Order Date]='{day}'";
                var resultTO = cmd.ExecuteScalar().ToString();
                labelTNO.Text += resultTO;

                cmd.CommandText = $"select count([order id]) from orders where [confirmation date]='{day}'";
                var resultTCO=cmd.ExecuteScalar().ToString();
                labelTCO.Text += resultTCO;

                cmd.CommandText = $"select SUM([Order Details].Quantity) as TotalQuantity from Orders inner join [Order Details] on [Order Details].[Order ID]= Orders.[Order ID] where [Orders].[Order Date]='{day}'";
                var resultTQ = cmd.ExecuteScalar().ToString();
                labelTIS.Text += resultTQ;

                //cmd.CommandText = $"select Count([Order ID]) from orders where (status='Cancelled' and [Order Date] = '{day}')";
                //var reultTCO=cmd.ExecuteScalar().ToString();   
                //labelTco.Text += reultTCO;

                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dtpDay_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dtpDay.Value;
            string day = dt.ToString("yyyy-MM-dd");

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"Select sum([Total amount]) as Total from orders where [Order Date] = '{day}'";
                var resultTSA = cmd.ExecuteScalar().ToString();
                //MessageBox.Show();
                labelTSA.Text = resultTSA;

                cmd.CommandText = $"Select Count([Order ID]) as Total from orders where  [Order Date]='{day}'";
                var resultTO = cmd.ExecuteScalar().ToString();
                labelTNO.Text = resultTO;

                cmd.CommandText = $"select SUM([Order Details].Quantity) as TotalQuantity from Orders inner join [Order Details] on [Order Details].[Order ID]= Orders.[Order ID] where [Orders].[Order Date]='{day}'";
                var resultTQ = cmd.ExecuteScalar().ToString();
                labelTIS.Text = resultTQ;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T_AdminHomePage ahp = new T_AdminHomePage(uid);
            ahp.Show();
            this.Hide();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
