using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class Sales
    {
        public static int SaleId { get; set; } = 0;
        public DateTime SaleTime { get; set; } = DateTime.Now;
        public Product SaleProduct { get; set; }

        public Sales(Product product)
        {
            SaleId++;
            SaleProduct = product;
        }
    }
}
