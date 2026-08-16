namespace BusinessLayer.Models.GameModel.Characters
{
    public interface IGameElement : IPositionable
    {
        string GetSymbol();
        void Behaviour();
        void HitWall();
    }
}


