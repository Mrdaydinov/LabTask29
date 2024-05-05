using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class Zoomagazin
    {
        public User User { get; set; }
        static List<Product> _products = new List<Product>();

        public Zoomagazin(User user)
        {
            User = user;
        }

        public void AddProduct(List<Product> products)
        {
            if (User.Role == Role.Admin || User.Role == Role.Moderator)
            {
                _products.AddRange(products);
                _products = _products.GroupBy(x => x.Name).Select(x => x.First()).ToList();
                Console.WriteLine("Product added");
            }
            else
                throw new("No access");
        }

        public void UpdateProduct(Product product)
        {
            if (User.Role == Role.Admin || User.Role == Role.Moderator)
            {
                if (_products.Any(p => p.Name == product.Name))
                {
                    var index = _products.IndexOf(_products.FirstOrDefault(p => p.Name == product.Name));
                    _products[index] = product;
                    Console.WriteLine("Product updated");
                }
                else
                    throw new("There is no such product");
            }
            else
                throw new("No access");
        }

        public void DeleteProduct(string product)
        {
            if (User.Role == Role.Admin)
            {
                _products.Remove(_products.First(p => p.Name == product));
                Console.WriteLine("Product deleted");
            }
            else
                throw new("No access");
        }

        public void GetProducts()
        {
            if (_products.Count != 0)
            {
                Console.WriteLine("List of products: ");
                foreach (Product prod in _products)
                {
                    Console.WriteLine($"Name: {prod.Name} | Count in stock: {prod.InStock} | Cost {prod.Cost}");
                }
            }
            else
                Console.WriteLine("There are no products");
        }


        public void DoSale(Customer customer)
        {
        startLabel:
            Console.WriteLine("List of products: ");
            foreach (Product prod in _products)
            {
                Console.WriteLine($"Name: {prod.Name} | Count in stock: {prod.InStock} | Cost {prod.Cost}");
            }

        prodLabel:
            Console.WriteLine("Enter the product name: ");
            string product = Console.ReadLine();

            if (string.IsNullOrEmpty(product) || !(_products.Any(p => p.Name == product))) { Console.WriteLine("Wrong name"); goto prodLabel; }

        countLabel:
            Console.WriteLine("Enter the product count: ");
            bool countCheck = int.TryParse(Console.ReadLine(), out int count);

            if (!countCheck) { Console.WriteLine("Not valid number"); goto countLabel; }
            if (_products.FirstOrDefault(p => p.Name == product).InStock < count) { Console.WriteLine("Not enough product in stock"); goto countLabel; }

            if (customer.Balance >= _products.FirstOrDefault(p => p.Name == product).Cost)
            {
                for (int i = 0; i < count; i++)
                {
                    customer.Products.Add(_products.FirstOrDefault(p => p.Name == product));
                }
                customer.Sales.Add(new Sales(_products.FirstOrDefault(p => p.Name == product)));
                customer.Balance -= _products.FirstOrDefault(p => p.Name == product).Cost * count;
                _products.FirstOrDefault(p => p.Name == product).InStock -= count;
                Console.WriteLine("Product purchased");
            }
            else
                throw new("Balance is not enough");

            optionLabel:
            Console.WriteLine("\nSelect an option: buy another product (type 1), view customer information (type 2)," +
                " view customer sales during the selected period (type 3), end sale (type 0)");


            var optionCheck = int.TryParse(Console.ReadLine(), out int option);
            if (!optionCheck) { Console.WriteLine("Incorrect option"); goto optionLabel; }

            switch (option)
            {
                case 1:
                    goto startLabel;
                case 2:
                    Console.WriteLine("Customer information:");
                    Console.WriteLine($"Name: {customer.Name}, Surname: {customer.Surname}, Balance: {customer.Balance}");
                    Console.WriteLine("Products:");
                    foreach (var prod in customer.Products.GroupBy(p=> p.Name)) { Console.WriteLine($"Name: {prod.Key} - Count: {prod.Count()}"); }
                    goto optionLabel;
                case 3:
                    startDateLabel:
                    Console.WriteLine("Enter start date:");
                    bool startDateCheck = DateTime.TryParse(Console.ReadLine(), out DateTime startDate);
                    if (!startDateCheck) { Console.WriteLine("Incorrect start date"); goto startDateLabel; }

                    endDateLabel:
                    Console.WriteLine("Enter end date:");
                    bool endDateCheck = DateTime.TryParse(Console.ReadLine(), out DateTime endDate);
                    if (!endDateCheck) { Console.WriteLine("Incorrect end date"); goto endDateLabel; }


                    if (customer.Sales.Any(s => s.SaleTime <= endDate && s.SaleTime >= startDate))
                    {
                        foreach (var sale in customer.Sales.Where(s => s.SaleTime <= endDate && s.SaleTime >= startDate))
                        {
                            Console.WriteLine($"Sale Time: {sale.SaleTime}, Product: {sale.SaleProduct.Name}," +
                                $" Count: {customer.Products.Where(p => p.Name == sale.SaleProduct.Name).Count()}");
                        }
                    }
                    else
                        Console.WriteLine("No sales on than period");

                    goto optionLabel;
                case 0:
                    return;
                default:
                    Console.WriteLine("Incorrect option");
                    goto optionLabel;
            }
        }
    }
}