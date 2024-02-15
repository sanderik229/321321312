using System.Text.Json.Serialization;

namespace _10Prakticheskaya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("C:\\Users\\Public\\Documents\\users.json"))
            {
                loginclass login = new loginclass();
            }
            else
            {
                File.Create("C:\\Users\\Public\\Documents\\users.json").Close();
                user admin = new user("admin", "admin", "admin", "1");
                List<user> usrs = new List<user>();
                usrs.Add(admin);
                JSONConverter.JSONSerializer("C:\\Users\\Public\\Documents\\users.json", usrs);
                loginclass login = new loginclass();
            }
        }
    }
}