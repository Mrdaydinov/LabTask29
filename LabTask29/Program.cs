namespace LabTask29
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
            logOrRegLabel:
                Console.WriteLine("Want to register or login:");
                string logOrRegister = Console.ReadLine();
                if (logOrRegister == null)
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
                            Console.WriteLine("Registration (enter username and password):");
                            User.Register(Console.ReadLine().Trim(), Console.ReadLine().Trim());

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
                            Console.WriteLine("Login (enter username and password):");
                            User.Login(Console.ReadLine().Trim(), Console.ReadLine().Trim());

                            // добавить возможность добавления через инпут продуктов и покупателей
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            goto loginLabel;
                        }
                        break;

                    default:
                        Console.WriteLine("Not correct");
                        goto logOrRegLabel;
                }




            }
        }
    }
}
