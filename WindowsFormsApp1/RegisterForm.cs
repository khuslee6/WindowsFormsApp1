using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RegisterForm : Form
    {
        private string connectionString = "Data Source=LibraryManagementSystem.db;Version=3;";

        public RegisterForm()

        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click; // Attach the event handler
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "All fields are required.";
                return;
            }

            string passwordHash = HashPassword(password);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (name, email, password, user_type) VALUES (@Username, @Email, @PasswordHash, @UserType)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@UserType", "regular"); // Default value for user_type

                        command.ExecuteNonQuery();
                        lblMessage.Text = "Registration successful!";
                        ClearFields();
                    }
                }
                catch (SQLiteException ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Helper function to clear text fields
        private void ClearFields()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
        }

        // Helper function to log errors
        private void LogError(Exception ex)
        {
            // Add your logging mechanism here (e.g., write to a log file or database)
            Console.WriteLine(ex.Message); // Example placeholder
        }

        // Email validation function
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
