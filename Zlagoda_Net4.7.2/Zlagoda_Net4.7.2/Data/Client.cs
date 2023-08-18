using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class Client
    {
        public string Card_Number { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Phone_Number { get; set; }
        public int Percent { get; set; }
        public Client(string card_number, string surname, string name, string phone_number, int percent)
        {
            Card_Number = card_number;
            Surname = surname;
            Name = name;
            Phone_Number = phone_number;
            Percent = percent;
        }
    }
}
