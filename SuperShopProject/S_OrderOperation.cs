using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperShopProject
{
    public partial class S_OrderOperation : Form
    {
        int uid;
        public S_OrderOperation(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

      



        private void S_OrderOperation_Load(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void LoadData()
        {
            // auto load DataGridView when a user enters
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open(); //connect button press kora

                SqlCommand cmd = new SqlCommand(); // Panel create kora jekhane query likha hoi
                cmd.Connection = conn; // je database e query likhbe sheta bolse (conn)
                cmd.CommandText = $"select * from Orders";

                //eta jodi insert/delete query hoto tahole data return kora lagto na --> tokhon cmd.ExecuteNonQuery(); conn.Close(); ei 2 ta function call korle hoto

                // but eta jehtu select query tai data return korbe so variable newa lagbe & result section return korar jnno dataset nilam
                DataSet ds = new DataSet(); // c# er party ds variable & Database er party cmd
                SqlDataAdapter adp = new SqlDataAdapter(cmd); //cmd ke pass 
                adp.Fill(ds);  // Database theke result dataset e chole asche

                conn.Close();

                DataTable dt = ds.Tables[0]; // Data Table fetch kora & table 1 ta tai [0] dewa
                dgvOrders.AutoGenerateColumns = false;  // Responsive er jnno kora, akhn ar auto design korbe na gridview te, design e giye amader design korte hobe cols & Rows
                dgvOrders.DataSource = dt;
                dgvOrders.Refresh(); // load howar shathe shate newly added jkono data load hobe
                dgvOrders.ClearSelection(); // pre selected kichu thakbe na (Visual problem solved)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // right side e data newa gridview theke 
            if (e.RowIndex >= 0)
            {
                txtOrderID.Text = dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                cmbStatus.Text = dgvOrders.Rows[e.RowIndex].Cells[4].Value.ToString();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime dt=DateTime.Now;
            string day = dt.ToString();

            string oId = txtOrderID.Text;

            if (oId == "Auto Generated")
            {
                MessageBox.Show("Select a row first","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (cmbPaymentType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Payment type.");
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            string paymentType = cmbPaymentType.Text;
            string status = cmbStatus.Text;

            string payment_status = "";
            if (status == "Cash on delivery") payment_status = "Unpaid";
            else payment_status = "Paid";

                string query = $"UPDATE [Payment Details] SET [Payment Type] = '{paymentType}',[Payment Status] = '{payment_status}' WHERE [ref ID] = {Convert.ToInt32(oId)};update Orders set status = '{status}',[Manager ID]={uid},[confirmation date]='{day}' where [order ID] = {Convert.ToInt32(oId)} and status = 'Pending' ";

            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                

                MessageBox.Show("Update Successful");
                this.LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            S_ManagerHomePage mp = new S_ManagerHomePage(uid);
            mp.Show();
            this.Hide();
        }

        private void S_OrderOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            S_ManagerHomePage mh = new S_ManagerHomePage(uid);
            mh.Show();
        }
    }
}
