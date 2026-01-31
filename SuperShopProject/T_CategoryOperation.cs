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
using System.Xml.Linq;

namespace SuperShopProject
{
    public partial class T_CategoryOperation : Form
    {
        int uid;
        public T_CategoryOperation(int uid)
        {
            this.uid = uid;
            InitializeComponent();
           
        }
        private void T_CategoryOperation_Load(object sender, EventArgs e)
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
                cmd.CommandText = "Select * from Catagory";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Close();

                DataTable dt = ds.Tables[0];
                dgvCategory.AutoGenerateColumns = false;
                dgvCategory.DataSource = dt;
                dgvCategory.Refresh();
                dgvCategory.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCid.Text = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCname.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                rtxtDesc.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }
        private void NewData()
        {
            txtCid.Text = "AutoGenerate";
            txtCname.Text = "";
            rtxtDesc.Text = "";

            dgvCategory.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtCid.Text;
            if (id == "AutoGenerate")
            {
                MessageBox.Show("Please Select a row");
                return;
            }

            var result = MessageBox.Show("Are u sure", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;


            try
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"delete from Catagory where [Catagory ID] ={id}; delete from product where [Catagory ID] ={id}";

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
            string id = txtCid.Text;
            string name = txtCname.Text;
            string desc = rtxtDesc.Text;
        
            string query = "";
            if (id == "AutoGenerate")
            {
                query = $"insert into Catagory values('{name}','{desc}')";
            }
            else
            {
                query = $"update Catagory set [Catagory Name]='{name}',Description='{desc}' where [Catagory ID]='{id}' ";
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
