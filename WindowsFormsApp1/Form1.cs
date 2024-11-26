using System;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=LibraryManagementSystem.db;Version=3;";


        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string passwordHash = HashPassword(password);


            int userId = GetLoggedInUserId(email, passwordHash);
            if (userId != -1)
            {
                MainForm mainForm = new MainForm(userId);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private int GetLoggedInUserId(string email, string passwordHash)
        {
            string query = "SELECT id FROM users WHERE email = @Email AND password = @PasswordHash";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private bool AuthenticateUser(string email, string password)
        {
            string passwordHash = HashPassword(password); 

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=LibraryManagementSystem.db;Version=3;"))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @PasswordHash";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; // Return true if a matching user is found
                    }
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                    return false;
                }
            }
        }

        private bool HashPassword(string inputPassword, string storedHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedHash;
        }

        // Helper Method for Hashing Password
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

    }
}
