namespace BusinessLayer.Models.GameModel.BulletModel
{
    public abstract class BulletModel(int x, int y, (int, int) hBorder, (int, int) vBorder) : IBulletModel
    {
        #region Fields
        protected readonly (int, int) _hBorder = hBorder;
        protected readonly (int, int) _vBorder = vBorder;
        public int PositionX { get; set; } = x;
        public int PositionY { get; set; } = y - 1;
        public bool HasHit { get; set; } = false;
        #endregion

        #region Public Methods
        public virtual string GetSymbol()
        {
            return "!";
        }

        public virtual void Behaviour()
        {
            PositionY -= 1;
            if (PositionY < _vBorder.Item1)
                HitWall();
        }

        public void HitWall()
        {
            HasHit = true;
        }
        #endregion
    }
}
