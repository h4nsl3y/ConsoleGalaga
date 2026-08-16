using BusinessLayer.Models.GameModel.Characters;

namespace BusinessLayer.Factory.BulletFactory
{
    public interface IBulletFactory
    {
        IGameElement Generate(int x, int y);
    }
}
