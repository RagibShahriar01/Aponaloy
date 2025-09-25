using APONALOY.DatabaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APONALOY.AllClass
{
    public partial class AdminPannel : Form
    {
        private readonly DataAccess db = new DataAccess();

        public AdminPannel()
        {
            InitializeComponent();
        }



        private void AdminPannel_Load(object sender, EventArgs e)
        {

            BtnApprove.Click -= BtnApprove_Click;
            BtnApprove.Click += BtnApprove_Click;

            BtnSendMessage.Click -= BtnSendMessage_Click;
            BtnSendMessage.Click += BtnSendMessage_Click;

            textBox1.TextChanged -= textBox1_TextChanged;
            textBox1.TextChanged += textBox1_TextChanged;

            // new hookups for users
            BtnDeleteUser.Click -= BtnDeleteUser_Click;
            BtnDeleteUser.Click += BtnDeleteUser_Click;

            textBoxUserSearch.TextChanged -= textBoxUserSearch_TextChanged;
            textBoxUserSearch.TextChanged += textBoxUserSearch_TextChanged;


            LoadRequests();
            LoadUsers();
        }






        private void LoadUsers()
        {
            try
            {
                DataTable dt = db.GetAllUserProfiles();
                dgvUsers.DataSource = dt;

                // hide internal columns if you want
                if (dgvUsers.Columns.Contains("ProfileID"))
                    dgvUsers.Columns["ProfileID"].Visible = false;
                if (dgvUsers.Columns.Contains("UserID"))
                    dgvUsers.Columns["UserID"].Visible = false; // optionally hide ID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }






        private void LoadRequests()
        {

            try
            {
                DataTable dt = db.GetAllFlatBuyRent();
                dgvRequests.DataSource = dt;

                // optional: hide columns you don't want to show
                if (dgvRequests.Columns.Contains("FlatBuyRentID"))
                    dgvRequests.Columns["FlatBuyRentID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading requests: " + ex.Message);
            }



        }




        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvRequests.CurrentRow == null)
            {
                MessageBox.Show("Please select a request row first.");
                return;
            }

            var row = dgvRequests.CurrentRow;
            if (row.Cells["FlatBuyRentID"].Value == null)
            {
                MessageBox.Show("Selected row has no ID.");
                return;
            }

            int id = Convert.ToInt32(row.Cells["FlatBuyRentID"].Value);
            DialogResult dr = MessageBox.Show("Mark this request as Approved?", "Confirm", MessageBoxButtons.YesNo);
            if (dr != DialogResult.Yes) return;

            try
            {
                db.UpdateFlatStatus(id, "Approved");
                MessageBox.Show("Request approved.");
                LoadRequests(); // refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving request: " + ex.Message);
            }


        }

        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            if (dgvRequests.CurrentRow == null)
            {
                MessageBox.Show("Please select a request row first.");
                return;
            }

            var row = dgvRequests.CurrentRow;
            if (row.Cells["UserID"].Value == null)
            {
                MessageBox.Show("Selected row has no UserID.");
                return;
            }

            int userId = Convert.ToInt32(row.Cells["UserID"].Value);
            string messageText = richTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(messageText))
            {
                MessageBox.Show("Enter a message before sending.");
                return;
            }

            try
            {
                // optional sender name
                db.AddMessage(userId,"Text From Admin", messageText);
                MessageBox.Show("Message sent to user.");
                richTextBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }

        }




        private void pictureBoxsingout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                SignInForm loginForm = new SignInForm();
                loginForm.Show();
            }

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = textBox1.Text.Trim();
                var dt = db.SearchFlatBuyRent(filter);
                dgvRequests.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a user row first.", "Select User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // make sure the selected row contains a UserID column (case-sensitive to your DataTable column name)
            var cell = dgvUsers.CurrentRow.Cells["UserID"];
            if (cell == null || cell.Value == null)
            {
                MessageBox.Show("Selected row has no UserID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = Convert.ToInt32(cell.Value);

            // optional safety: prevent deleting the currently logged-in admin (if you track current admin ID)
            // if (userId == currentAdminId) { MessageBox.Show("You cannot delete the currently logged-in admin."); return; }

            DialogResult dr = MessageBox.Show("Delete selected user and all related data? This action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr != DialogResult.Yes) return;

            try
            {
                int rowsDeleted = db.DeleteUserCascade(userId);
                MessageBox.Show($"User deleted. Rows affected: {rowsDeleted}", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh both grids so the admin panel shows updated data (auto-load)
                LoadUsers();
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void textBoxUserSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = textBoxUserSearch.Text.Trim();
                var dt = db.SearchUserProfiles(filter);
                dgvUsers.DataSource = dt;

                // optional: hide internal columns you don't want visible
                if (dgvUsers.Columns.Contains("ProfileID"))
                    dgvUsers.Columns["ProfileID"].Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("User search failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
