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
    public partial class EditProduct : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        private readonly int id;
        public EditProduct(int id)
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                this.id = id;
                var list = _adminrepository.ListOfProducts();
                var p = false;
                foreach (var product in list)
                    if (product.Id == id)
                    {
                        CategoryBox.Text = product.Category_Number.ToString();
                        NameBox.Text = product.Product_Name;
                        CharacteristicsBox.Text = product.Characteristics;
                        p = true; break;
                    }

                if (!p)
                {
                    var loginForm = new Products();
                    Hide();
                    loginForm.ShowDialog();
                    Close();
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
                    var product = new Product();

                    if (NameBox.Text.Length > 0)
                        if (NameBox.Text.Length <= 50)
                            product.Product_Name = NameBox.Text;
                        else
                            throw new Exception("Name needs to be less 51");
                    else
                        throw new Exception("Name cannot be empty");

                    if (CharacteristicsBox.Text.Length > 0)
                        if (CharacteristicsBox.Text.Length <= 100)
                            product.Characteristics = CharacteristicsBox.Text;
                        else
                            throw new Exception("Characteristics need to be less 101");
                    else
                        throw new Exception("Characteristics cannot be empty");

                    if (int.TryParse(CategoryBox.Text, out int num))
                    {
                        var categories = _adminrepository.ListOfCategories();
                        var p = false;
                        foreach (var category in categories)
                            if (category.id == num)
                                p = true;
                        if (p)
                        {
                            product.Category_Number = num;
                            product.Id = id;
                            _adminrepository.Update(product);
                            var products = new Products();
                            Hide();
                            products.ShowDialog();
                            Close();
                        }
                        else
                            throw new Exception("There is not category with this number");
                    }
                    else
                        throw new Exception("Category number is not a number");
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
            var products = new Products();
            Hide();
            products.ShowDialog();
            Close();
        }
    }
}
