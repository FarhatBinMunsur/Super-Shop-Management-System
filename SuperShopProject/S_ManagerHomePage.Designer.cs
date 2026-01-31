namespace SuperShopProject
{
    partial class S_ManagerHomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnViewProducts = new System.Windows.Forms.Button();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnUpdatePassword = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.btnSR = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewProducts
            // 
            this.btnViewProducts.BackColor = System.Drawing.Color.Maroon;
            this.btnViewProducts.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewProducts.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnViewProducts.Location = new System.Drawing.Point(133, 131);
            this.btnViewProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewProducts.Name = "btnViewProducts";
            this.btnViewProducts.Size = new System.Drawing.Size(133, 72);
            this.btnViewProducts.TabIndex = 0;
            this.btnViewProducts.Text = "View Products";
            this.btnViewProducts.UseVisualStyleBackColor = false;
            this.btnViewProducts.Click += new System.EventHandler(this.btnViewProducts_Click);
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.BackColor = System.Drawing.Color.DarkBlue;
            this.btnViewOrders.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewOrders.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnViewOrders.Location = new System.Drawing.Point(333, 131);
            this.btnViewOrders.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(133, 72);
            this.btnViewOrders.TabIndex = 1;
            this.btnViewOrders.Text = "View Orders";
            this.btnViewOrders.UseVisualStyleBackColor = false;
            this.btnViewOrders.Click += new System.EventHandler(this.btnViewOrders_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLogOut.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(333, 237);
            this.btnLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(133, 72);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnUpdatePassword
            // 
            this.btnUpdatePassword.BackColor = System.Drawing.Color.DarkCyan;
            this.btnUpdatePassword.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdatePassword.Location = new System.Drawing.Point(133, 237);
            this.btnUpdatePassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdatePassword.Name = "btnUpdatePassword";
            this.btnUpdatePassword.Size = new System.Drawing.Size(133, 72);
            this.btnUpdatePassword.TabIndex = 3;
            this.btnUpdatePassword.Text = "Update Password";
            this.btnUpdatePassword.UseVisualStyleBackColor = false;
            this.btnUpdatePassword.Click += new System.EventHandler(this.btnUpdatePassword_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.BackColor = System.Drawing.Color.Gold;
            this.buttonOrder.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOrder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonOrder.Location = new System.Drawing.Point(246, 30);
            this.buttonOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(133, 72);
            this.buttonOrder.TabIndex = 4;
            this.buttonOrder.Text = "Create Order";
            this.buttonOrder.UseVisualStyleBackColor = false;
            this.buttonOrder.Click += new System.EventHandler(this.buttonOrder_Click);
            // 
            // btnSR
            // 
            this.btnSR.BackColor = System.Drawing.Color.OrangeRed;
            this.btnSR.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSR.Location = new System.Drawing.Point(233, 341);
            this.btnSR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSR.Name = "btnSR";
            this.btnSR.Size = new System.Drawing.Size(133, 72);
            this.btnSR.TabIndex = 5;
            this.btnSR.Text = "Sales Report";
            this.btnSR.UseVisualStyleBackColor = false;
            this.btnSR.Click += new System.EventHandler(this.btnSR_Click);
            // 
            // S_ManagerHomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SuperShopProject.Properties.Resources.WhatsApp_Image_2026_01_20_at_18_24_16;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(646, 424);
            this.Controls.Add(this.btnSR);
            this.Controls.Add(this.buttonOrder);
            this.Controls.Add(this.btnUpdatePassword);
            this.Controls.Add(this.btnLogOut);
            this.Controls.Add(this.btnViewOrders);
            this.Controls.Add(this.btnViewProducts);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "S_ManagerHomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manager Home Page";
            this.Load += new System.EventHandler(this.S_ManagerHomePage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViewProducts;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnUpdatePassword;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button btnSR;
    }
}