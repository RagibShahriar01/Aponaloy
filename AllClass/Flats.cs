using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealState.AllClass
{
    public partial class Flats : Form
    {
        private int currentUserId;
        public Flats(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
        }

        private void Flats_Load(object sender, EventArgs e)
        {

        }
    }
}
