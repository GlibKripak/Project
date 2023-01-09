using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
namespace Project
{
    public static class Game
    {
        private static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Оберіть один із пунктів меню");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Почати гру");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("2. Реєстрація нового гравця");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. ІсторІя ігор");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("4. Статистика всіх гравців");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("5. Закрити гру");
            Console.ResetColor();
            try
            {
                var choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        ChooseErrors();
                        Menu();
                        break;
                    case 2:
                        Register();
                        Menu();
                        break;
                    case 3:
                        Db.PrintHistory();
                        Menu();
                        break;
                    case 4:
                        Db.PrintStatistic();
                        Menu();
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("I'll be back!"); 
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Такої функції не існує");
                        Console.ResetColor();
                        Menu();
                        break;
                }
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Такої функції не існує");
                Console.ResetColor();
                Menu();
            }
        }
        private static void Register()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Оберіть тип користувача");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Звичайний ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. З подвійним рейтингом");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. Зі зменшеним програшом");
            Console.ResetColor();
            var choose = 0;
            try
            {
                var option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        choose = 1;
                        break;
                    case 2:
                        choose = 2;
                        break;
                    case 3:
                        choose = 3;
                        break;
                    case 0:
                        Console.Clear();
                        return;
                }
            }
            catch
            {
                Console.Clear();
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Реєстрація ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Логін ");
            Console.ResetColor();
            var newUserName = Console.ReadLine();
            if (Db.Users.Any(user => newUserName == user.Name))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Користувач с таким ім'ям вже існує");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Пароль: ");
            Console.ResetColor();
            var newUserPassword = Hash(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Повторіть пароль: ");
            Console.ResetColor();
            var checkPassword = Hash(Console.ReadLine());
            var tries = 3;
            while ((checkPassword == null || checkPassword != newUserPassword) && tries > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Паролі не співпадають (залишилось спроб: {tries}) ");
                Console.ResetColor();
                checkPassword = Hash(Console.ReadLine());
                tries--;
                if (tries != 0) continue;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Спроби закінчились");
                Console.ResetColor();
                return;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Вітаємо у грі");
            Console.ResetColor();
            switch (choose)
            {
                case 1:
                    Db.Users.Add(new Basic(newUserName, newUserPassword));
                    break;
                case 2:
                    Db.Users.Add(new DoubleWin(newUserName, newUserPassword));
                    break;
                case 3:
                    Db.Users.Add(new DoubleLose(newUserName, newUserPassword));
                    break;
            }
        }
        private static Player Login()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Логін:");
                Console.ResetColor();
                var name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Пароль:");
                Console.ResetColor();
                var password = Hash(Console.ReadLine());
                foreach (var user in Db.Users.Where(user => user.Name == name && user.Password == password))
                    return user;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Невірний логін чи пароль");
                Console.ResetColor();
                return null;
            }

            return null;
        }
        private static void ChooseErrors()
        {
            if (Db.Users.Count < 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Зареєстровано меньше 2 гравців");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Перший гравець");
            Console.ResetColor();
            var player1 = Login();
            while (player1 == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Такого користувача не існує");
                Console.ResetColor();
                player1 = Login();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Другий гравець");
            Console.ResetColor();
            var player2 = Login();
            while (player1 == player2 || player2 == null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(player1 == player2 ? "Цей гравець вже у грі" : "Такого користувача не існує");
                Console.ResetColor();
                player2 = Login();
            }
            var game = new Play(player1, player2);
            Console.Clear();
            game.Turns();
            Db.Games.Add(game);
        }
        private static string Hash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
        private static Data Db = new Data();
        static void Main()
        {
            var binFormatter = new BinaryFormatter();
            using (var file = new FileStream("DB.bin", FileMode.OpenOrCreate))
            {
                if (binFormatter.Deserialize(file) is Data newDb)
                    Db = newDb;
            }
            Menu();
            using (var file = new FileStream("DB.bin", FileMode.OpenOrCreate))
            {
                binFormatter.Serialize(file, Db);
            }
        }
    }
}