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
    public partial class S_ProductOperation : Form
    {
        int uid;
        public S_ProductOperation(int uid)
        {
            InitializeComponent();
            this.uid = uid;
        }

        private void S_ProductOperation_Load(object sender, EventArgs e)
        {
            this.LoadData();
            this.NewData();
        }

        private void LoadData()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open(); //connect button press kora

                SqlCommand cmd = new SqlCommand(); // Panel create kora jekhane query likha hoi
                cmd.Connection = conn; // je database e query likhbe sheta bolse (conn)
                cmd.CommandText = $"select Product.*, Catagory.[Catagory Name] from Product inner join Catagory on Catagory.[Catagory ID] = Product.[Catagory ID];select * from Catagory ;\r\n";

                //eta jodi insert/delete query hoto tahole data return kora lagto na --> tokhon cmd.ExecuteNonQuery(); conn.Close(); ei 2 ta function call korle hoto

                // but eta jehtu select query tai data return korbe so variable newa lagbe & result section return korar jnno dataset nilam
                DataSet ds = new DataSet(); // c# er party ds variable & Database er party cmd
                SqlDataAdapter adp = new SqlDataAdapter(cmd); //cmd ke pass 
                adp.Fill(ds);  // Database theke result dataset e chole asche

                conn.Close();

                DataTable dt = ds.Tables[0]; // Data Table fetch kora & table 1 ta tai [0] dewa
                dgvProduct.AutoGenerateColumns = false;  // Responsive er jnno kora, akhn ar auto design korbe na gridview te, design e giye amader design korte hobe cols & Rows
                dgvProduct.DataSource = dt;
                dgvProduct.Refresh(); // load howar shathe shate newly added jkono data load hobe
                dgvProduct.ClearSelection(); // pre selected kichu thakbe na (Visual problem solved)

                // porer 1 ta query er jnno likha code
                 
                DataTable dt1 = ds.Tables[1];
                cmbCatagoryName.DataSource = dt1;
                cmbCatagoryName.ValueMember = "Catagory ID";   // jeta diye data pabo
                cmbCatagoryName.DisplayMember = "Catagory Name";
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

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex + "");

            if (e.RowIndex >= 0)
            {
                txtProductID.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtStock.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbCatagoryName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtUnitPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();





            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.NewData();
        }

        private void NewData()
        {
            // etar kaj holo new button click korle right side e khali hoye jabe
            txtProductID.Text = "Auto Generated";
            txtProductName.Text = "";
            txtStock.Text = "";
            cmbCatagoryName.Text = "";
            txtUnitPrice.Text = "";


            dgvProduct.ClearSelection(); // new button click korle kono row selected thakbe na
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // kokhon bujhbo je kon row ta delete korbo jokhon kono kichu select kora thakbe na
            string pId = txtProductID.Text;
            if (pId == "Auto Generated")
            {
                MessageBox.Show("Please select a row first","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Are you Confirm ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.No)
            {
                return;

            }

            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open(); //connect button press kora

                SqlCommand cmd = new SqlCommand(); // Panel create kora jekhane query likha hoi
                cmd.Connection = conn; // je database e query likhbe sheta bolse (conn)
                cmd.CommandText = $"delete from Product where [Product ID] ={pId}";


                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Successfully Deleted");
                this.LoadData(); // delete button click er shathe shate gridview thekeo jaate refresh hoye jai otherwise database theke delete hobe but design e gridview te dekhabe, then refresh button e click kora lagbe user ke jeta hassle
                this.NewData(); // Right side o jaate khali thake


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1st kaj right side e data fetch kora
            string pId = txtProductID.Text;
            string pName = txtProductName.Text;
            string pStock = txtStock.Text;
            string pCatagoryName = cmbCatagoryName.SelectedValue.ToString();
            string pUnitPrice = txtUnitPrice.Text;



            string query = ""; // etar karone delete query er pura code copy kora lagtese na karon pura code same just query different tai ei variable ta newa & nicher line e if,else er vitor query ta likhle hocche insert & Update er

            // insert or update ? 
            if (pId == "Auto Generated" ) // jokhon new button click kore hoise tokhon insert hobe
            {
                if (pName == "" || pStock == "" || pUnitPrice == "" || pCatagoryName == "")
                {
                    MessageBox.Show("Select a Row to Update or Fill the information to add items","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    


                }
                else
                {
                    query = $"insert into Product values('{pName}', {pStock},'{pCatagoryName}',{pUnitPrice})";

                }
            }
            else  // jokhon auto generated likha thakbe na tar mane kono ekta row selected hoise then oita update hobe
            {
                query = $"update Product set [Product Name]='{pName}', Stock={pStock},[Catagory ID]={pCatagoryName},[Unit Price]={pUnitPrice} where [Product ID] ={pId}";

            }

            // Query likha hoise but Database e execute korte hobe so Delete button er try catch er full code copy korbo just change in cmd.CommandText
            try
            {

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = AppHelper.cs;
                conn.Open(); //connect button press kora

                SqlCommand cmd = new SqlCommand(); // Panel create kora jekhane query likha hoi
                cmd.Connection = conn; // je database e query likhbe sheta bolse (conn)
                cmd.CommandText = query;


                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Successful","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.LoadData(); // delete button click er shathe shate gridview thekeo jaate refresh hoye jai otherwise database theke delete hobe but design e gridview te dekhabe, then refresh button e click kora lagbe user ke jeta hassle
                this.NewData(); // Right side o jaate khali thake


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

        private void S_ProductOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            S_ManagerHomePage mh = new S_ManagerHomePage(uid);
            mh.Show();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        //private void btnBack_Click(object sender, EventArgs e)
        //{
        //    //S_ManagerHomePage mp = new S_ManagerHomePage();
        //    //mp.Show();
        //    //this.Hide();
        //}
    }
}
