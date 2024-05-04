using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTask29
{
    internal class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        static List<User> _users = new List<User>();

       
        public static void Login(string userName, string password)
        {

            if (!(string.IsNullOrEmpty(userName)))
            {
                if (!(string.IsNullOrEmpty(password)))
                {
                    foreach (var user in _users)
                    {
                        if (user.UserName == userName && user.Password == password)
                        {
                            if (user.UserName.ToLower() == "admin")
                                user.Role = Role.User;
                            else if (user.UserName.ToLower() == "moderator")
                                user.Role = Role.Moderator;
                            else
                                user.Role = Role.User;

                            Console.WriteLine("You have successfully login");
                        }
                        else
                            throw new("Username or password is incorrect");
                    }
                }
                else
                    throw new("Password is empty");
            }
            else
                throw new("Username is empty");
        }


        public static void Register(string userName, string password)
        {
            bool isUpper = false;
            bool isLower = false;


            if (!(string.IsNullOrEmpty(userName)))
            {
                if (!(string.IsNullOrEmpty(password)))
                {
                    foreach (var user in _users)
                    {
                        if(user.UserName == userName)
                        {
                            throw new ("User is exist");
                        }
                    }
                    if (password.Length >= 8)
                    {
                        foreach (var c in password)
                        {
                            if (Char.IsUpper(c))
                            {
                                isUpper = true;
                            }
                        }
                        foreach (var c in password)
                        {
                            if (Char.IsLower(c))
                            {
                                isLower = true;
                            }
                        }    

                        if (isUpper && isLower)
                        {
                            _users.Add(new User { UserName = userName, Password = password });
                            Console.WriteLine("You have successfully registered");
                        }
                        else
                            throw new ("1 upper and 1 lower case letter is required");
                    }
                    else
                        throw new("Password length should be more than 7");
                }
                else
                    throw new("Password is empty");
            }
            else
                throw new("Username is empty");
        }
    }
}
