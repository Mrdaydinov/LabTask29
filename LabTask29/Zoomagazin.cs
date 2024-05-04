using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class Zoomagazin
    {
        public User User { get; set; }
        List<Product> _products;

        public Zoomagazin(List<Product> products, User user)
        {
            User = user;
            _products = products;
        }

        public void AddProduct(User user, Product product)
        {
            if (user.Role == Role.Admin || user.Role == Role.Moderator)
            {
                if (!_products.Contains(product))
                {
                    _products.Add(product);
                }
                else
                    throw new("Product exist");
            }
            else
                throw new("No access");
        }

        public void UpdateProduct(User user, Product product)
        {
            if (user.Role == Role.Admin || user.Role == Role.Moderator)
            {
                if (_products.Contains(product))
                {
                    _products.IndexOf(_products.Where(p => p.Name == product.Name).FirstOrDefault());
                }
            }
            else
                throw new("No access");
        }

        public void DeleteProduct(User user, Product product)
        {
            if (user.Role == Role.Admin)
            {
                _products.Remove(product);
            }
        }


        public void DoSale(Customer customer)
        {
            foreach (Product prod in _products)
            {
                Console.WriteLine("List of products: ");
                Console.Write($"Name: {prod.Name} | Count in stock: {prod.InStock} | Cost {prod.Cost}");
            }

        prodLabel:
            Console.WriteLine("Enter the product name: ");
            string product = Console.ReadLine();

            if (product == null || !(_products.Any(p => p.Name == product))) { Console.WriteLine("Wrong name"); goto prodLabel; }

        countLabel:
            Console.WriteLine("Enter the product count: ");
            bool countCheck = int.TryParse(Console.ReadLine(), out int count);

            if(!countCheck) { Console.WriteLine("Not valid number"); goto countLabel; }
            if(_products.FirstOrDefault(p=>p.Name == product).InStock < count) { Console.WriteLine("Not enough product in stock"); goto countLabel; }

            if (customer.Balance >= _products.FirstOrDefault(p=>p.Name == product).Cost)
            {
                for (int i = 0; i < count; i++)
                {
                    customer.Products.Add(_products.FirstOrDefault(p=> p.Name == product));
                }
                customer.Sales.Add(new Sales(_products.FirstOrDefault(p=> p.Name == product)));
                customer.Balance -= _products.FirstOrDefault(p=>p.Name == product).Cost;
                _products.FirstOrDefault(p=>p.Name == product).InStock -= count;
            }
            else
                throw new("Balance is not enough");
        }
    }
}
