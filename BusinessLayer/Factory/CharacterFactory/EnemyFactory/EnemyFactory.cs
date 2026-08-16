using BusinessLayer.Models.GameModel.Characters.Enemy;

namespace BusinessLayer.Factory.CharacterFactory.EnemyFactory
{
    public class EnemyFactory((int min, int max) hBorder, (int min, int max) vBorder) : ICharacterFactory<IEnemy>
    {
        #region Fields
        private const int DefaultHealth = 1;
        #endregion

        #region Public Methods
        public IEnemy Generate()
        {
            return new Enemy(DefaultHealth, hBorder, vBorder);
        }
        #endregion
    }
}