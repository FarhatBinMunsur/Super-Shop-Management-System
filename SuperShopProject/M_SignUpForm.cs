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
    public partial class M_SignUpForm : Form
    {
        public M_SignUpForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            M_LoginForm LF=new M_LoginForm();
            LF.Show();
            this.Hide();
        }

        private void M_SignUpForm_Load(object sender, EventArgs e)
        {

        }

        private void M_SignUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            M_LoginForm lf=new M_LoginForm();
            lf.Show();
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            if (userName == "")
            {
                MessageBox.Show("Enter the User Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtUsername.Focus();
                return;
            }

            string pass=txpass.Text;
            string cpass=txtCpass.Text;
            if (cpass == "" || pass=="")
            {
                MessageBox.Show("Enter the Password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtCpass.Focus();
                return;
            }

            if (pass != cpass)
            {
                MessageBox.Show("Paswords are not matching please check again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string mobile=txtMobile.Text;
            if (mobile.Length<11)
            {
                MessageBox.Show("Enter the Correct Mobile Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtMobile.Focus();
                return;
            }
            string Email =txtEmail.Text;
            if (Email == "")
            {
                MessageBox.Show("Enter the EMail.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtEmail.Focus();
                return;
            }
            string address=rtxtAdress.Text;
            if (address == "")
            {
                MessageBox.Show("Enter the Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                rtxtAdress.Focus();
                return;
            }

            string gender=null;
            if (rbFemale.Checked) gender = "Female";
            else if (rbMale.Checked) gender = "Male";

            if(cbAgree.Checked)
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = AppHelper.cs;
                    con.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"insert into UserInfo values('{userName}','{Email}','{mobile}','{cpass}','Customer','{address}',Null,Null,'{gender}')";
                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Registered Successfully.","Confirmation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                    M_LoginForm lf=new M_LoginForm();
                    lf.Show();
                    this.Hide();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("You Must agree to the terms and conditions to continue.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }
    }
}
