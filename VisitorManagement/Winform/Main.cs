using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisitorManagement.Winform
{
    public partial class Main : Form
    { 
        Login login;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            login = new Login();
            this.CenterToScreen();
            register1.BringToFront();
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            register1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkout1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            report1.BringToFront();
        }
    }
}
