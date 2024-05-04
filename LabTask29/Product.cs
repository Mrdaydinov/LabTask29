using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int InStock { get; set; }

        public Product(string name, decimal cost, int inStock)
        {
            Name = name;
            Cost = cost;
            InStock = inStock;
        }
    }
}
