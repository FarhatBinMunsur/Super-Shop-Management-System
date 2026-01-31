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
    public partial class M_OrderDetails : Form
    {
        int uid;
        public M_OrderDetails(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        string oid;
        public void LoadOID(string oid)
        {
            this.oid = oid;
        }

       
        private void M_OrderDetails_Load(object sender, EventArgs e)
        {
            lblOID.Text = oid;

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();
                int oid2 = Convert.ToInt32(oid);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [Order Details].[Product ID],[PRODUCT].[Product Name],[Quantity],[Order Details].[Unit Price],[Total Price] from [Order Details] inner join Product on [Order Details].[Product ID] = Product.[Product ID] where [Order Details].[Order ID]= {oid2}";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];

                dgvOD.DataSource = dt;
                dgvOD.Refresh();

                cmd.CommandText = $"select Name from UserInfo where UID={uid}"; 

                var value=cmd.ExecuteScalar().ToString();
                labelUserName.Text +=value+".";

                cmd.CommandText = $"select Address from UserInfo where UID={uid}";
                var value2 =cmd.ExecuteScalar().ToString();
                labelSA.Text +=value2;

                cmd.CommandText = $"select [Total Amount] from Orders where [Order ID]={oid}";
                var value3=cmd.ExecuteScalar().ToString();
                labelTA.Text += value3;

                cmd.CommandText = $"select status from orders where [order id]={oid}";
                var result4=cmd.ExecuteScalar().ToString(); 
                labelOS.Text += result4;

               


                con.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvOD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            M_OrderHistory oh = new M_OrderHistory(uid);
            oh.Show();
            this.Close();


        }

        private void M_OrderDetails_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"update Orders set Status='Cancelled' where [Order ID]={oid}";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
