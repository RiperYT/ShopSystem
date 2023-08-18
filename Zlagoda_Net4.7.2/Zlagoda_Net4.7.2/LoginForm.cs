using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zlagoda_Net4._7._2.Cashier;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Repositories;

namespace Zlagoda_Net4._7._2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Label_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var password = sha256(Password.Text);
            if (new AdminRepository().IsAdmin(Login.Text, password))
            {
                StaticInfo.id = this.Login.Text;
                StaticInfo.password = password;
                StaticInfo.role = "admin";

                var products = new Zlagoda_Net4._7._2.Admin.Products();
                Hide();
                products.ShowDialog();
                Close();
            }
            else if (new CashierRepository().IsCashier(Login.Text, password))
            {
                StaticInfo.id = Login.Text;
                StaticInfo.password = password;
                StaticInfo.role = "cashier";

                var products = new Products();
                Hide();
                products.ShowDialog();
                Close();
            }
            Error.Text = "Id or password is inccorect";
        }

        private string sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
