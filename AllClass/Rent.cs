using APONALOY.GetSet;
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
    public partial class Rent : Form
    {
        private readonly int currentUserId;
        private readonly Appointment appointment;
        public Rent(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
        }

        private void Rent_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Flats flat = new Flats(currentUserId);
            flat.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent1 A1 = new Rent1(currentUserId, appointment);
            A1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent2 A2 = new Rent2(currentUserId, appointment);
            A2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent3 A13 = new Rent3(currentUserId, appointment);
            A13.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent4 A15 = new Rent4(currentUserId, appointment);
            A15.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent5 A19 = new Rent5(currentUserId, appointment);
            A19.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent6 A10 = new Rent6(currentUserId, appointment);
            A10.Show();
        }
    }
}
