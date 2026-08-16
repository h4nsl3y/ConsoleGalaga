namespace BusinessLayer.Models.GameModel.Characters.Enemy
{
    public interface IEnemy : IGameElement, ICharacter
    {
        bool IsAlive { get; set; }
        int Size { get; set; }
        bool OccupiesPosition(int x, int y);
    }
}


