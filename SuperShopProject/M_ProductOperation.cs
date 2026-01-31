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
    public partial class M_ProductOperation : Form
    {
        int uid;
        public M_ProductOperation(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void M_ProductOperation_Load(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();

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
                cmd.CommandText = $"select Product.*, Catagory.[Catagory Name] from Product inner join Catagory on Catagory.[Catagory ID] = Product.[Catagory ID];select * from Catagory";


                DataSet ds = new DataSet(); 
                SqlDataAdapter adp = new SqlDataAdapter(cmd);  
                adp.Fill(ds); 

                con.Close();

                DataTable dt = ds.Tables[0];
                dgvProduct.AutoGenerateColumns = false;  
                dgvProduct.DataSource = dt;
                dgvProduct.Refresh(); 
                dgvProduct.ClearSelection();

             

                DataTable dt1 = ds.Tables[1];
                cmbCatagory.DataSource = dt1;
                cmbCatagory.ValueMember = "Catagory ID";
                cmbCatagory.DisplayMember = "Catagory Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewData()
        {
            txtID.Text = "Auto Generated";
            txtPn.Text= "";
            txtPrice.Text = "";
            txtStk.Text = "";
            cmbCatagory.Text = "";

            dgvProduct.ClearSelection();


        }

      

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            M_InventoryManager im=new M_InventoryManager(uid);
            im.Show();
            this.Hide();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                txtID.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtPn.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbCatagory.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtStk.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string pId = txtID.Text;
            string name = txtPn.Text;
            string stock = txtStk.Text;
            string catagory = cmbCatagory.SelectedValue.ToString();
            string price = txtPrice.Text;


            string query = ""; 

             
            if (pId == "Auto Generated") 
            {
                if (Name == "" || stock == "" || price == "" || catagory == "")
                {
                    MessageBox.Show("Please select a Row or insert the information correctly.", "Can't Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    query = $"insert into Product values('{name}', {stock},'{catagory}',{price})";

                }
            }
            else  
            {
                query = $"update Product set [Product Name]='{name}', Stock={stock},[Catagory ID]={catagory},[Unit Price]={price} where [Product ID] ={pId}";
            }

            try
            {

                SqlConnection con= new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open(); 

                SqlCommand cmd = new SqlCommand(); 
                cmd.Connection = con; 
                cmd.CommandText = query;


                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Added Successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.LoadData(); 
                this.NewData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string pId = txtID.Text;
            if (pId == "Auto Generated")
            {
                MessageBox.Show("Please select a Product to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result= MessageBox.Show("Are you Sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open(); 

                SqlCommand cmd = new SqlCommand(); 
                cmd.Connection = conn; 
                cmd.CommandText = $"delete from Product where [Product ID] ={pId}";


                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(" Deleted Successfully");
                this.LoadData(); 
                this.NewData(); 


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
