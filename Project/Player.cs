namespace Project
{
    [Serializable]
    public abstract class Player
    {
        private int Id { get; }
        public string Name { get; set; }
        public int Raiting { get; set; }
        public int GameRait;
        public string Password { get; set; }
        public List<Play> History;
        private static int IdCreate;
        protected abstract int GetRaiting(Play game);
        protected Player(string name, string password)
        {
            Name = name;
            Password = password;
            Id = IdCreate++;
            History = new List<Play>();
        }
        public virtual void WinGame(Play game)
        {
            game.P1CurrRait = game.P1.Raiting;
            game.P2CurrRait = game.P2.Raiting;
            History.Add(game);
        }
        public virtual void LoseGame(Play game)
        {
            game.P1CurrRait = game.P1.Raiting;
            game.P2CurrRait = game.P2.Raiting;
            History.Add(game);
        }
        public void GameResult(Play game)
        {
            History.Add(game);
        }
    }
    [Serializable]
    public class Basic : Player
    {
        public Basic(string name, string password) : base(name, password)
        {
        }
        protected override int GetRaiting(Play game) => GameRait = game.WinRait;
        public override void WinGame(Play game)
        {
            GetRaiting(game);
            Raiting += GameRait;
            base.WinGame(game);
        }
        public override void LoseGame(Play game)
        {
            if (Raiting + game.LoseRait < 0) Raiting = 0;
            else Raiting += game.LoseRait;
            base.LoseGame(game);
        }
    }
    [Serializable]
    public class DoubleWin : Player
    {
        public DoubleWin(string name, string password) : base(name, password)
        {
        }
        protected override int GetRaiting(Play game) => GameRait = 2 * game.WinRait;
        public override void WinGame(Play game)
        {
            GetRaiting(game);
            Raiting += GameRait;
            base.WinGame(game);
        }
        public override void LoseGame(Play game)
        {
            if (Raiting + game.LoseRait < 0) Raiting = 0;
            else Raiting += game.LoseRait;
            base.LoseGame(game);
        }
    }
    [Serializable]
    public class DoubleLose : Player
    {
        public DoubleLose(string name, string password) : base(name, password)
        {
        }
        protected override int GetRaiting(Play game) => GameRait = game.WinRait;
        public override void WinGame(Play game)
        {
            GetRaiting(game);
            Raiting += GameRait;
            base.WinGame(game);
        }
        public override void LoseGame(Play game)
        {
            if (Raiting + game.LoseRait < 0) Raiting = 0;
            else Raiting += game.LoseRait/2;
            base.LoseGame(game);
        }
    }
}