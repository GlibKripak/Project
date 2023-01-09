namespace Project
{
    [Serializable]
    public class Data
    {
        public List<Player> Users { get; set; }
        public List<Play> Games { get; set; }

        public Data()
        {
            Users = new List<Player>();
            Games = new List<Play>();
        }
        public void PrintHistory()
        {
            var gameCount = 1;
            if (HistoryError()) return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Історія ігор");
            Console.ResetColor();
            foreach (var game in Games)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"№{gameCount} X: {game.P1.Name} O: {game.P2.Name}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                if (game.WinCondition("X") || game.WinCondition("O"))
                    Console.WriteLine($"Переможець : {game.Winner.Name} Програвший : {game.Looser.Name}");
                else
                {
                    Console.WriteLine("Нічия");
                }
                Console.ResetColor();
                if (game == Games[Games.Count - 1])
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("----------");
                    Console.ResetColor();
                    return;
                }
                gameCount++;
            }
        }
        public void PrintStatistic()
        {
            if (StatisticError()) return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Статистика гравців");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var user in Users)
                Console.WriteLine($"Гравець: {user.Name} Рейтинг: {user.Raiting}");
            Console.ResetColor();
        }
        private bool HistoryError()
        {
            if (Games.Count != 0) return false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Щє не зіграно жодної гри");
            Console.ResetColor();
            return true;
        }
        private bool StatisticError()
        {
            if (Users.Count != 0) return false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Не зареєстровано жодного гравця");
            Console.ResetColor();
            return true;
        }

    }
}