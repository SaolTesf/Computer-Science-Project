using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfessorApplication
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // field validation
            bool valid = true;
            string id = NetIDText.Text.Trim();
            string password = PasswordText.Text.Trim();

            if (id.Length < 1 || password.Length < 1)
            {
                valid = false;
            }
            if (valid == false)
            {
                MessageBox.Show("Please fill out all required fields", "Error");
                return;
            }
            // if you got to here, get database to make sure you have a valid log in
            MessageBox.Show("Log in", "Success");
        }

        // opens window for registration
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            // create new registration window
            Registration registerForm = new Registration();
            registerForm.Show();
        }
    }
}
