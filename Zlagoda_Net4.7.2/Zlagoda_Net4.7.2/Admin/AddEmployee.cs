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
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Data;
using Zlagoda_Net4._7._2.Repositories;

namespace Zlagoda_Net4._7._2.Admin
{
    public partial class AddEmployee : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public AddEmployee()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                DateOfBirthPicker.MaxDate = DateTime.Now;
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
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var employee = new Employee();

                    if (SurnameBox.Text.Length > 0)
                        if (SurnameBox.Text.Length <= 50)
                            employee.surname = SurnameBox.Text;
                        else
                            throw new Exception("Surname needs to be less 51");
                    else
                        throw new Exception("Surname cannot be empty");

                    if (NameBox.Text.Length > 0)
                        if (NameBox.Text.Length <= 50)
                            employee.name = NameBox.Text;
                        else
                            throw new Exception("Name needs to be less 51");
                    else
                        throw new Exception("Name cannot be empty");

                    if (PatronymicBox.Text.Length <= 50)
                        if (PatronymicBox.Text.Length == 0)
                            employee.patronymic = null;
                        else
                            employee.patronymic = PatronymicBox.Text;
                    else
                        throw new Exception("Patronymic needs to be less 51");

                    if (RoleBox.Text.Equals("admin"))
                        employee.role = RoleBox.Text;
                    else if (RoleBox.Text.Equals("cashier"))
                        employee.role = RoleBox.Text;
                    else
                        throw new Exception("Role needs be 'admin' or 'cashier'");

                    if (decimal.TryParse(SalaryBox.Text, out var price))
                        if (price > 0)
                            employee.salary = price;
                        else
                            throw new Exception("Salary needs to be more 0");
                    else
                        throw new Exception("Salary is not decimal");

                    employee.birth = DateOfBirthPicker.Value;
                    employee.start = DateTime.Now;

                    if (NumberBox.Text.Length > 0)
                        if (NumberBox.Text.Length <= 13)
                            employee.phone = NumberBox.Text;
                        else
                            throw new Exception("Phone needs to be less 14");
                    else
                        throw new Exception("Phone cannot be empty");

                    if (CityBox.Text.Length <= 50)
                        if (CityBox.Text.Length == 0)
                            employee.city = null;
                        else
                            employee.city = CityBox.Text;
                    else
                        throw new Exception("City needs to be less 51");

                    if (StreetBox.Text.Length <= 50)
                        if (StreetBox.Text.Length == 0)
                            employee.street = null;
                        else
                            employee.street = StreetBox.Text;
                    else
                        throw new Exception("Street needs to be less 51");

                    if (ZipCodeBox.Text.Length <= 50)
                        if (ZipCodeBox.Text.Length == 0)
                            employee.zip = null;
                        else
                            employee.zip = ZipCodeBox.Text;
                    else
                        throw new Exception("ZipCode needs to be less 10");

                    if (PasswordBox.Text.Length < 4)
                        throw new Exception("Password need be more 4 symphols");

                    var empl = _adminrepository.ListOfEmployees();

                    var p = false;
                    while (!p)
                    {
                        p = true;
                        var card_number = RandomString(10);
                        foreach (var t in empl)
                            if (t.id.Equals(card_number))
                            {
                                p = false;
                                break;
                            }
                        employee.id = card_number;
                    }

                    _adminrepository.Add(employee, sha256(PasswordBox.Text));

                    var loginForm = new Employees();
                    Hide();
                    loginForm.ShowDialog();
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

        private void button2_Click(object sender, EventArgs e)
        {
            var loginForm = new Employees();
            Hide();
            loginForm.ShowDialog();
            Close();
        }
    }
}
