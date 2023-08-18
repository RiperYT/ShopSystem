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
    public partial class Clients : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        public Clients()
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var clients = _cashierRepository.ListOfClients();
                for (int i = 0; i < clients.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(clients[i].Card_Number, i);
                    lv.SubItems.Add(clients[i].Surname);
                    lv.SubItems.Add(clients[i].Name);
                    lv.SubItems.Add(clients[i].Phone_Number);
                    lv.SubItems.Add(clients[i].Percent.ToString());
                    ListClients.Items.Add(lv);
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var clients = _cashierRepository.ListOfClientsBySurname(NameBox.Text);
                ListClients.Items.Clear();
                for (int i = 0; i < clients.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(clients[i].Card_Number, i);
                    lv.SubItems.Add(clients[i].Surname);
                    lv.SubItems.Add(clients[i].Name);
                    lv.SubItems.Add(clients[i].Phone_Number);
                    lv.SubItems.Add(clients[i].Percent.ToString());
                    ListClients.Items.Add(lv);
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
            if (ListClients.Items.Count > 0)
            {
                var editClient = new EditClient(ListClients.SelectedItems[0].Text);
                editClient.ShowDialog();
            }
        }

        private void ChecksMenu_Click(object sender, EventArgs e)
        {
            var inShop = new Checks();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void AboutMeMenu_Click(object sender, EventArgs e)
        {
            var loginForm = new AboutMe();
            Hide();
            loginForm.ShowDialog();
            Close();
        }
    }
}
