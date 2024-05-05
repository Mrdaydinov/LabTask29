using System.Linq.Expressions;

namespace LabTask29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
            logOrRegLabel:
                Console.WriteLine("Want to register or login (enter \"exit\" if you want to exit):");
                string logOrRegister = Console.ReadLine();
                if (string.IsNullOrEmpty(logOrRegister))
                {
                    Console.WriteLine("Choose one option");
                    goto logOrRegLabel;
                }

                switch (logOrRegister.ToLower())
                {
                    case "register":
                    registerLabel:
                        try
                        {
                            Console.WriteLine("\nRegistration (enter username and password):");
                            User.Register(Console.ReadLine(), Console.ReadLine());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            goto registerLabel;
                        }
                        break;

                    case "login":
                    loginLabel:
                        try
                        {
                            Console.WriteLine("\nLogin (enter username and password):");
                            
                            string userName = Console.ReadLine();
                            string password = Console.ReadLine();

                            User.Login(userName, password);
                            User user = new User(userName, password);

                            // добавить возможность добавления через инпут продуктов и покупателей

                            Zoomagazin zoomagazin = new Zoomagazin(user);

                            Console.WriteLine("\nWelcome to Zoomagazin");
                        optionSelectLabel:
                            Console.WriteLine("\nSelect an option:\nSee all products (type 1) " +
                                "| Add products (type 2) | Update product (type 3) | Delete product (type 4) | Do sale (type 5)\t(or enter 0 for logout)");

                            bool optionCheck = int.TryParse(Console.ReadLine(), out int option);
                            if (!optionCheck)
                            {
                                Console.WriteLine("\nIncorrect");
                                goto optionSelectLabel;
                            }

                            switch (option)
                            {
                                case 1:
                                    try
                                    {
                                        zoomagazin.GetProducts();
                                        goto optionSelectLabel;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                    break;
                                case 2:
                                    try
                                    {
                                        List<Product> products = new List<Product>();

                                        do
                                        {
                                            Console.WriteLine("\nEnter the name of the new product (Name, Cost (numeric), Count in stock (numeric)):");
                                            products.Add(new Product(Console.ReadLine(), decimal.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));

                                            Console.WriteLine("\nType \"add\" or \"done\" if u end:");
                                        } while (Console.ReadLine().ToLower() != "done");

                                        zoomagazin.AddProduct(products);
                                        goto optionSelectLabel;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        goto optionSelectLabel;
                                    }
                                case 3:
                                    try
                                    {
                                        Console.WriteLine("\nEnter the new values of the product (Name, Cost (numeric), Count in stock (numeric)) you want to update:");
                                        zoomagazin.UpdateProduct(new Product(Console.ReadLine(), decimal.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
                                        goto optionSelectLabel;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        goto optionSelectLabel;
                                    }
                                case 4:
                                    try
                                    {
                                        Console.WriteLine("\nEnter the product name:");
                                        zoomagazin.DeleteProduct(Console.ReadLine());
                                        goto optionSelectLabel;
                                    }
                                    catch (Exception e)
                                    {
                                        if (e is InvalidOperationException)
                                            Console.WriteLine("\nThere is no such product");
                                        else
                                            Console.WriteLine(e.Message);
                                        goto optionSelectLabel;
                                    }
                                case 5:
                                    try
                                    {
                                        Console.WriteLine("Enter the customer info (type Name, Surname, Age, Balance)");
                                        nameLabel:
                                        string name = Console.ReadLine();
                                        if(string.IsNullOrEmpty(name))
                                        {
                                            Console.WriteLine("Name is empty");
                                            goto nameLabel;
                                        }
                                        surNameLabel:
                                        string surname = Console.ReadLine();
                                        if (string.IsNullOrEmpty(surname))
                                        {
                                            Console.WriteLine("Surname is empty");
                                            goto surNameLabel;
                                        }
                                        ageLabel:
                                        var ageCheck = byte.TryParse(Console.ReadLine(), out byte age);
                                        if (!ageCheck)
                                        {
                                            Console.WriteLine("Age is incorrect");
                                            goto ageLabel;
                                        }
                                        balanceLabel:
                                        var balanceCheck = decimal.TryParse(Console.ReadLine(), out decimal balance);
                                        if (!balanceCheck)
                                        {
                                            Console.WriteLine("Balance is incorrect");
                                            goto balanceLabel;
                                        }
                                            
                                        zoomagazin.DoSale(new Customer(name, surname, age, balance));
                                        goto optionSelectLabel;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        goto optionSelectLabel;
                                    }
                                case 0:
                                    goto logOrRegLabel;
                                default:
                                    Console.WriteLine("\nIncorrect");
                                    goto optionSelectLabel;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            returnLabel:
                            Console.WriteLine("\nReturn to login page or main page (type login or main)?");
                            string option = Console.ReadLine();
                            if (string.IsNullOrEmpty(option))
                                goto returnLabel;
                            switch (option)
                            {
                                case "login":
                                    goto loginLabel;
                                case "main":
                                    goto logOrRegLabel;
                                default:
                                    Console.WriteLine("\nIncorrect");
                                    goto returnLabel;
                            }
                        }
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nNot correct");
                        goto logOrRegLabel;
                }
            }
        }
    }
}
