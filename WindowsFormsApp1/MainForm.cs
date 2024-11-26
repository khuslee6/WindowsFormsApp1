using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private int LoggedInUserId;
        private string connectionString = "Data Source=LibraryManagementSystem.db;Version=3;";
        private FlowLayoutPanel postsPanel;
        private TextBox txtPostContent;

        public MainForm(int userId)
        {
            InitializeComponent();
            LoggedInUserId = userId;
            SetupMainForm();
            LoadPosts();
        }

        private void SetupMainForm()
        {
            // Set MainForm properties
            this.Text = "Social Media Platform";
            this.Size = new Size(800, 600);

            // Add MenuStrip
            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;
            menuStrip.Dock = DockStyle.Top;

            // Add Menu Items
            ToolStripMenuItem homeMenuItem = new ToolStripMenuItem("Home");
            ToolStripMenuItem profileMenuItem = new ToolStripMenuItem("Profile");
            ToolStripMenuItem settingsMenuItem = new ToolStripMenuItem("Settings");
            ToolStripMenuItem logoutMenuItem = new ToolStripMenuItem("Logout");

            // Add Click Events
            settingsMenuItem.Click += SettingsMenuItem_Click;
            profileMenuItem.Click += ProfileMenuItem_Click;
            logoutMenuItem.Click += LogoutMenuItem_Click;

            // Add Items to MenuStrip
            menuStrip.Items.Add(homeMenuItem);
            menuStrip.Items.Add(profileMenuItem);
            menuStrip.Items.Add(settingsMenuItem);
            menuStrip.Items.Add(logoutMenuItem);

            // Add MenuStrip to Form
            this.Controls.Add(menuStrip);

            // Add FlowLayoutPanel for Posts
            postsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            this.Controls.Add(postsPanel);

            // Add "Add Post" TextBox
            // Add "Add Post" TextBox
            txtPostContent = new TextBox
            {
                Dock = DockStyle.Bottom,
                Multiline = true,
                Height = 50,
                ForeColor = Color.Gray,
                Text = "What's on your mind?"
            };

            // Add Placeholder Events
            txtPostContent.GotFocus += (s, e) =>
            {
                if (txtPostContent.Text == "What's on your mind?")
                {
                    txtPostContent.Text = "";
                    txtPostContent.ForeColor = Color.Black;
                }
            };

            txtPostContent.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPostContent.Text))
                {
                    txtPostContent.Text = "What's on your mind?";
                    txtPostContent.ForeColor = Color.Gray;
                }
            };

            this.Controls.Add(txtPostContent);


            // Add "Add Post" Button
            Button addPostButton = new Button
            {
                Text = "Add Post",
                Dock = DockStyle.Bottom,
                Height = 50
            };

            addPostButton.Click += BtnAddPost_Click;

            this.Controls.Add(addPostButton);
        }

        private void ProfileMenuItem_Click(object sender, EventArgs e)
        {
            // Assuming you have a valid userId to pass
            int userId = LoggedInUserId; 
            ProfileForm profileForm = new ProfileForm(userId);
            profileForm.Show();

        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logging out...");
            Application.Exit(); // Exit application
        }

        private void BtnAddPost_Click(object sender, EventArgs e)
        {
            string content = txtPostContent.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Post content cannot be empty.");
                return;
            }

            string query = "INSERT INTO posts (user_id, content) VALUES (@UserId, @Content)";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", LoggedInUserId);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Post added successfully.");
            txtPostContent.Text = ""; // Clear the input
            LoadPosts(); // Refresh the post list
        }

        private void LoadPosts()
        {
            postsPanel.Controls.Clear(); // Clear existing posts

            string query = @"
                SELECT posts.content, posts.created_at, users.name
                FROM posts
                JOIN users ON posts.user_id = users.id
                ORDER BY posts.created_at DESC";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string userName = reader["name"].ToString();
                        string content = reader["content"].ToString();
                        string createdAt = reader["created_at"].ToString();

                        // Create a panel for each post
                        Panel postPanel = new Panel
                        {
                            Size = new Size(750, 100),
                            BorderStyle = BorderStyle.FixedSingle,
                            Margin = new Padding(10)
                        };

                        // Add a label for post details
                        Label postLabel = new Label
                        {
                            Text = $"{userName} ({createdAt}):\n{content}",
                            AutoSize = true
                        };

                        postPanel.Controls.Add(postLabel);
                        postsPanel.Controls.Add(postPanel);
                    }
                }
            }
        }
    }
}
