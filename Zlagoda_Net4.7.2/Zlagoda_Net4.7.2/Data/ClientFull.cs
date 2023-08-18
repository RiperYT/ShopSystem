using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class ClientFull
    {
        public string Card_Number { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone_Number { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int Percent { get; set; }
        public ClientFull() { }
        public ClientFull(string card_number, string surname, string name, string patronymic, string phone_number, string city, string street, string zipcode, int percent)
        {
            Card_Number = card_number;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Phone_Number = phone_number;
            City = city;
            Street = street;
            ZipCode = zipcode;
            Percent = percent;
        }
    }
}
