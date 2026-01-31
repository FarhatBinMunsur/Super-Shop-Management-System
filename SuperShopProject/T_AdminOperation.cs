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
    public partial class T_AdminOperation : Form
    {
        int uid;
        public T_AdminOperation(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void T_AdminOperation_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            try
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = AppHelper.cs; 
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"Select * from UserInfo where [User Type]='Admin' ";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Close();

                DataTable dt = ds.Tables[0];
                dgvAdmin.AutoGenerateColumns = false;
                dgvAdmin.DataSource = dt;
                dgvAdmin.Refresh();
                dgvAdmin.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvAdmin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvAdmin.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dgvAdmin.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEmail.Text = dgvAdmin.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPhone.Text = dgvAdmin.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtPass.Text = dgvAdmin.Rows[e.RowIndex].Cells[4].Value.ToString();
                rtxtAddress.Text = dgvAdmin.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void NewData()
        {
            txtId.Text = "AutoGenerate";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPass.Text = "";
            rtxtAddress.Text = "";
            dgvAdmin.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();

        }
 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (id == "AutoGenerate")
            {
                MessageBox.Show("Please Select a row");
                return;
            }

            var result = MessageBox.Show("Are u sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;


            try
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"delete from UserInfo where UID ={id}";

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successful");
                this.LoadData();
                this.NewData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string password = txtPass.Text;
            string address = rtxtAddress.Text;

            string query = "";
            if (id == "AutoGenerate")
            {
                query = $"insert into UserInfo values('{name}','{email}','{phone}','{password}','Admin','{address}',Null,Null,Null)";
            }
            else
            {
                query = $"update UserInfo set Name='{name}',Email='{email}',Phone='{phone}',Password='{password}',Address='{address}' where UID={id}";
            }
            try
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Successful");
                this.LoadData();
                this.NewData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            T_AdminHomePage ahp = new T_AdminHomePage(uid);
            ahp.Show();
            this.Hide();
        }


    }


}


