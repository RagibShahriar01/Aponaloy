using APONALOY.DatabaseClass;
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
    public partial class AppointmentFormRent : Form
    {
        private readonly int currentUserId;
        private readonly Appointment appointment;
        private readonly DataAccess dbHelper = new DataAccess();

        public AppointmentFormRent(int userId, Appointment item)
        {
            InitializeComponent();
            currentUserId = userId;
            appointment = item;
        }

        private void AppointmentFormRent_Load(object sender, EventArgs e)
        {
            labelflatname.Text = appointment.Flattype;
            labelprice.Text = appointment.Price + "TK";
            labelparkingfee.Text = appointment.ParkingFee.ToString();
            labelpurpose.Text = appointment.Purpose ?? string.Empty;

            // start with the button disabled until all terms are agreed
            buttonGETappoinment.Enabled = false;

            // wire the CheckedChanged handlers (if not wired in Designer)
            checkBox1.CheckedChanged += Terms_CheckedChanged;
            checkBox2.CheckedChanged += Terms_CheckedChanged;
            checkBox3.CheckedChanged += Terms_CheckedChanged;
            checkBox4.CheckedChanged += Terms_CheckedChanged;
        }



        private bool AllTermsChecked()
        {
            return checkBox1.Checked
                && checkBox2.Checked
                && checkBox3.Checked
                && checkBox4.Checked;
        }




        private void Terms_CheckedChanged(object sender, EventArgs e)
        {
            buttonGETappoinment.Enabled = AllTermsChecked();
        }

        private void buttonGETappoinment_Click(object sender, EventArgs e)
        {
            // Delegate to DatabaseQ
            dbHelper.AddFlatInfo(
                currentUserId,
                appointment.Flattype,
                appointment.Price,
                appointment.ParkingFee,
                appointment.Purpose

            );

            MessageBox.Show("Your order is confirmed!", "Order Placed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            Flats A1 = new Flats(currentUserId);
            A1.Show();
        }
    }
}
