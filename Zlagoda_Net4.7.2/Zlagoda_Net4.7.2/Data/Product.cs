using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class Product
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Category_Name { get; set; }
        public int Category_Number { get; set; }
        public string Characteristics { get; set;}

        public Product() { }
        public Product(int id, string product_name, string category_name, string characteristics)
        {
            Id = id;
            Product_Name = product_name;
            Category_Name = category_name;
            Characteristics = characteristics;
        }
        public Product(int id, string product_name, string category_name, string characteristics, int number)
        {
            Id = id;
            Product_Name = product_name;
            Category_Name = category_name;
            Characteristics = characteristics;
            Category_Number = number;
        }
    }
}
