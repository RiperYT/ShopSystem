﻿using System;
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
    public partial class Categoies : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public Categoies()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                var categories = _adminrepository.ListOfCategories();
                for (int i = 0; i < categories.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(categories[i].id.ToString(), i);
                    lv.SubItems.Add(categories[i].name);
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

        private void InShopMenu_Click(object sender, EventArgs e)
        {
            var inShop = new InShop();
            Hide();
            inShop.ShowDialog();
            Close();
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                if (ListProducts.Items.Count > 0)
                {
                    try
                    {
                        _adminrepository.DeleteCategory(int.Parse(ListProducts.SelectedItems[0].Text));
                        var loginForm = new Categoies();
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

        private void Add_Click(object sender, EventArgs e)
        {
            var loginForm = new AddCategory();
            Hide();
            loginForm.ShowDialog();
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ListProducts.Items.Count > 0)
            {
                var loginForm = new EditCategory(int.Parse(ListProducts.SelectedItems[0].Text));
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }
    }
}
