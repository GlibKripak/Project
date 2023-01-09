namespace Project
{
    [Serializable]
    public class Play
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        private string[] GameField = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public int WinRait = 10;
        public int LoseRait = -10;
        public int P1CurrRait;
        public int P2CurrRait;
        public Player Winner { get; set; }
        public Player Looser { get; set; }
        private int Turn = 1;
        private bool End;
        public Play(Player p1, Player p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public void PrintField()
        {
            for (var i = 1; i < GameField.Length + 1; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|" + GameField[i - 1] + "|");
                Console.ResetColor();
                if (i % 3 == 0)
                    Console.WriteLine();
            }
        }
        public void Turns()
        {
            var who = Turn % 2 != 0 ? "X" : "O";
            Turn++;
            if (!End)
                Choose(who);
        }
        public bool Tie() => GameField.All(field => field.Equals("X") || field.Equals("O"));
        private void Choose(string who)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Оберіть вільну клітинку 1-9, щоб зробити хід:");
                Console.ResetColor();
                PrintField();
                var cell = int.Parse(Console.ReadLine());
                Console.Clear();
                if (cell != int.Parse(GameField[cell - 1])) return;
                GameField[cell - 1] = who;
                if (!Finish())
                    Turns();
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Оберіть вільну клітинку !");
                Console.ResetColor();
                Choose(who);
            }
        }
        private bool Finish()
        {
            if (WinCondition("X"))
            {
                Win(P1);
                End = true;
                return true;
            }
            if (WinCondition("O"))
            {
                Win(P2);
                End = true;
                return true;
            }
            if (!Tie()) return false;
            PrintField();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Нічия");
            Console.ResetColor();
            P1.GameResult(this);
            P2.GameResult(this);
            return true;
        }
        public bool WinCondition(string condition)
        {
            return GameField[0].Equals(condition) && GameField[4].Equals(condition) && GameField[8].Equals(condition) ||
                   GameField[2].Equals(condition) && GameField[4].Equals(condition) && GameField[6].Equals(condition) ||
                   GameField[0].Equals(condition) && GameField[1].Equals(condition) && GameField[2].Equals(condition) ||
                   GameField[3].Equals(condition) && GameField[4].Equals(condition) && GameField[5].Equals(condition) ||
                   GameField[6].Equals(condition) && GameField[7].Equals(condition) && GameField[8].Equals(condition) ||
                   GameField[0].Equals(condition) && GameField[3].Equals(condition) && GameField[6].Equals(condition) ||
                   GameField[1].Equals(condition) && GameField[4].Equals(condition) && GameField[7].Equals(condition) ||
                   GameField[2].Equals(condition) && GameField[5].Equals(condition) && GameField[8].Equals(condition);
        }
        private void Win(Player user)
        {
            PrintField();
            if (P1 == user)
            {
                Winner = P1;
                Looser = P2;
                WriteWinner(P1, P2, "X");
            }
            else
            {
                Winner = P2;
                Looser = P1;
                WriteWinner(P2, P1, "O");
            }
        }
        private void WriteWinner(Player winner, Player loser, string who)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Переміг " + who + ": " + winner.Name);
            Console.ResetColor();
            winner.WinGame(this);
            loser.LoseGame(this);
        }

    }
}