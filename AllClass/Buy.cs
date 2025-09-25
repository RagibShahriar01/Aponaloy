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
    public partial class Buy : Form
    {
        private readonly int currentUserId;
        private readonly Appointment appointment;
        public Buy(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
        }

        private void Buy_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            this.Hide();
            Buy1 A1 = new Buy1(currentUserId, appointment);
            A1.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Flats flat = new Flats(currentUserId);
            flat.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy2 A2 = new Buy2(currentUserId, appointment);
            A2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy3 A4 = new Buy3(currentUserId, appointment);
            A4.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy4 A5 = new Buy4(currentUserId, appointment);
            A5.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy5 A6 = new Buy5(currentUserId, appointment);
            A6.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Buy6 A7 = new Buy6(currentUserId, appointment);
            A7.Show();
        }
    }
}
