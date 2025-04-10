using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace ProfessorApplication
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }


        // verifies whether or not the email textbox has an actual email
        public bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Register button
        private void button4_Click(object sender, EventArgs e)
        {
            // field validation
            bool valid = true;
            string email = EmailText.Text;
            string id = NetIDText.Text.Trim();
            string password = PasswordText.Text.Trim();

            if (id.Length < 1 || password.Length < 1 || email.Length < 1)
            {
                valid = false;
            }
            if (valid == false)
            {
                MessageBox.Show("Please fill out all required fields", "Error");
                return;
            }
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address", "Error");
                return;
            }
            // if get to here, successful registration, do some posting
            MessageBox.Show("Successfully registered account!", "Success");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            // closes window
            this.Close();
        }

        private void NetIDText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
