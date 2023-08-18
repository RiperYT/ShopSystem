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

namespace Zlagoda_Net4._7._2.Cashier
{
    public partial class InShop : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        public InShop()
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var products = _cashierRepository.ListOfProductsInShop();
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
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var products = _cashierRepository.ListOfProductsInShopSearch(NameBox.Text);
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
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var products = _cashierRepository.ListOfProductsInShopProm();
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
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var products = _cashierRepository.ListOfProductsInShop();
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
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                var products = _cashierRepository.ListOfProductsInShopNotProm();
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
    }
}
