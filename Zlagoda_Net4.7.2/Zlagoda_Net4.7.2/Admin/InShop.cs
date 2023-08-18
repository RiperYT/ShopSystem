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
    public partial class InShop : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public InShop()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfProductsInShop();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].UPC, i);
                    lv.SubItems.Add(products[i].Id.ToString());
                    lv.SubItems.Add(products[i].Product_Name);
                    lv.SubItems.Add(products[i].Product_Price.ToString());
                    lv.SubItems.Add(products[i].Count.ToString());
                    lv.SubItems.Add(products[i].IsPromotional.ToString());
                    lv.SubItems.Add(products[i].UPC_Prom);
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
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfProductsInShopSearch(NameBox.Text);
                ListProducts.Items.Clear();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].UPC, i);
                    lv.SubItems.Add(products[i].Id.ToString());
                    lv.SubItems.Add(products[i].Product_Name);
                    lv.SubItems.Add(products[i].Product_Price.ToString());
                    lv.SubItems.Add(products[i].Count.ToString());
                    lv.SubItems.Add(products[i].IsPromotional.ToString());
                    lv.SubItems.Add(products[i].UPC_Prom);
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
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfProductsInShopProm();
                ListProducts.Items.Clear();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].UPC, i);
                    lv.SubItems.Add(products[i].Id.ToString());
                    lv.SubItems.Add(products[i].Product_Name);
                    lv.SubItems.Add(products[i].Product_Price.ToString());
                    lv.SubItems.Add(products[i].Count.ToString());
                    lv.SubItems.Add(products[i].IsPromotional.ToString());
                    lv.SubItems.Add(products[i].UPC_Prom);
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

        private void AllProductsButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfProductsInShop();
                ListProducts.Items.Clear();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].UPC, i);
                    lv.SubItems.Add(products[i].Id.ToString());
                    lv.SubItems.Add(products[i].Product_Name);
                    lv.SubItems.Add(products[i].Product_Price.ToString());
                    lv.SubItems.Add(products[i].Count.ToString());
                    lv.SubItems.Add(products[i].IsPromotional.ToString());
                    lv.SubItems.Add(products[i].UPC_Prom);
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
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var products = _adminrepository.ListOfProductsInShopNotProm();
                ListProducts.Items.Clear();
                for (int i = 0; i < products.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(products[i].UPC, i);
                    lv.SubItems.Add(products[i].Id.ToString());
                    lv.SubItems.Add(products[i].Product_Name);
                    lv.SubItems.Add(products[i].Product_Price.ToString());
                    lv.SubItems.Add(products[i].Count.ToString());
                    lv.SubItems.Add(products[i].IsPromotional.ToString());
                    lv.SubItems.Add(products[i].UPC_Prom);
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

        private void Add_Click(object sender, EventArgs e)
        {
            var categories = new AddInShop();
            Hide();
            categories.ShowDialog();
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ListProducts.SelectedItems.Count > 0)
            {
                var product = new EditInShop(ListProducts.SelectedItems[0].Text);
                Hide();
                product.ShowDialog();
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
                        _adminrepository.DeleteInShop(ListProducts.SelectedItems[0].Text);
                        var product = new InShop();
                        Hide();
                        product.ShowDialog();
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
