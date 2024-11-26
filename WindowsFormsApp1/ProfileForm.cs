using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
    public partial class ProfileForm : Form
    {
        private int LoggedInUserId;
        private string connectionString = "Data Source=LibraryManagementSystem.db;Version=3;";

        public ProfileForm(int userId)
        {


            InitializeComponent();
            LoggedInUserId = userId;
            LoadProfileData();
            LoadPosts();
        }
        private void LoadProfileData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name, bio, email FROM users WHERE id = @UserId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", LoggedInUserId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["name"].ToString();
                            txtBio.Text = reader["bio"]?.ToString();
                            lblEmail.Text = reader["email"].ToString();
                        }
                    }
                }
            }
        }
        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            string newName = txtName.Text.Trim();
            string newBio = txtBio.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE users SET name = @Name, bio = @Bio WHERE id = @UserId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@Bio", newBio);
                    command.Parameters.AddWithValue("@UserId", LoggedInUserId);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Profile updated successfully.");
        }
        private void LoadPosts()
        {
            // Clear existing controls
            flowLayoutPanelPosts.Controls.Clear();

            // Example: Add posts dynamically from the database
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT content FROM posts WHERE user_id = @UserId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", LoggedInUserId);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string content = reader["content"].ToString();

                            // Create a panel for each post
                            Panel postPanel = new Panel
                            {
                                Size = new Size(700, 100),
                                BorderStyle = BorderStyle.FixedSingle,
                                Margin = new Padding(10)
                            };

                            Label postLabel = new Label
                            {
                                Text = content,
                                AutoSize = true,
                                Location = new Point(10, 10)
                            };

                            postPanel.Controls.Add(postLabel);
                            flowLayoutPanelPosts.Controls.Add(postPanel);
                        }
                    }
                }
            }
        }

        private Panel CreatePostPanel(int postId, string content)
        {
            Panel panel = new Panel
            {
                Size = new Size(750, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            Label lblContent = new Label
            {
                Text = content,
                AutoSize = true,
                MaximumSize = new Size(700, 0)
            };

            Button btnEdit = new Button
            {
                Text = "Edit",
                Size = new Size(75, 25),
                Location = new Point(650, 10)
            };

            btnEdit.Click += (s, e) => EditPost(postId, content);

            Button btnDelete = new Button
            {
                Text = "Delete",
                Size = new Size(75, 25),
                Location = new Point(650, 40)
            };

            btnDelete.Click += (s, e) => DeletePost(postId);

            panel.Controls.Add(lblContent);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);

            return panel;
        }
        private void EditPost(int postId, string oldContent)
        {
            string newContent = Microsoft.VisualBasic.Interaction.InputBox("Edit your post:", "Edit Post", oldContent);
            if (!string.IsNullOrEmpty(newContent) && newContent != oldContent)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE posts SET content = @Content WHERE id = @PostId";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Content", newContent);
                        command.Parameters.AddWithValue("@PostId", postId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Post updated successfully.");
                LoadPosts(); // Refresh posts
            }
        }
        private void DeletePost(int postId)
        {
            if (MessageBox.Show("Are you sure you want to delete this post?", "Delete Post", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM posts WHERE id = @PostId";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PostId", postId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Post deleted successfully.");
                LoadPosts(); // Refresh posts
            }
        }
    }
}
