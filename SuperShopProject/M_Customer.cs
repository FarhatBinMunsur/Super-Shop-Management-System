using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SuperShopProject
{
    public partial class M_Customer : Form
    {
        int uid;
        public M_Customer(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select [Product ID],[Product Name],Catagory.[Catagory Name],[Unit Price] from Product inner join Catagory on Catagory.[Catagory ID]=Product.[Catagory ID]";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];

                dgvProduct.DataSource = dt;
                dgvProduct.Refresh();
                dgvProduct.ClearSelection();

                cmd.CommandText = $"select [Product Name] ,[Product ID],Quantity ,[Total Price] from [Cart Details] where [Customer ID] ='{uid}' ";

                float total = 0f;
                xtTotal.Text = total.ToString();

                DataSet ds2 = new DataSet();
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd);
                adp2.Fill(ds2);

                con.Close();


                DataTable dt2 = ds2.Tables[0];


                foreach (DataRow rows in dt2.Rows)
                {
                    total += Convert.ToSingle(rows["Total Price"]);
                }

                xtTotal.Text = total.ToString();
                dgvCart.DataSource = dt2;
                dgvCart.Refresh();
                dgvCart.ClearSelection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void M_Customer_Load(object sender, EventArgs e)
        {
            this.LoadData();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var row = dgvProduct.SelectedRows[0];

            string productID = row.Cells["ProductID"].Value.ToString();
            string product = row.Cells["Product"].Value.ToString();
            float price = float.Parse(row.Cells["Price"].Value.ToString());
            int qty=0;

            
            if (cmbQty.SelectedItem == null) {
                qty = 1;
            }
            else
            {
                qty = Int32.Parse(cmbQty.SelectedItem.ToString());
            }

            float total = 0f;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"select stock from [Product] where [Product ID] ={productID}";
                int stock = (int)cmd.ExecuteScalar();

                if (qty > stock)
                {
                    MessageBox.Show($"You can take the {product} {stock} unit maximum.", "", MessageBoxButtons.OK);
                    return;
                }


                //new product naki existing product
                cmd.CommandText = $"select Count(*) from [Cart Details] where [Product ID] ={productID} and [Customer ID] = {uid} ";
                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    cmd.CommandText = $"insert into [Cart Details] values ({uid},{productID},'{product}',{qty},{price},{price * qty})";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"Update [Product]  set Stock={(stock - qty)} where [Product ID]={productID}";
                    cmd.ExecuteNonQuery();

                    dgvProduct.ClearSelection();

                }

                else
                {
                    cmd.CommandText = $"update [Cart Details] set Quantity = {qty},[Total Price]={price * qty} where [Product ID] = {productID}";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"update [Product]  set Stock={(stock - qty)} where [Product ID]={productID}";
                    cmd.ExecuteNonQuery();

                    dgvProduct.ClearSelection();

                }

                cmd.CommandText = $"select [Product Name] ,[Product ID],Quantity ,[Total Price] from [Cart Details] where [Customer ID] ={uid}";

                xtTotal.Text = total.ToString();
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                con.Close();


                DataTable dt = ds.Tables[0];


                foreach (DataRow rows in dt.Rows)
                {
                    total += Convert.ToSingle(rows["Total Price"]);
                }

                xtTotal.Text = total.ToString();
                dgvCart.DataSource = dt;
                dgvCart.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRMV_Click(object sender, EventArgs e)
        {



            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product that you want to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            try
            {
                var row = dgvCart.SelectedRows[0];

                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                string product = row.Cells["CartProduct"].Value.ToString();
                int qty = (int)row.Cells["CartQty"].Value;

                cmd.CommandText = $"select Stock from Product where [Product Name] ='{product}'";
                int stock = (int)cmd.ExecuteScalar();

                cmd.CommandText = $"delete from [Cart Details] where [Product Name] ='{product}' and [Customer ID]={uid}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"Update [Product]  set stock={(stock + qty)} where [Product Name]='{product}'";
                cmd.ExecuteNonQuery();

                float total = 0f;
                xtTotal.Text = total.ToString();

                cmd.CommandText = $"select [Product Name] ,[Product ID],Quantity ,[Total Price] from [Cart Details] where [Customer ID] ={uid} ";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                con.Close();

                DataTable dt = ds.Tables[0];


                foreach (DataRow rows in dt.Rows)
                {
                    total += Convert.ToSingle(rows["Total Price"]);
                }

                xtTotal.Text = total.ToString();
                dgvCart.DataSource = dt;
                dgvCart.Refresh();
            }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result= MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText =$"SELECT [Product Name], Quantity FROM [Cart Details] WHERE [Customer ID] = {uid}";

                DataTable dt0 = new DataTable();
                SqlDataAdapter adp0 = new SqlDataAdapter(cmd);
                adp0.Fill(dt0);

                foreach (DataRow row in dt0.Rows)
                {
                    string product = row["Product Name"].ToString();
                    int qty = Convert.ToInt32(row["Quantity"]);

                    cmd.CommandText = $"SELECT Stock FROM Product WHERE [Product Name] = '{product}'";
                    int stock = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = $"Update [Product]  set stock={(stock + qty)} where [Product Name]='{product}'";
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = $"delete from [Cart Details] where [Customer ID]={uid}";
                cmd.ExecuteNonQuery();

                float total = 0f;
                xtTotal.Text = total.ToString();

                this.LoadData();

            }
             
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        


        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DateTime dtm= DateTime.Now.Date;
            string today=dtm.Date.ToString();

            try
            {
                float totalAmount = float.Parse(xtTotal.Text);
                if (totalAmount == 0) return;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"select [user type] from userinfo where [UID]={uid}";
                var value = cmd.ExecuteScalar().ToString();

                if (value == "Customer")
                {
                    cmd.CommandText = $"insert into Orders values({uid},'{today}',{totalAmount},'Pending',Null,Null);";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = $"insert into Orders values({uid},'{today}',{totalAmount},'Confirmed',{uid},'{today}');";
                    cmd.ExecuteNonQuery();
                }

             

                cmd.CommandText = $"SELECT MAX([Order ID]) FROM Orders where[Customer ID] = {uid}";
                int oID = (int)cmd.ExecuteScalar();
                //MessageBox.Show(oID.ToString());

                //order details e individually entry 
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.IsNewRow == true) continue;
                    int productId = Convert.ToInt32(row.Cells["PID"].Value);
                    int qty = Convert.ToInt32(row.Cells["CartQty"].Value);

                    double price = float.Parse(row.Cells["TotalPrice"].Value.ToString());
                    double unitPrice = price / qty;

                    cmd.CommandText = $"insert into [Order Details] values({oID},{productId},{qty},{unitPrice},{price})";
                    cmd.ExecuteNonQuery();
                }
                //payment code
                string payment = cmbPmtype.SelectedItem.ToString();
                if (payment == "Cash on Delivery")
                {
                    cmd.CommandText = $"insert into [Payment Details] values({oID},'{payment}',{totalAmount},'Unpaid')";
                    cmd.ExecuteNonQuery ();
                }
                else {
                    cmd.CommandText = $"insert into [Payment Details] values({oID},'{payment}',{totalAmount},'paid')";
                    cmd.ExecuteNonQuery ();
                }
                cmd.CommandText = $"delete from [Cart Details] where [Customer ID]={uid}";
                cmd.ExecuteNonQuery();

                xtTotal.Clear();

                cmd.CommandText = $"select [Product Name] ,[Product ID],Quantity ,[Total Price] from [Cart Details] where [Customer ID] ={uid} ";
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvCart.DataSource = dt;
                dgvCart.Refresh();



                var result = MessageBox.Show("Do you want to see the order History?", "Confiramtion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    labelTnx.Text = "Thanks For your Order!!";
                    return;
                }

                else
                {
                    M_OrderHistory oh = new M_OrderHistory(uid);
                    oh.Show();
                    this.Hide();
                }
                
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

        private void M_Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            M_LoginForm lf=new M_LoginForm();
            lf.Show();
        }

        private void txtSrch_TextChanged(object sender, EventArgs e)
        {
            string txt= txtSrch.Text;

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [Product ID],[Product Name],Catagory.[Catagory Name],[Unit Price] from Product inner join Catagory on Catagory.[Catagory ID]=Product.[Catagory ID] where Product.[Product Name] like '%{txt}%'";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvProduct.DataSource = dt;
                dgvProduct.Refresh();


               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
        private void cmbFBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select [Product ID],[Product Name],Catagory.[Catagory Name],[Unit Price] from Product inner join Catagory on Catagory.[Catagory ID]=Product.[Catagory ID] where Catagory.[Catagory Name] ='{cmbFBC.Text}'";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                dgvProduct.DataSource = dt;
                dgvProduct.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbFBC_DropDown(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT [Catagory ID] ,[Catagory Name] from Catagory";

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                cmbFBC.DataSource = dt;
                cmbFBC.ValueMember = "Catagory ID";
                cmbFBC.DisplayMember = "Catagory Name";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
