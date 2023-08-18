using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zlagoda_Net4._7._2.Data
{
    internal class Employee
    {
        public string id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string role { get; set; }
        public decimal salary { get; set; }
        public DateTime birth { get; set; }
        public DateTime start { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string zip { get; set; }

        public Employee() { }
        public Employee(string id, string surname, string name, string patronymic, string role, decimal salary, DateTime birth, DateTime start, string phone, string city, string street, string zip)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.role = role;
            this.salary = salary;
            this.birth = birth;
            this.start = start;
            this.phone = phone;
            this.city = city;
            this.street = street;
            this.zip = zip;
        }
    }
}
