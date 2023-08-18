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
    public partial class AddCheck : Form
    {
        private CashierRepository _cashierRepository = new CashierRepository();
        private List<Sale> _saleList = new List<Sale>();
        private decimal _total = 0;
        public AddCheck()
        {
            InitializeComponent();
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
            }
            else
            {
                var loginForm = new LoginForm();
                Hide();
                loginForm.ShowDialog();
                Close();
            }
        }

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var upc = UPCBox.Text;
                    var countStr = NumberBox.Text;

                    foreach (var sale in _saleList)
                        if (sale.UPC.Equals(upc))
                            throw new Exception("There is product with this UPC");

                    if (int.TryParse(countStr, out int count))
                    {
                        if(count  <= 0)
                            throw new Exception("Number needs to be more 0");

                        var list = _cashierRepository.ListOfProductsInShop();

                        var p = false;
                        foreach (var product in list)
                            if (product.UPC.Equals(upc))
                            {
                                p = true;
                                if (count > product.Count)
                                    throw new Exception("Number more than number of products in shop");
                                _saleList.Add(new Sale() { UPC = product.UPC, Price = product.Product_Price, Number = count });
                                UpdateList();
                                ErrorProduct.Text = "";
                                UPCBox.Text = "";
                                NumberBox.Text = "";
                                break;
                            }

                        if (!p)
                            throw new Exception("There is no product with this UPC");
                    }
                    else
                    {
                        throw new Exception("Number needs to be a number");
                    }
                }
                catch (Exception ex)
                {
                    ErrorProduct.Text = ex.Message;
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
        private void UpdateList()
        {
            _total = 0;
            ListProducts.Items.Clear();
            for (int i = 0; i < _saleList.Count; i++)
            {
                ListViewItem lv = new ListViewItem(_saleList[i].UPC, i);
                lv.SubItems.Add(_saleList[i].Number.ToString());
                lv.SubItems.Add(_saleList[i].Price.ToString());
                ListProducts.Items.Add(lv);
                _total += (_saleList[i].Number * _saleList[i].Price);
            }
            TotalCountLabel.Text = _total.ToString();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ListProducts.Items.Count > 0)
            {
                _saleList.RemoveAt(ListProducts.SelectedItems[0].Index);
                UpdateList();
            }
        }

        private void CreateCheckButton_Click(object sender, EventArgs e)
        {
            if (_cashierRepository.IsCashier(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    if (_saleList.Count == 0)
                        throw new Exception("There are no products");
                    if (!CardNumberBox.Text.Equals(""))
                    {
                        var list = _cashierRepository.ListOfClients();
                        var p = false;
                        foreach(var client in list)
                            if (client.Card_Number == CardNumberBox.Text)
                                p = true;
                        if (!p)
                            throw new Exception("There is no client with this card number");
                    }

                    var listChecks = _cashierRepository.ListOfChecks();
                    var p1 = false;
                    var check_number = "";
                    while (!p1)
                    {
                        check_number = RandomString(10);
                        foreach (var check in listChecks)
                            if (!check_number.Equals(check.check_number))
                            {
                                p1 = true;
                                break;
                            }
                    }

                    var checkEnd = new Check(check_number, DateTime.Now, _total, (_total / 5));
                    if (!CardNumberBox.Text.Equals(""))
                        checkEnd.card_number = CardNumberBox.Text;

                    _cashierRepository.AddCheck(checkEnd);

                    foreach (var product in _saleList)
                    {
                        product.CheckNumber = check_number;
                        _cashierRepository.AddSale(product);
                        _cashierRepository.UpdateCountProduct(product.UPC, product.Number);
                    }

                    var checks = new Checks();
                    Hide();
                    checks.ShowDialog();
                    Close();
                }
                catch (Exception ex)
                {
                    ErrorCheck.Text = ex.Message;
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

        public string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            var checks = new Checks();
            Hide();
            checks.ShowDialog();
            Close();
        }
    }
}
