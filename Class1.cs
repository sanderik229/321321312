using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _10Prakticheskaya
{
    internal class admin : CRUD
    {
        private int _pos;
        private string _name;
        private bool _isError;

        public admin(string AdminName)
        {
            _name = AdminName;
            Read();
        }
        private void MenuShow()
        {
            Console.Clear();
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            int y = 0;
            foreach (var item in users)
            {
                Console.SetCursorPosition(3, y);
                Console.Write(item.Login);
                Console.SetCursorPosition(25, y);
                Console.Write(item.Role);
                Console.SetCursorPosition(45, y);
                Console.Write(item.ID);
                y += 1;
            }
            Console.SetCursorPosition(75, 0);
            Console.Write("F1 - Добавление");
            Console.SetCursorPosition(75, 1);
            Console.Write("F2 - Удаление");
            Console.SetCursorPosition(75, 2);
            Console.Write("F3 - Фильтр поиска");
            Console.SetCursorPosition(75, 5);
            Console.Write("Вы зашли под логином: " + _name);
            Console.SetCursorPosition(3, users.Count + 3);
            Console.Write("Логин");
            Console.SetCursorPosition(25, users.Count + 3);
            Console.Write("Роль");
            Console.SetCursorPosition(45, users.Count + 3);
            Console.Write("ID");
        }
        public void Create()
        {
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            Console.Clear();
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            Console.Write("Введите уникальный ID: ");
            string ID = Console.ReadLine();
            Console.Write("Введите роль: ");
            string Role = Console.ReadLine();
            user new_user = new user(name, password, Role, ID);
            users.Add(new_user);
            JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users);
        }
        public void Read()
        {
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            Console.Clear();
            MenuShow();
            int pos = 0;
            Console.SetCursorPosition(0, pos);
            Console.Write("->");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, pos);
                    Console.Write("  ");
                    pos -= 1;
                    if (pos > users.Count - 1) { pos = users.Count - 1; }
                    if (pos < 0) { pos = 0; }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, pos);
                    Console.Write("  ");
                    pos += 1;
                    if (pos > users.Count - 1) { pos = users.Count - 1; }
                    if (pos < 0) { pos = 0; }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    _pos = pos;
                    Update();
                    Console.Clear();
                    users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
                    MenuShow();

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users);
                    loginclass login = new loginclass();
                    break;
                }
                else if (key.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Create();
                    Console.Clear();

                    users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.F2)
                {
                    _pos = pos;
                    Delete();
                    Console.Clear();
                    if (_isError)
                    {
                        Console.SetCursorPosition(75, 7);
                        Console.WriteLine("Вы не можете удалить активный профиль!");
                        _isError = false;
                        Console.SetCursorPosition(0, 0);
                    }
                    else
                    {
                        Console.Clear();
                    }

                    users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.F3)
                {
                    Console.Clear();
                    Console.Write("Введите TAG по которому искать пользователей(это может быть login, роль или ID): ");
                    string findableItem = Console.ReadLine();
                    FindByItem(findableItem);
                }
                Console.SetCursorPosition(0, pos);
                Console.Write("->");
            }

        }

        public void Delete()
        {
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            if (users[_pos].Login != _name)
            {
                users.Remove(users[_pos]);
                JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users);
            }
            else
            {
                _isError = true;
            }
        }
        public void Update()
        {
            Console.Clear();
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            Console.WriteLine($"   ID: {users[_pos].ID}\n   Login: {users[_pos].Login}\n   Password: {users[_pos].Password}\n   Role: {users[_pos].Role}");
            int pos = 0;
            Console.SetCursorPosition(0, pos);
            Console.Write("->");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, pos);
                    Console.Write("  ");
                    pos -= 1;
                    if (pos > 3) { pos = 3; }
                    if (pos < 0) { pos = 0; }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, pos);
                    Console.Write("  ");
                    pos += 1;
                    if (pos > 3) { pos = 3; }
                    if (pos < 0) { pos = 0; }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (pos == 0)
                    {
                        Console.SetCursorPosition(7, 0);
                        Console.Write("                             ");
                        Console.SetCursorPosition(7, 0);
                        string new_ID = Console.ReadLine();
                        users[_pos].ID = new_ID;
                    }
                    else if (pos == 1)
                    {
                        Console.SetCursorPosition(10, 1);
                        Console.Write("                             ");
                        Console.SetCursorPosition(10, 1);
                        string new_login = Console.ReadLine();
                        users[_pos].Login = new_login;
                    }
                    else if (pos == 2)
                    {
                        Console.SetCursorPosition(13, 2);
                        Console.Write("                             ");
                        Console.SetCursorPosition(13, 2);
                        string new_password = Console.ReadLine();
                        users[_pos].Password = new_password;
                    }
                    else if (pos == 3)
                    {
                        Console.SetCursorPosition(9, 3);
                        Console.Write("                             ");
                        Console.SetCursorPosition(9, 3);
                        string new_role = Console.ReadLine();
                        users[_pos].Role = new_role;
                    }
                    Console.Clear();
                    Console.WriteLine($"   ID: {users[_pos].ID}\n   Login: {users[_pos].Login}\n   Password: {users[_pos].Password}\n   Role: {users[_pos].Role}");
                }
                else if (key.Key == ConsoleKey.Escape) { JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users); Console.Clear(); break; }
                Console.SetCursorPosition(0, pos);
                Console.Write("->");
            }
            JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users);
        }
        private void FindByItem(string foundableItem)
        {
            Console.Clear();
            List<user> users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
            List<user> sorted_users = new List<user>();
            foreach (user user in users)
            {
                if (foundableItem.Equals(user.ID) || foundableItem.Equals(user.Role) || foundableItem.Equals(user.Login))
                {
                    sorted_users.Add(user);
                }
            }
            int y = 0;
            foreach (var item in sorted_users)
            {
                Console.SetCursorPosition(3, y);
                Console.Write(item.Login);
                Console.SetCursorPosition(25, y);
                Console.Write(item.Role);
                Console.SetCursorPosition(45, y);
                Console.Write(item.ID);
                y += 1;
            }
            y = 0;
            Console.SetCursorPosition(75, 0);
            Console.Write("F1 - Добавление");
            Console.SetCursorPosition(75, 1);
            Console.Write("F2 - Удаление");
            Console.SetCursorPosition(75, 3);
            Console.Write("F3 - Фильтр поиска");
            Console.SetCursorPosition(75, 4);
            Console.Write("F4 - Сбросить фильтр поиска");
            Console.SetCursorPosition(75, 7);
            Console.Write("Вы зашли под логином: " + _name);
            Console.SetCursorPosition(3, sorted_users.Count + 3);
            Console.Write("Логин");
            Console.SetCursorPosition(25, sorted_users.Count + 3);
            Console.Write("Роль");
            Console.SetCursorPosition(45, sorted_users.Count + 3);
            Console.Write("ID");
            int position = 0;
            Console.SetCursorPosition(0, position);
            Console.Write("->");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.Write("  ");
                    position -= 1;
                    if (position > sorted_users.Count - 1) { position = sorted_users.Count - 1; }
                    if (position < 0) { position = 0; }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.Write("  ");
                    position += 1;
                    if (position > sorted_users.Count - 1) { position = sorted_users.Count - 1; }
                    if (position < 0) { position = 0; }
                }
                else if (key.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Create();
                    Console.Clear();
                    y = 0;
                    foreach (var item in sorted_users)
                    {
                        Console.SetCursorPosition(3, y);
                        Console.Write(item.Login);
                        Console.SetCursorPosition(25, y);
                        Console.Write(item.Role);
                        Console.SetCursorPosition(45, y);
                        Console.Write(item.ID);
                        y += 1;
                    }
                    y = 0;
                    Console.SetCursorPosition(75, 0);
                    Console.Write("F1 - Добавление");
                    Console.SetCursorPosition(75, 1);
                    Console.Write("F2 - Удаление");
                    Console.SetCursorPosition(75, 3);
                    Console.Write("F3 - Фильтр поиска");
                    Console.SetCursorPosition(75, 4);
                    Console.Write("F4 - Сбросить фильтр поиска");
                    Console.SetCursorPosition(75, 7);
                    Console.Write("Вы зашли под логином: " + _name);
                    Console.SetCursorPosition(3, sorted_users.Count + 3);
                    Console.Write("Логин");
                    Console.SetCursorPosition(25, sorted_users.Count + 3);
                    Console.Write("Роль");
                    Console.SetCursorPosition(45, sorted_users.Count + 3);
                    Console.Write("ID");

                }
                else if (key.Key == ConsoleKey.F2)
                {
                    int index = users.IndexOf(sorted_users[position]);
                    users.RemoveAt(index);
                    sorted_users.Remove(sorted_users[position]);
                    JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", users);
                    Console.Clear();
                    y = 0;
                    foreach (var item in sorted_users)
                    {
                        Console.SetCursorPosition(3, y);
                        Console.Write(item.Login);
                        Console.SetCursorPosition(25, y);
                        Console.Write(item.Role);
                        Console.SetCursorPosition(45, y);
                        Console.Write(item.ID);
                        y += 1;
                    }
                    y = 0;
                    Console.SetCursorPosition(75, 0);
                    Console.Write("F1 - Добавление");
                    Console.SetCursorPosition(75, 1);
                    Console.Write("F2 - Удаление");
                    Console.SetCursorPosition(75, 3);
                    Console.Write("F3 - Фильтр поиска");
                    Console.SetCursorPosition(75, 4);
                    Console.Write("F4 - Сбросить фильтр поиска");
                    Console.SetCursorPosition(75, 7);
                    Console.Write("Вы зашли под логином: " + _name);
                    Console.SetCursorPosition(3, sorted_users.Count + 3);
                    Console.Write("Логин");
                    Console.SetCursorPosition(25, sorted_users.Count + 3);
                    Console.Write("Роль");
                    Console.SetCursorPosition(45, sorted_users.Count + 3);
                    Console.Write("ID");
                }
                else if (key.Key == ConsoleKey.F3)
                {
                    Console.Clear();
                    Console.Write("Введите TAG по которому искать пользователей(это может быть login, роль или ID): ");
                    string findableItem = Console.ReadLine();
                    FindByItem(findableItem);
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    int index = users.IndexOf(sorted_users[position]);
                    _pos = index;
                    Update();
                    users = JSONConverter.JSONDeserializer<List<user>>("C:\\Users\\Public\\Documents\\users.json");
                    sorted_users = new List<user>();
                    foreach (user user in users)
                    {
                        if (foundableItem.Equals(user.ID) || foundableItem.Equals(user.Role) || foundableItem.Equals(user.Login))
                        {
                            sorted_users.Add(user);
                        }
                    }
                    Console.Clear();
                    y = 0;
                    foreach (var item in sorted_users)
                    {
                        Console.SetCursorPosition(3, y);
                        Console.Write(item.Login);
                        Console.SetCursorPosition(25, y);
                        Console.Write(item.Role);
                        Console.SetCursorPosition(45, y);
                        Console.Write(item.ID);
                        y += 1;
                    }
                    y = 0;
                    Console.SetCursorPosition(75, 0);
                    Console.Write("F1 - Добавление");
                    Console.SetCursorPosition(75, 1);
                    Console.Write("F2 - Удаление");
                    Console.SetCursorPosition(75, 3);
                    Console.Write("F3 - Фильтр поиска");
                    Console.SetCursorPosition(75, 4);
                    Console.Write("F4 - Сбросить фильтр поиска");
                    Console.SetCursorPosition(75, 7);
                    Console.Write("Вы зашли под логином: " + _name);
                    Console.SetCursorPosition(3, sorted_users.Count + 3);
                    Console.Write("Логин");
                    Console.SetCursorPosition(25, sorted_users.Count + 3);
                    Console.Write("Роль");
                    Console.SetCursorPosition(45, sorted_users.Count + 3);
                    Console.Write("ID");
                }
                else if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.F4)
                {
                    Console.Clear();
                    Read();
                    break;
                }
                Console.SetCursorPosition(0, position);
                Console.Write("->");
            }
        }
    }
}