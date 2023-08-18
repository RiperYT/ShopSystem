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
    public partial class EditCategory : Form
    {
        private AdminRepository _adminrepository = new AdminRepository();
        private readonly int id;
        public EditCategory(int id)
        {
            InitializeComponent();
            if (_adminrepository.IsAdmin(StaticInfo.id, StaticInfo.password))
            {
                this.id = id;
                var list = _adminrepository.ListOfCategories();
                var p = false; 
                foreach (var category in list)
                    if (category.id == id)
                    {
                        p = true;
                        NameBox.Text = category.name;
                        break;
                    }
                if (!p)
                {
                    var loginForm = new Categoies();
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
                    var category = new Category();
                    category.id = id;
                    if (NameBox.Text.Length > 0)
                        if (NameBox.Text.Length <= 50)
                            category.name = NameBox.Text;
                        else
                            throw new Exception("Name needs to be less 51");
                    else
                        throw new Exception("Name cannot be empty");

                    _adminrepository.Update(category);
                    var loginForm = new Categoies();
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
            var loginForm = new Categoies();
            Hide();
            loginForm.ShowDialog();
            Close();
        }
    }
}
