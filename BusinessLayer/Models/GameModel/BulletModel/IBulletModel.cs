using BusinessLayer.Models.GameModel.Characters;

namespace BusinessLayer.Models.GameModel.BulletModel
{
    public interface IBulletModel : IGameElement
    {
        bool HasHit { get; set; }
    }
}


