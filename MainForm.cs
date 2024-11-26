using System;
using System.Drawing;
using System.Windows.Forms;

namespace SocialMediaApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupMainForm();
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
            logoutMenuItem.Click += LogoutMenuItem_Click;

            // Add Items to MenuStrip
            menuStrip.Items.Add(homeMenuItem);
            menuStrip.Items.Add(profileMenuItem);
            menuStrip.Items.Add(settingsMenuItem);
            menuStrip.Items.Add(logoutMenuItem);

            // Add MenuStrip to Form
            this.Controls.Add(menuStrip);

            // Add FlowLayoutPanel for Posts
            FlowLayoutPanel postsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // Add Sample Posts
            for (int i = 1; i <= 10; i++)
            {
                Panel postPanel = new Panel
                {
                    Size = new Size(750, 100),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                Label postLabel = new Label
                {
                    Text = $"Post #{i}: This is an example post content.",
                    AutoSize = true
                };

                postPanel.Controls.Add(postLabel);
                postsPanel.Controls.Add(postPanel);
            }

            this.Controls.Add(postsPanel);

            // Add "Add Post" Button
            Button addPostButton = new Button
            {
                Text = "Add Post",
                Dock = DockStyle.Bottom,
                Height = 50
            };

            addPostButton.Click += AddPostButton_Click;

            this.Controls.Add(addPostButton);
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            // Handle logout logic
            MessageBox.Show("Logging out...");
            Application.Exit(); // Exit application
        }

        private void AddPostButton_Click(object sender, EventArgs e)
        {
            // Handle adding a new post
            MessageBox.Show("Add Post Clicked!");
        }
    }
}
