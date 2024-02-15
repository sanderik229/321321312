using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _10Prakticheskaya
{
    internal class loginclass
    {
        public string Password;
        public string Username;
        private string NAMEUSED;
        public loginclass()
        {
            LoginPage();
        }
        private void LoginPage()
        {
            Console.Write("Введите логин: ");
            string user_name = Console.ReadLine();
            Username = user_name;
            Console.Write("Введите пароль: ");
            ConsoleKeyInfo a = Console.ReadKey(true);
            if (a.Key == ConsoleKey.Escape) { Console.Clear(); LoginPage(); }
            else
            {
                string pass = "" + a.KeyChar;
                Console.SetCursorPosition(17 + pass.Length - 2, 1);
                Console.Write('*');
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Password = pass;
                        Check();
                        break;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        LoginPage();
                        break;
                    }
                    else
                    {
                        pass += key.KeyChar;
                        Console.SetCursorPosition(17 + pass.Length - 2, 1);
                        Console.Write('*');
                    }
                }
            }
        }
        private void Check()
        {
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            bool Fool = true;
            foreach (user item in users)
            {
                if (item.Login == Username && item.Password == Password)
                {
                    Console.Clear();
                    NAMEUSED = item.Login;
                    if (item.Role == "admin")
                    {
                        admin admin = new admin(NAMEUSED);
                    }
                    Fool = false;
                    break;
                }
            }
            if (Fool)
            {
                Console.WriteLine("\nВы ввели несущетсвуещего пользователя, хотите попробовать еще раз? (y/n)");
                string answ = Console.ReadLine();
                if (answ == "y")
                {
                    Console.Clear();
                    LoginPage();
                }
                else { Console.WriteLine("Хорошо, досвидания!"); }
            }

        }
    }
}