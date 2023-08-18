using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class Check
    {
        public string check_number { get; set; }
        public string card_number { get; set; }
        public DateTime print_date { get; set; }
        public decimal sum_total { get; set; }
        public decimal vat { get; set; }
        public Check(string check_number, DateTime print_date, decimal sum_total, decimal vat)
        {
            this.check_number = check_number;
            this.print_date = print_date;
            this.sum_total = sum_total;
            this.vat = vat;
        }
        public Check(string check_number, string card_number, DateTime print_date, decimal sum_total, decimal vat)
        {
            this.check_number = check_number;
            this.card_number = card_number;
            this.print_date = print_date;
            this.sum_total = sum_total;
            this.vat = vat;
        }
    }
}
