using BusinessLayer.Enums;

namespace BusinessLayer.Models.GameModel.BulletModel
{
    public class SlashBulletModel(int x, int y, (int, int) hBorder, (int, int) vBorder) : BulletModel(x, y, hBorder, vBorder)
    {
        #region Public Methods
        public string GetSymbol(BulletFrameEnum index)
        {
            if (index == BulletFrameEnum.Frame1)
            {
                return "\\";
            }
            else if (index == BulletFrameEnum.Frame2)
            {
                return "/";
            }
            return base.GetSymbol();
        }
        #endregion
    }
}
