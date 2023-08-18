using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class ProductInShop
    {
        public string UPC { get; set; }
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public int Count { get; set; }
        public bool IsPromotional { get; set; }
        public string UPC_Prom { get; set; }

        public ProductInShop() { }
        public ProductInShop(string upc, int id, string product_name, decimal product_price, int count, bool isPromotional, string upc_prom)
        {
            UPC = upc;
            Id = id;
            Product_Name = product_name;
            Product_Price = product_price;
            Count = count;
            IsPromotional = isPromotional;
            UPC_Prom = upc_prom;
        }
    }
}
