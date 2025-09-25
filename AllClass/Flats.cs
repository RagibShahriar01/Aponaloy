using APONALOY.DatabaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APONALOY.AllClass
{
    public partial class Flats : Form
    {
        private int currentUserId;
        private readonly DataAccess db = new DataAccess();
        public Flats(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
        }

        private void Flats_Load(object sender, EventArgs e)
        {
            labelNameGoodMorning.Text = db.GetUserByID(currentUserId)?.Username ?? string.Empty;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent R1 = new Rent(currentUserId);
            R1.Show();
        }

        private void pictureBoxBuy_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy R1 = new Buy(currentUserId);
            R1.Show();
        }

        private void pictureBoxacccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyAccount A1 = new MyAccount(currentUserId);
            A1.Show();

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
    }
}
