namespace BusinessLayer.Models.GameModel.Characters.Enemy
{
    public class Enemy : IEnemy
    {
        #region Fields
        private static readonly Random SharedRandom = new();

        private readonly (int, int) _hBorder;
        private readonly (int, int) _vBorder;

        public int Health { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool IsAlive { get; set; } = true;
        public int Size { get; set; }
        #endregion

        #region Constructor
        public Enemy(int health, (int, int) hBorder, (int, int) vBorder)
        {
            if (health <= 0)throw new ArgumentOutOfRangeException(nameof(health), "Health must be positive.");

            _hBorder = hBorder;
            _vBorder = vBorder;
            Health = health;
            PositionY = hBorder.Item1 + 1;
            PositionX = vBorder.Item1 + 1;
            Size = SharedRandom.Next(1, 4);
        }
        #endregion

        #region Public Methods
        public string GetSymbol()
        {
            return Size switch
            {
                1 => "W",
                2 => "WW",
                _ => "MwM",
            };
        }

        public bool OccupiesPosition(int x, int y)
        {
            return y == PositionY && x >= PositionX && x < PositionX + Size;
        }

        public void Behaviour()
        {
            int xMovementDirection = PositionY % 2 == 0 ? 1 : -1;
            PositionX += xMovementDirection;

            if (PositionX <= _hBorder.Item1 || PositionX + Size - 1 >= _hBorder.Item2)
                PositionY += 1;

            if (PositionY >= _vBorder.Item2)
                HitWall();
        }

        public void HitWall()
        {
            IsAlive = false;
        }
        #endregion
    }
}