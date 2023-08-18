using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Repositories;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Zlagoda_Net4._7._2.Cashier
{
    public partial class Checks : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        public Checks()
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                FromDate.MaxDate = DateTime.Now;
                ToDate.MaxDate = DateTime.Now;
                var checks = _cashierRepository.GetChecksDay(StaticInfo.id);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var from = FromDate.Value;
                    var to = ToDate.Value;

                    if (from > to)
                        throw new Exception("From need to be less then to");

                    ListChecks.Items.Clear();
                    var checks = _cashierRepository.GetChecksPeriod(StaticInfo.id, from, to);
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
            var addCheck = new AddCheck();
            Hide();
            addCheck.ShowDialog();
            Close();
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

        private void TodayButton_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                ListChecks.Items.Clear();
                var checks = _cashierRepository.GetChecksDay(StaticInfo.id);
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

        private void AboutMeMenu_Click(object sender, EventArgs e)
        {
            var loginForm = new AboutMe();
            Hide();
            loginForm.ShowDialog();
            Close();
        }
    }
}
