using BusinessLayer.Models.GameModel.Characters;

namespace BusinessLayer.Service.DisplayService
{
    public interface IDisplayService
    {
        string Name { get; }
        string Description { get; }
        int Width { get; }
        int Height { get; }

        void Initialize();
        void Draw(IList<IGameElement> elements, int score, int health);
        void ShowGameOver(int score);
        Task EnterNameForLeaderBoard(int score);
    }
}
