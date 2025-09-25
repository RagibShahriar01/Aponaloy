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
    public partial class Rent4 : Form
    {
        private readonly int currentUserId;
        private readonly Appointment appointment;
        private decimal basePrice = 27000;
        private bool parkingIncluded = true;
        private decimal parkingFee = 2000m;

        public Rent4(int userId, Appointment item)
        {
            InitializeComponent();
            currentUserId = userId;
            appointment = item;
        }

        private void Rent4_Load(object sender, EventArgs e)
        {
            radioWithParking.Checked = true;
            labelpricefix.Text = basePrice.ToString("N0") + " TK";

            radioWithParking.CheckedChanged += ParkingOption_CheckedChanged;
            radioWithoutParking.CheckedChanged += ParkingOption_CheckedChanged;

            UpdatePriceDisplay();

        }





        private void UpdatePriceDisplay()
        {
            decimal unitPrice = parkingIncluded ? basePrice : Math.Max(0, basePrice - parkingFee);
            labelpricefix.Text = unitPrice.ToString("N0") + " TK";
        }




        private void ParkingOption_CheckedChanged(object sender, EventArgs e)
        {
            parkingIncluded = radioWithParking.Checked;
            UpdatePriceDisplay();
        }

        private void Checkoutbutton_Click(object sender, EventArgs e)
        {
            decimal unitPrice = basePrice + (parkingIncluded ? parkingFee : 0m);

            var item = new Appointment
            {
                Flattype = "3 Beds, 3 Baths Flat(Uttara Sector-6)",
                Price = unitPrice,
                ParkingFee = parkingIncluded ? parkingFee : 0m,
                Purpose = labelpurpose.Text
            };

            this.Hide();
            AppointmentFormRent A1 = new AppointmentFormRent(currentUserId, item); // pass the NEW item
            A1.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rent flat = new Rent(currentUserId);
            flat.Show();
        }
    }
}
