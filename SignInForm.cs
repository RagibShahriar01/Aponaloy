using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APONALOY.AllClass;
using APONALOY.DatabaseClass;
using APONALOY.GetSet;

namespace APONALOY
{
    public partial class SignInForm : Form
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {
            passwordtext.UseSystemPasswordChar = true;

            DataAccess db = new DataAccess();
            bool isConnected = db.TestConnection();
            if (!isConnected)
            {
                // Disable login functionality or inform the user
                Signinbutton.Enabled = false;
                MessageBox.Show("Cannot connect to the database. Please check your connection settings.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void signupbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp registerForm = new SignUp();
            registerForm.Show();
        }

        private void Signinbutton_Click(object sender, EventArgs e)
        {
            string username = usernametext.Text.Trim();
            string password = passwordtext.Text;

            // Input Validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Initialize Database Helper
                DataAccess db = new DataAccess();

                // Retrieve User from Database
                User existingUser = db.GetUserByUsername(username);

                if (existingUser == null)
                {
                    // User not found
                    MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (password == existingUser.Password)
                {
                    // Authentication successful
                    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Redirect based on role
                    RedirectUser(existingUser.Role, existingUser.UserID);


                    // Optionally, close the Login Form
                    this.Hide();
                }
                else
                {
                    // Password does not match
                    MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show("An error occurred during login: " + ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }








        private void RedirectUser(string role, int userId)
        {
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("User role is not defined. Please contact support.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (role.Trim().ToLower())
            {
                case "admin":
                    // Instantiate and show the AdminDashboard
                    AdminPannel adminDashboard = new AdminPannel();
                    adminDashboard.Show();
                    break;


                case "user":
                    // Retrieve the UserID using the UserID
                    DataAccess dbUser = new DataAccess();
                    User user = dbUser.GetUserByID(userId);

                    if (user != null)
                    {
                        // Instantiate the JewelleryDashboard with the correct AuthorID
                        Flats flat = new Flats(user.UserID);
                        flat.Show();
                    }
                    else
                    {
                        MessageBox.Show("User profile not found. Please contact support.", "Profile Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                default:
                    MessageBox.Show("Unknown user role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            RecoverPassword re = new RecoverPassword();
            re.Show();
        }
    }
}
