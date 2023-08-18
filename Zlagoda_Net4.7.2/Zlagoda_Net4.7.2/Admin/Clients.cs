using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zlagoda_Net4._7._2.Cashier;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zlagoda_Net4._7._2.Admin
{
    public partial class Clients : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public Clients()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var clients = _adminrepository.ListOfClients();
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
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {   
                if (PercentBox.Text.Equals(""))
                {
                    var clients = _adminrepository.ListOfClients();
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
                else if (int.TryParse(PercentBox.Text, out var percent))
                {
                    var clients = _adminrepository.ListOfClientsByPercent(percent);
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
                    Error.Text = "Need to be a number";
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
            Hide();
            addClient.ShowDialog();
            Close();
        }

        private void EditClientButton_Click(object sender, EventArgs e)
        {
            if (ListClients.Items.Count > 0)
            {
                var editClient = new EditClient(ListClients.SelectedItems[0].Text);
                Hide();
                editClient.ShowDialog();
                Close();
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

        private void EmployeesButton_Click(object sender, EventArgs e)
        {
            var employees = new Employees();
            Hide();
            employees.ShowDialog();
            Close();
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            var categories = new Categoies();
            Hide();
            categories.ShowDialog();
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    _adminrepository.DeleteClient(ListClients.SelectedItems[0].Text);
                    var loginForm = new Clients();
                    Hide();
                    loginForm.ShowDialog();
                    Close();
                }
                catch (Exception ex){ Error.Text = ex.Message; _adminrepository.connection.Close(); }
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }
    }
}
