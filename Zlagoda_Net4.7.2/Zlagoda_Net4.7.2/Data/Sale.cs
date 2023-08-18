using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class Sale
    {
        public string UPC { get; set; }
        public string CheckNumber { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public Sale() { }
        public Sale(string uPC, string checkNumber, int number, decimal price)
        {
            UPC = uPC;
            CheckNumber = checkNumber;
            Number = number;
            Price = price;
        }
    }
}
