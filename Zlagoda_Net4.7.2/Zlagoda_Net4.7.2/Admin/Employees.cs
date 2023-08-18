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

namespace Zlagoda_Net4._7._2.Admin
{
    public partial class Employees : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public Employees()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfEmployees();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].id, i);
                    lv.SubItems.Add(products[i].surname);
                    lv.SubItems.Add(products[i].name);
                    lv.SubItems.Add(products[i].patronymic);
                    lv.SubItems.Add(products[i].role);
                    lv.SubItems.Add(products[i].salary.ToString());
                    lv.SubItems.Add(products[i].birth.ToString());
                    lv.SubItems.Add(products[i].start.ToString());
                    lv.SubItems.Add(products[i].phone);
                    lv.SubItems.Add(products[i].city);
                    lv.SubItems.Add(products[i].street);
                    lv.SubItems.Add(products[i].zip);
                    ListProducts.Items.Add(lv);
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

        }

        private void ListProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClientsMenu_Click(object sender, EventArgs e)
        {
            var inShop = new Clients();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void ChecksMenu_Click(object sender, EventArgs e)
        {
            var inShop = new Checks();
            Hide();
            inShop.ShowDialog();
            Close();
        }

        private void PromButton_Click(object sender, EventArgs e)
        {

        }

        private void AllProductsButton_Click(object sender, EventArgs e)
        {
            ListProducts.Items.Clear();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfEmployees();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].id, i);
                    lv.SubItems.Add(products[i].surname);
                    lv.SubItems.Add(products[i].name);
                    lv.SubItems.Add(products[i].patronymic);
                    lv.SubItems.Add(products[i].role);
                    lv.SubItems.Add(products[i].salary.ToString());
                    lv.SubItems.Add(products[i].birth.ToString());
                    lv.SubItems.Add(products[i].start.ToString());
                    lv.SubItems.Add(products[i].phone);
                    lv.SubItems.Add(products[i].city);
                    lv.SubItems.Add(products[i].street);
                    lv.SubItems.Add(products[i].zip);
                    ListProducts.Items.Add(lv);
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

        private void NotPromButton_Click(object sender, EventArgs e)
        {

        }

        private void AboutMeMenu_Click(object sender, EventArgs e)
        {
            var loginForm = new AboutMe();
            Hide();
            loginForm.ShowDialog();
            Close();
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            var categories = new Categoies();
            Hide();
            categories.ShowDialog();
            Close();
        }

        private void CashiersButton_Click(object sender, EventArgs e)
        {
            ListProducts.Items.Clear();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfEmployeesCashiers();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].id, i);
                    lv.SubItems.Add(products[i].surname);
                    lv.SubItems.Add(products[i].name);
                    lv.SubItems.Add(products[i].patronymic);
                    lv.SubItems.Add(products[i].role);
                    lv.SubItems.Add(products[i].salary.ToString());
                    lv.SubItems.Add(products[i].birth.ToString());
                    lv.SubItems.Add(products[i].start.ToString());
                    lv.SubItems.Add(products[i].phone);
                    lv.SubItems.Add(products[i].city);
                    lv.SubItems.Add(products[i].street);
                    lv.SubItems.Add(products[i].zip);
                    ListProducts.Items.Add(lv);
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

        private void Add_Click(object sender, EventArgs e)
        {
            var loginForm = new AddEmployee();
            Hide();
            loginForm.ShowDialog();
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ListProducts.SelectedItems.Count > 0)
            {
                var loginForm = new EditEmployee(ListProducts.SelectedItems[0].Text);
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                if (ListProducts.SelectedItems.Count > 0)
                {
                    try
                    {
                        _adminrepository.DeleteEmployee(ListProducts.SelectedItems[0].Text);
                        var loginForm = new Employees();
                        Hide();
                        loginForm.ShowDialog();
                        Close();
                    }
                    catch (Exception ex)
                    {
                        Error.Text = ex.Message; _adminrepository.connection.Close();
                    }
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
    }
}
