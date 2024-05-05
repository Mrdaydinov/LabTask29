using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte Age { get; set; }
        public decimal Balance { get; set; }

        public List<Product> Products { get; set; }
        public List<Sales> Sales { get; set; }

        public Customer(string name, string surname, byte age, decimal balance)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Balance = balance;
            Products = new List<Product>();
            Sales = new List<Sales>();
        }
    }
}
