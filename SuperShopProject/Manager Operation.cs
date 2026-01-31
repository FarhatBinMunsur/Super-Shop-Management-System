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
   
    public partial class T_ManagerOperation : Form
    {
        int uid;
        public T_ManagerOperation(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void Manager_Operation_Load(object sender, EventArgs e)
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
                cmd.CommandText = $"Select * from UserInfo where [User Type]='Manager' ";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                con.Close();

                DataTable dt = ds.Tables[0];
                dgvManager.AutoGenerateColumns = false;
                dgvManager.DataSource = dt;
                dgvManager.Refresh();
                dgvManager.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvManager_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvManager.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dgvManager.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEmail.Text = dgvManager.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPhone.Text = dgvManager.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtPass.Text = dgvManager.Rows[e.RowIndex].Cells[4].Value.ToString();
                rtxtAddress.Text = dgvManager.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtSalary.Text = dgvManager.Rows[e.RowIndex].Cells[6].Value.ToString();
                dtpJd.Value = Convert.ToDateTime(dgvManager.CurrentRow.Cells[7].Value);

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
            dgvManager.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData ();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            if (id == "AutoGenerate")
            {
                MessageBox.Show("Please Select a row");
                return;
            }

            var result = MessageBox.Show("Are You sure", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
            DateTime dt = dtpJd.Value;
            string day = dt.ToString();
            int salary =Int32.Parse( txtSalary.Text);

            string query = "";
            if (id == "AutoGenerate")
            {
                query = $"insert into UserInfo values('{name}','{email}','{phone}','{password}','Manager','{address}','{day}','{salary}',Null)";
            }
            else
            {
                query = $"update UserInfo set Name='{name}',Email='{email}',Phone='{phone}',Password='{password}',Address='{address}',Salary='{salary}',[Joining Date]='{day}' where UID={id}";
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
