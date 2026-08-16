namespace BusinessLayer.Models.GameModel.BulletModel
{
    public class IceBulletModel(int x, int y, (int, int) hBorder, (int, int) vBorder) : BulletModel(x, y, hBorder, vBorder)
    {
        #region Fields
        private int _movementDirection = 1;
        #endregion

        #region Public Methods
        public override string GetSymbol()
        {
            return "I";
        }

        public override void Behaviour()
        {
            bool IsEvenRow = PositionY % 2 == 0;
            base.Behaviour();
            PositionX = IsEvenRow ? x : Divert();
        }
        #endregion

        #region Private Methods
        private int Divert()
        {
            int axis = x + _movementDirection;
            _movementDirection *= -1;
            return axis;
        }
        #endregion
    }
}