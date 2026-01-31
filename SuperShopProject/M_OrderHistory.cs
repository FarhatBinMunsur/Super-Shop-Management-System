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
    public partial class M_OrderHistory : Form
    {
        int uid;
        public M_OrderHistory(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        
        private void M_OrderHistory_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [Order ID],[Order Date],[Total Amount],Status from Orders where [Customer ID]={uid}";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];

                dgvOH.DataSource = dt;
                dgvOH.Refresh();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var row = dgvOH.SelectedRows[0];

            string oid = row.Cells["OID"].Value.ToString();
            M_OrderDetails od=new M_OrderDetails(uid);
            od.LoadOID(oid);
            od.Show();
            this.Hide();

        }

        private void M_OrderHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [user type] from userinfo where [UID]={uid}";

                var value = cmd.ExecuteScalar().ToString();
                if (value == "Customer")
                {
                    M_CustomerHomePage chp = new M_CustomerHomePage(uid);
                    chp.Show();
                    this.Hide();
                }
                else
                {
                    S_ManagerHomePage mhp = new S_ManagerHomePage(uid);
                    mhp.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [user type] from userinfo where [UID]={uid}";

                var value = cmd.ExecuteScalar().ToString();
                if (value == "Customer")
                {
                    M_CustomerHomePage chp = new M_CustomerHomePage(uid);
                    chp.Show();
                    this.Hide();
                }
                else
                {
                    S_ManagerHomePage mhp = new S_ManagerHomePage(uid);
                    mhp.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
