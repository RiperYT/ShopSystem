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
using Zlagoda_Net4._7._2.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zlagoda_Net4._7._2.Cashier
{
    public partial class AboutMe : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        public AboutMe()
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var me = _cashierRepository.AboutMe(StaticInfo.id);
                IdLabel.Text = me.id;
                Surname.Text = me.surname;
                NameLabel.Text = me.name;
                Patronymic.Text = me.patronymic;
                Salary.Text = me.salary.ToString();
                Birth.Text = me.birth.ToString();
                Start.Text = me.start.ToString();
                Phone.Text = me.phone;
                City.Text = me.city;
                Street.Text = me.street;
                ZipCode.Text = me.zip;
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

        private void ProductsMenuButton_Click(object sender, EventArgs e)
        {
            var inShop = new Products();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void MenuLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void ListProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void InShopMenu_Click(object sender, EventArgs e)
        {
            var inShop = new InShop();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void AllProductsButton_Click(object sender, EventArgs e)
        {
            var addClient = new AddClient();
            addClient.ShowDialog();
        }

        private void EditClientButton_Click(object sender, EventArgs e)
        {
        }

        private void ClientsMenu_Click(object sender, EventArgs e)
        {
            var inShop = new Clients();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void TodayButton_Click(object sender, EventArgs e)
        {
        }

        private void ChecksMenu_Click(object sender, EventArgs e)
        {
            var loginForm = new Checks();
            Hide();
            loginForm.ShowDialog();
            Close();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
