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

namespace Zlagoda_Net4._7._2.Admin
{
    public partial class AddInShop : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        public AddInShop()
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                try
                {
                    var product = new ProductInShop();

                    var list = _adminrepository.ListOfProductsInShop();

                    if (UpcBox.Text.Length == 12)
                            product.UPC = UpcBox.Text;
                    else
                        throw new Exception("UPC need to be 12 lenght");

                    foreach (var item in list)
                        if (item.UPC.Equals(product.UPC))
                            throw new Exception("UPC is not exist");

                    if (UpcPromBox.Text.Length == 12)
                    {
                        foreach (var item in list)
                            if (item.UPC_Prom.Equals(UpcPromBox.Text))
                                throw new Exception("UPC prom is already exist");
                        product.UPC_Prom = UpcPromBox.Text;
                    }
                    else if (UpcPromBox.Text.Length != 0)
                        throw new Exception("UPC prom need to be 12 lenght or empty");

                    var p = false;
                    if (int.TryParse(IdProductBox.Text, out var id))
                    {
                        product.Id = id;
                        var listProducts = _adminrepository.ListOfProducts();
                        foreach (var item in listProducts)
                            if (item.Id.Equals(product.Id))
                            {
                                p = true;
                                break;
                            }
                    }
                    else
                        throw new Exception("Id need to be a number");

                    if (!p)
                        throw new Exception("There is not this id");

                    if (decimal.TryParse(PriceBox.Text, out var price))
                        if (price > 0)
                            product.Product_Price = price;
                        else
                            throw new Exception("Price needs to be more 0");
                    else
                        throw new Exception("Price is not decimal");

                    if (int.TryParse(NumberBox.Text, out var number))
                        if (number > 0)
                            product.Count = number;
                        else
                            throw new Exception("Number needs to be more 0");
                    else
                        throw new Exception("Number is not number");

                    product.IsPromotional = PromotionalBox.Checked;
                    if ((product.IsPromotional && product.UPC_Prom == null) || (!product.IsPromotional && product.UPC_Prom != null))
                        throw new Exception("Need UPC prom");

                    _adminrepository.Add(product);
                    var loginForm = new InShop();
                    Hide();
                    loginForm.ShowDialog();
                    Close();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = ex.Message;
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

        private void button2_Click(object sender, EventArgs e)
        {
            var loginForm = new InShop();
            Hide();
            loginForm.ShowDialog();
            Close();
        }
    }
}
