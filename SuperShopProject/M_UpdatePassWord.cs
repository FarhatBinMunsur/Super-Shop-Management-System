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
    public partial class M_UpdatePassWord : Form
    {
        int uid;
        public M_UpdatePassWord(int uid)
        {
            this.uid = uid;
            InitializeComponent();
        }

        private void M_UpdatePassWord_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string op=txtOP.Text;
            string np=txtNP.Text;


            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = AppHelper.cs;
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"select Password from UserInfo where UID={uid}";

                var result = cmd.ExecuteScalar().ToString();

                if (result != op)
                {
                    MessageBox.Show("Your Old Password is incorrect.\nInsert the correct Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtOP.Focus();
                    return;
                }

                cmd.CommandText = $"Update UserInfo set Password='{np}' where UID='{uid}'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your password Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                con.Close();
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
                cmd.CommandText = $"select [User Type] from UserInfo where UID={uid}";
                var value = cmd.ExecuteScalar().ToString();

                if (value == "Admin")
                {
                    T_AdminHomePage ahp = new T_AdminHomePage(uid);
                    ahp.Show();
                    this.Hide();
                }
                else if (value == "Manager")
                {
                    S_ManagerHomePage mhp = new S_ManagerHomePage(uid);
                    mhp.Show();
                    this.Hide();

                }

                else if(value == "Inventory Manager")
                {
                    M_InventoryManager im=new M_InventoryManager(uid);
                    im.Show();
                    this.Hide();
                }
                else
                {
                    M_CustomerHomePage chp = new M_CustomerHomePage(uid);
                    chp.Show();
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
