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
    public partial class M_LoginForm : Form
    {
        public M_LoginForm()
        {
            InitializeComponent();
        }

        private void M_LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSU_Click(object sender, EventArgs e)
        {
            M_SignUpForm sup = new M_SignUpForm();
            sup.Show();
            this.Hide();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
           
        }

        private void btnShow_MouseHover(object sender, EventArgs e)
        {
           txtPass.UseSystemPasswordChar = false;
        }

        private void btnShow_MouseLeave(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnLgn_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string pass=txtPass.Text;

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select * from UserInfo where Email='{email}' and Password='{pass}'";

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Invalid Email or PassWord.\nPlease Insert Correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    if (dt.Rows[0]["User Type"].ToString() == "Customer")
                    {
                        int uid=Convert.ToInt32(dt.Rows[0]["UID"]);
                        M_CustomerHomePage chp = new M_CustomerHomePage(uid);
                        chp.Show();
                        this.Hide();
                    }

                    else if (dt.Rows[0]["User Type"].ToString() == "Admin")
                    {
                        int uid = Convert.ToInt32(dt.Rows[0]["UID"]);
                        T_AdminHomePage ahp = new T_AdminHomePage(uid);
                        ahp.Show();
                        this.Hide();
                    }

                    else if (dt.Rows[0]["User Type"].ToString()== "Inventory Manager")
                    {
                        int uid = Convert.ToInt32(dt.Rows[0]["UID"]);
                        M_InventoryManager im = new M_InventoryManager(uid);
                        im.Show();
                        this.Hide();
                    }

                    else
                    {
                        int uid = Convert.ToInt32(dt.Rows[0]["UID"]);
                        S_ManagerHomePage mhp = new S_ManagerHomePage(uid);
                        mhp.Show();
                        this.Hide();
                    }


                }
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
