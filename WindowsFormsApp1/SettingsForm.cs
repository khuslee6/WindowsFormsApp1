using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            SetupSettingsForm();
        }

        private void SetupSettingsForm()
        {
            this.Text = "Settings - Change Password";
            this.Size = new System.Drawing.Size(400, 300);

            // Labels and TextBoxes
            Label lblCurrentPassword = new Label
            {
                Text = "Current Password:",
                Location = new System.Drawing.Point(30, 30),
                AutoSize = true
            };
            TextBox txtCurrentPassword = new TextBox
            {
                Location = new System.Drawing.Point(150, 30),
                Width = 200,
                PasswordChar = '*'
            };

            Label lblNewPassword = new Label
            {
                Text = "New Password:",
                Location = new System.Drawing.Point(30, 70),
                AutoSize = true
            };
            TextBox txtNewPassword = new TextBox
            {
                Location = new System.Drawing.Point(150, 70),
                Width = 200,
                PasswordChar = '*'
            };

            Label lblConfirmPassword = new Label
            {
                Text = "Confirm Password:",
                Location = new System.Drawing.Point(30, 110),
                AutoSize = true
            };
            TextBox txtConfirmPassword = new TextBox
            {
                Location = new System.Drawing.Point(150, 110),
                Width = 200,
                PasswordChar = '*'
            };

            // Save Button
            Button btnSave = new Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(150, 160),
                Width = 100
            };
            btnSave.Click += (sender, e) =>
            {
                if (ValidatePassword(txtCurrentPassword.Text, txtNewPassword.Text, txtConfirmPassword.Text))
                {
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            };

            // Add controls to the form
            this.Controls.Add(lblCurrentPassword);
            this.Controls.Add(txtCurrentPassword);
            this.Controls.Add(lblNewPassword);
            this.Controls.Add(txtNewPassword);
            this.Controls.Add(lblConfirmPassword);
            this.Controls.Add(txtConfirmPassword);
            this.Controls.Add(btnSave);
        }

        private bool ValidatePassword(string current, string newPassword, string confirmPassword)
        {
            // Dummy password for demonstration
            string existingPassword = "password123";

            if (current != existingPassword)
            {
                MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // In a real app, you'd save the new password securely.
            return true;
        }
    }
}
