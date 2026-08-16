using BusinessLayer.Models.GameModel.Characters;
using BusinessLayer.Models.GameModel.Characters.Enemy;
using BusinessLayer.Service.LeaderBoardService;

namespace BusinessLayer.Service.DisplayService
{
    public class DisplayService(ILeaderBoardService leaderBoardService, string name, string description, int width, int height) : IDisplayService
    {
        #region Fields
        private const string HorizontalBorder = "-";
        private const string VerticalBorder = "|";
        public string Name { get; } = name;
        public string Description { get; } = description;
        public int Width { get; } = width;
        public int Height { get; } = height;

        #endregion
        #region Constructor
        #endregion

        #region Public Methods
        public void Initialize()
        {
            Console.CursorVisible = false;
            Console.WriteLine($"=== {Name} ===");
            Console.WriteLine(Description);
            Console.ReadKey(true);
        }

        public void Draw(IList<IGameElement> elements, int score, int health)
        {
            Console.Clear();
            DrawHorizontalBorder();
            DrawRowElements(elements);
            DrawHorizontalBorder();

            Console.WriteLine();
            Console.WriteLine($" Score: {score}  |  Health: {health}  |  Press Arrow Keys to move, Space to Shoot");
        }

        public void ShowGameOver(int score)
        {
            Console.Clear();
            Console.WriteLine($"Game Over! Score: {score}");
        }

        public async Task EnterNameForLeaderBoard(int score)
        {
            Console.WriteLine("Enter your name for the leaderboard:");
            string name = Console.ReadLine() ?? "Player";

            await leaderBoardService.AddScoreAsync(name, score);
        }
        #endregion

        #region Private Methods
        private void DrawHorizontalBorder()
        {
            for (int i = 0; i < Width + 2; i++)
            {
                Console.Write(HorizontalBorder);
            }
            Console.WriteLine();
        }

        private void DrawRowElements(IList<IGameElement> elements)
        {
            for (int row = 0; row < Height; row++)
            {
                Console.Write(VerticalBorder);
                for (int col = 0; col < Width; col++)
                {
                    var enemy = elements.OfType<IEnemy>().FirstOrDefault(elem => elem.IsAlive && elem.OccupiesPosition(col, row));
                    if (enemy != null)
                    {
                        int offset = col - enemy.PositionX;
                        Console.Write(enemy.GetSymbol()[offset]);
                    }
                    else
                    {
                        var element = elements.FirstOrDefault(elem => elem.PositionX == col && elem.PositionY == row);
                        if (element != null && element is not IEnemy)
                        {
                            Console.Write(element.GetSymbol());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine(VerticalBorder);
            }
        }
        #endregion
    }
}