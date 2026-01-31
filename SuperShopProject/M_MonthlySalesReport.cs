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
    public partial class M_MonthlySalesReport : Form
    {
        int uid;
        public M_MonthlySalesReport(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void M_MonthlySalesReport_Load(object sender, EventArgs e)
        {

            DateTime dt=dtp1.Value;
            string day1 = dt.Date.ToString();

            string day2 = dtp2.Value.Date.ToString();

            //MessageBox.Show(day1 +"\n"+day2);

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"select Sum([Total Amount]) from Orders where [Order Date] BETWEEN '{day1}' AND '{day2}'";
                var resultTSA = cmd.ExecuteScalar().ToString();
                labelTSA.Text = resultTSA;

                cmd.CommandText = $"select count([Order ID])from orders where [order date] between '{day1}' and '{day2}'";
                var resultTNO = cmd.ExecuteScalar().ToString();
                labelTNO.Text = resultTNO;

                cmd.CommandText = $"select sum([Order Details].Quantity) from orders inner join [Order Details] on orders.[Order ID]=[Order Details].[Order ID]  where [Order Date] between '{day1}' and '{day2}'";
                var resultTIS = cmd.ExecuteScalar().ToString();
                labelTIS.Text = resultTIS;

                cmd.CommandText =$"select count([order id]) from orders where (status='Cancelled' and [Order date] between '{day1}' and '{day2}')";
                var resultTco = cmd.ExecuteScalar().ToString();
                labelTCO.Text=resultTco;


                con.Close();
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {

            DateTime dt = dtp1.Value;
            string day1 = dt.ToString();

            string day2 = dtp2.Value.ToString();

            //MessageBox.Show(day1 +"\n"+day2);

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"select Sum([Total Amount]) from Orders where [Order Date] BETWEEN '{day1}' AND '{day2}'";
                var resultTSA = cmd.ExecuteScalar().ToString();
                labelTSA.Text = resultTSA;

                cmd.CommandText = $"select count([Order ID])from orders where [order date] between '{day1}' and '{day2}'";
                var resultTNO = cmd.ExecuteScalar().ToString();
                labelTNO.Text = resultTNO;

                cmd.CommandText = $"select sum([Order Details].Quantity) from orders inner join [Order Details] on orders.[Order ID]=[Order Details].[Order ID]  where [Order Date] between '{day1}' and '{day2}'";
                var resultTIS = cmd.ExecuteScalar().ToString();
                labelTIS.Text = resultTIS;

                cmd.CommandText = $"select count([order id]) from orders where (status='Cancelled' and [Order date] between '{day1}' and '{day2}')";
                var resultTco = cmd.ExecuteScalar().ToString();
                labelTCO.Text = resultTco;

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T_AdminHomePage ahp=new T_AdminHomePage(uid);
            ahp.Show();
            this.Hide();
        }
    }
}
