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
        public Role Role { get; set; } = Role.User;

        static List<User> _users = new List<User>();

        public User()
        {

        }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            if (userName.ToLower() == "admin")
                Role = Role.Admin;
            else if (userName.ToLower() == "moderator")
                Role = Role.Moderator;
            else
                Role = Role.User;
        }

        public static void Login(string userName, string password)
        {

            if (!(string.IsNullOrEmpty(userName)))
            {
                if (!(string.IsNullOrEmpty(password)))
                {
                    if (_users.Count != 0)
                    {
                        foreach (var user in _users)
                        {
                            if (user.UserName == userName && user.Password == password)
                            {
                                if (user.UserName.ToLower() == "admin")
                                    user.Role = Role.Admin;
                                else if (user.UserName.ToLower() == "moderator")
                                    user.Role = Role.Moderator;
                                else
                                    user.Role = Role.User;

                                Console.WriteLine("you have successfully logged in");
                                return;
                            }

                        }
                        throw new("Username or password is incorrect");

                    }
                    else
                        throw new("Username or password is incorrect");
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
                        if (user.UserName == userName)
                        {
                            throw new("User is exist");
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
                            throw new("1 upper and 1 lower case letter is required");
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
