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

namespace Zlagoda_Net4._7._2.Admin
{
    public partial class Checks : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public Checks()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                FromDate.MaxDate = DateTime.Now;
                ToDate.MaxDate = DateTime.Now;
                var checks = _adminrepository.GetChecksDay(StaticInfo.id);
                for (int i = 0; i < checks.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(checks[i].check_number, i);
                    lv.SubItems.Add(checks[i].card_number);
                    lv.SubItems.Add(checks[i].print_date.ToString());
                    lv.SubItems.Add(checks[i].sum_total.ToString());
                    lv.SubItems.Add(checks[i].vat.ToString());
                    ListChecks.Items.Add(lv);
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

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var from = FromDate.Value;
                    var to = ToDate.Value;


                    if (from > to)
                        throw new Exception("From need to be less then to");

                    if (IdCashierBox.Text.Equals(""))
                    {
                        ListChecks.Items.Clear();
                        var checks = _adminrepository.GetChecksPeriod(from, to);
                        for (int i = 0; i < checks.Count; i++)
                        {
                            ListViewItem lv = new ListViewItem(checks[i].check_number, i);
                            lv.SubItems.Add(checks[i].card_number);
                            lv.SubItems.Add(checks[i].print_date.ToString());
                            lv.SubItems.Add(checks[i].sum_total.ToString());
                            lv.SubItems.Add(checks[i].vat.ToString());
                            ListChecks.Items.Add(lv);
                        }
                    }
                    else
                    {
                        var list = _adminrepository.ListOfEmployees();
                        var p = false;
                        foreach (var employee in list)
                            if (employee.id.Equals(IdCashierBox.Text))
                            {
                                p = true;
                                break;
                            }

                        if (!p)
                            throw new Exception("Id is not correct");


                        ListChecks.Items.Clear();
                        var checks = _adminrepository.GetChecksPeriodIdEmployee(IdCashierBox.Text, from, to);
                        for (int i = 0; i < checks.Count; i++)
                        {
                            ListViewItem lv = new ListViewItem(checks[i].check_number, i);
                            lv.SubItems.Add(checks[i].card_number);
                            lv.SubItems.Add(checks[i].print_date.ToString());
                            lv.SubItems.Add(checks[i].sum_total.ToString());
                            lv.SubItems.Add(checks[i].vat.ToString());
                            ListChecks.Items.Add(lv);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.Text = ex.Message;
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
        }

        private void EditClientButton_Click(object sender, EventArgs e)
        {
            if (ListChecks.Items.Count > 0)
            {
                var addCheck = new ShowCheck(ListChecks.SelectedItems[0].Text);
                Hide();
                addCheck.ShowDialog();
                Close();
            }
        }

        private void ClientsMenu_Click(object sender, EventArgs e)
        {
            var inShop = new Clients();
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

        private void AllSearchButton_Click(object sender, EventArgs e)
        {

        }

        private void SumCashierButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var from = FromDate.Value;
                    var to = ToDate.Value;

                    if (from > to)
                        throw new Exception("From need to be less then to");
                    if (IdCashierBox.Text.Equals(""))
                    {
                        throw new Exception($"Sum = {_adminrepository.Sum(from, to)}");
                    }
                    else
                    {
                        var list = _adminrepository.ListOfEmployees();
                        var p = false;
                        foreach (var employee in list)
                            if (employee.id.Equals(IdCashierBox.Text))
                            {
                                p = true;
                                break;
                            }

                        if (!p)
                            throw new Exception("Id is not correct");

                        throw new Exception($"Sum = {_adminrepository.SumEmployee(IdCashierBox.Text, from, to)}");
                    }
                }
                catch (Exception ex)
                {
                    Error.Text = ex.Message;
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

        private void AmountProductBox_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var from = FromDate.Value;
                    var to = ToDate.Value;

                    if (from > to)
                        throw new Exception("From need to be less then to");
                    
                    var list = _adminrepository.ListOfProductsInShop();
                    var p = false;

                    foreach (var product in list)
                        if (product.UPC.Equals(UpcBox.Text))
                        {
                            p = true;
                            break;
                        }

                    if (!p)
                        throw new Exception("Upc is not correct");

                    throw new Exception($"Number = {_adminrepository.SumNumber(UpcBox.Text, from, to)}");
                }
                catch (Exception ex)
                {
                    Error.Text = ex.Message;
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                if (ListChecks.Items.Count > 0)
                {
                    try
                    {
                        _adminrepository.DeleteCheck(ListChecks.SelectedItems[0].Text);

                        var loginForm = new Checks();
                        Hide();
                        loginForm.ShowDialog();
                        Close();
                    }
                    catch (Exception ex)
                    { Error.Text = ex.Message; _adminrepository.connection.Close(); }
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
