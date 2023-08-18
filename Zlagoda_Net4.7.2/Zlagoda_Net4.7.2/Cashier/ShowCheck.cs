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
using Zlagoda_Net4._7._2.Data;
using Zlagoda_Net4._7._2.Repositories;

namespace Zlagoda_Net4._7._2.Cashier
{
    public partial class ShowCheck : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        private decimal _total = 0;
        public ShowCheck(string check_number)
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                CardNumber.Text = check_number;
                var list = _cashierRepository.GetSales(check_number);
                _total = 0;
                ListProducts.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(list[i].UPC, i);
                    lv.SubItems.Add(list[i].Number.ToString());
                    lv.SubItems.Add(list[i].Price.ToString());
                    ListProducts.Items.Add(lv);
                    _total += (list[i].Number * list[i].Price);
                }
                TotalCountLabel.Text = _total.ToString();
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

       

        private void CreateCheckButton_Click(object sender, EventArgs e)
        {
            var checks = new Checks();
            Hide();
            checks.ShowDialog();
            Close();
        }
    }
}
