using System.Drawing;

namespace WindowsFormsApp1
{
    partial class ProfileForm
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "ProfileForm";
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();

            this.txtName = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(20, 80),
                Size = new System.Drawing.Size(200, 20)
            };
            this.txtBio = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(20, 110),
                Size = new System.Drawing.Size(200, 20)
            };
            this.lblEmail = new System.Windows.Forms.Label
            {
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(200, 20)
            };
            this.Controls.Add(this.txtName); 
            this.Controls.Add(this.txtBio); 
            this.Controls.Add(this.lblEmail);

            // Initialize FlowLayoutPanel for Posts
            this.flowLayoutPanelPosts = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPosts.AutoScroll = true;
            this.flowLayoutPanelPosts.Name = "flowLayoutPanelPosts";

            // Add FlowLayoutPanel to the Form
            this.Controls.Add(this.flowLayoutPanelPosts);

            // Initialize TextBox for email and password (optional, remove if not needed)
            this.txtEmail = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(200, 20),
                Text = "Email", // Placeholder text ForeColor = SystemColors.GrayText // Set placeholder text color
            };
                this.txtPassword = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(200, 20),
                Text = "Password",
                ForeColor = SystemColors.GrayText,
                PasswordChar = '\0' // To show placeholder text
            };

            // Add TextBoxes to the Form
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
        }

        #endregion

        // Declare UI components
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPosts;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
    }
}
