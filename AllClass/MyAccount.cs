using APONALOY.DatabaseClass;
using APONALOY.GetSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APONALOY.AllClass
{
    public partial class MyAccount : Form
    {
        private readonly int currentUserId;
        private readonly DataAccess dbHelper;
        public MyAccount(int currentUserId)
        {
            InitializeComponent();
            usernametextBox.ReadOnly = true;

            // Important: initialize the DatabaseQ helper so it is not null

            this.currentUserId = currentUserId;

            dbHelper = new DataAccess();

        }

        private void MyAccount_Load(object sender, EventArgs e)
        {

            try
            {

                editbutton.Click += editbutton_Click;
                savebutton.Click += savebutton_Click;
                cancelbutton.Click += cancelbutton_Click;


                LoadUserProfile();
                

                // AFTER loading or creating profile, load user's request + messages:
                LoadUserRequestAndMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }






        private void LoadUserRequestAndMessages()
        {
            try
            {
                // 1) request status (only their rows)
                DataTable req = dbHelper.GetFlatBuyRentByUser(currentUserId);
                if (req != null && req.Rows.Count > 0)
                {
                    // show latest request (first row because DB ordered DESC)
                    DataRow r = req.Rows[0];
                    lblRequestStatus.Text = r["Status"]?.ToString() ?? "Pending";
                    // optionally show more details in controls, e.g. price, purpose
                }
                

                // 2) latest admin message for this user (if any)
                DataTable msgs = dbHelper.GetMessagesForUser(currentUserId);
                if (msgs != null && msgs.Rows.Count > 0)
                {
                    DataRow m = msgs.Rows[0]; // latest (we ordered DESC)
                    txtAdminMessage.Text = $"{m["Sender"] ?? "Admin"} ({((DateTime)m["SentAt"]).ToString("g")}):{Environment.NewLine}{m["MessageText"]}";
                }
                else
                {
                    txtAdminMessage.Text = string.Empty; // no message
                }
            }
            catch (Exception ex)
            {
                // non-fatal: show message but keep profile visible
                Console.WriteLine("Error loading user request/messages: " + ex.Message);
            }
        }




        private void ToggleEditMode(bool isEditable)
        {
            emailtextBox.ReadOnly = !isEditable;
            mobiletextBox.ReadOnly = !isEditable;
            addresstextBox.ReadOnly = !isEditable;
            cancelbutton.Visible = isEditable;
            savebutton.Visible = isEditable;
            editbutton.Visible = !isEditable;

        }





        private void LoadUserProfile()
        {
            try
            {
                // Try to get profile row from tblUserProfile first
                DataRow userProfile = dbHelper.GetUserProfileById(currentUserId);

                if (userProfile != null)
                {
                    // Populate from profile table
                    usernametextBox.Text = userProfile["Username"]?.ToString() ?? string.Empty;
                    emailtextBox.Text = userProfile["Email"]?.ToString() ?? string.Empty;
                    mobiletextBox.Text = userProfile["Mobile"]?.ToString() ?? string.Empty;
                    addresstextBox.Text = userProfile["Address"]?.ToString() ?? string.Empty;

                    ToggleEditMode(false);
                    return;
                }

                // No profile row found — fall back to tblUser
                User user = dbHelper.GetUserByID(currentUserId);
                if (user != null)
                {
                    usernametextBox.Text = user.Username ?? string.Empty;
                    emailtextBox.Text = user.Email ?? string.Empty;
                    mobiletextBox.Text = user.Mobile ?? string.Empty;
                    addresstextBox.Text = user.Address ?? string.Empty;

                    // Optionally create a tblUserProfile row so it exists next time
                    try
                    {
                        var newProfile = new Profile
                        {
                            UserID = user.UserID,
                            Username = user.Username ?? string.Empty,
                            Email = user.Email ?? string.Empty,
                            Address = user.Address ?? string.Empty,
                            Mobile = user.Mobile ?? string.Empty
                        };
                        dbHelper.CreateUserProfile(newProfile);
                    }
                    catch
                    {
                        // ignore create-profile errors — we still show the profile from tblUser
                    }

                    ToggleEditMode(false);
                    return;
                }

                // If we get here, no user found either
                MessageBox.Show("User profile not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                string email = emailtextBox.Text.Trim();
                string username = usernametextBox.Text.Trim();
                string mobile = mobiletextBox.Text.Trim();
                string address = addresstextBox.Text.Trim();

                if (string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Email is required.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Full Name is required.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // Debug: Log profile update details
                Console.WriteLine($"Updating Profile: Username={username}, Email={email}, Address={address},  Mobile={mobile}");

                // Save to DB
                dbHelper.UpdateUserProfile(currentUserId, email, address, mobile);

                MessageBox.Show("Profile updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form to view-only mode
                ToggleEditMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void editbutton_Click(object sender, EventArgs e)
        {
            ToggleEditMode(true);
        }


        private void cancelbutton_Click(object sender, EventArgs e)
        {
            ToggleEditMode(false);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Flats flat = new Flats(currentUserId);
            flat.Show();
        }
    }
}
