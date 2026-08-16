using BusinessLayer.Models.GameModel.BulletModel;
using BusinessLayer.Models.GameModel.Characters;


namespace BusinessLayer.Factory.BulletFactory
{
    public class BulletFactory((int min, int max) hBorder, (int min, int max) vBorder) : IBulletFactory
    {

        #region Public Methods
        public IGameElement Generate(int x, int y)
        {
            return new SlashBulletModel(x, y, hBorder, vBorder);
        }
        #endregion
    }
}

