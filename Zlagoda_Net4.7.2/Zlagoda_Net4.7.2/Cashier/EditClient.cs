using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Data;
using Zlagoda_Net4._7._2.Repositories;

namespace Zlagoda_Net4._7._2.Cashier
{
    public partial class EditClient : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        private readonly string card_number;
        public EditClient(string card_number)
        {
            InitializeComponent();
            this.card_number = card_number;
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var client = _cashierRepository.GetClientFullInfo(card_number);
                SurnameBox.Text = client.Surname;
                NameBox.Text = client.Name;
                PatronymicBox.Text = client.Patronymic;
                NumberBox.Text = client.Phone_Number;
                CityBox.Text = client.City;
                StreetBox.Text = client.Street;
                ZipCodeBox.Text = client.ZipCode;
                PercentBox.Text = client.Percent.ToString();
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    ClientFull client = new ClientFull();

                    if (SurnameBox.Text.Length > 0)
                        if (SurnameBox.Text.Length <= 50)
                            client.Surname = SurnameBox.Text;
                        else
                            throw new Exception("Surname needs to be less 51");
                    else
                        throw new Exception("Surname cannot be empty");

                    if (NameBox.Text.Length > 0)
                        if (NameBox.Text.Length <= 50)
                            client.Name = NameBox.Text;
                        else
                            throw new Exception("Name needs to be less 51");
                    else
                        throw new Exception("Name cannot be empty");

                    if (PatronymicBox.Text.Length <= 50)
                        if (PatronymicBox.Text.Length == 0)
                            client.Patronymic = null;
                        else
                            client.Patronymic = PatronymicBox.Text;
                    else
                        throw new Exception("Patronymic needs to be less 51");

                    if (NumberBox.Text.Length > 0)
                        if (NumberBox.Text.Length <= 13)
                            client.Phone_Number = NumberBox.Text;
                        else
                            throw new Exception("Number needs to be less 14");
                    else
                        throw new Exception("Number cannot be empty");

                    if (CityBox.Text.Length <= 50)
                        if (CityBox.Text.Length == 0)
                            client.City = null;
                        else
                            client.City = CityBox.Text;
                    else
                        throw new Exception("City needs to be less 51");

                    if (StreetBox.Text.Length <= 50)
                        if (StreetBox.Text.Length == 0)
                            client.Street = null;
                        else
                            client.Street = StreetBox.Text;
                    else
                        throw new Exception("Street needs to be less 51");

                    if (ZipCodeBox.Text.Length <= 50)
                        if (ZipCodeBox.Text.Length == 0)
                            client.ZipCode = null;
                        else
                            client.ZipCode = ZipCodeBox.Text;
                    else
                        throw new Exception("ZipCode needs to be less 10");

                    if (PercentBox.Text.Length > 0)
                        if (int.TryParse(PercentBox.Text, out int t))
                            if (t >= 0 && t <= 100)
                                client.Percent = t;
                            else
                                throw new Exception("Percent needs to be bitween 0 and 100");
                        else
                            throw new Exception("Percent needs to be a number");
                    else
                        throw new Exception("Percent cannot be empty");

                    client.Card_Number = card_number;

                    _cashierRepository.UpdateClient(client);
                    Close();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = ex.Message;
                }
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

        public string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void AddContactLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
