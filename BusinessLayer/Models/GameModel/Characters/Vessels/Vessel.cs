namespace BusinessLayer.Models.GameModel.Characters.Vessels
{
    public class Vessel : IVessel
    {
        #region Fields
        private readonly (int, int) _hBorder;
        private readonly (int, int) _vBorder;
        public int Health { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool Shoot { get; set; }
        #endregion

        #region Constructor
        public Vessel(int health, (int, int) hBorder, (int, int) vBorder)
        {
            if (health <= 0)
                throw new ArgumentOutOfRangeException(nameof(health), "Health must be positive.");

            _hBorder = hBorder;
            _vBorder = vBorder;
            Health = health;
            PositionX = (_hBorder.Item1 + _hBorder.Item2) / 2;
            PositionY = _vBorder.Item2 - 1;
        }
        #endregion

        #region Public Methods
        public void Behaviour()
        {
            ConsoleKeyInfo keyInfo;

            if (!Console.KeyAvailable) return;
            keyInfo = Console.ReadKey(true);
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (PositionX <= _hBorder.Item1)
                    {
                        HitWall();
                        break;
                    }
                    PositionX -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    if (PositionX >= _hBorder.Item2 - 1)
                    {
                        HitWall();
                        break;
                    }
                    PositionX += 1;
                    break;
                case ConsoleKey.Spacebar:
                    Shoot = true;
                    break;
                default:
                    break;
            }
        }

        public string GetSymbol()
        {
            return "A";
        }

        public void HitWall()
        {
            PositionX = PositionX;
        }

        #endregion
    }
}
