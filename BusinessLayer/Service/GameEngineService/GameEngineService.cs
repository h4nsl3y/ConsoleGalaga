using BusinessLayer.Factory.BulletFactory;
using BusinessLayer.Factory.CharacterFactory;
using BusinessLayer.Models.GameModel.BulletModel;
using BusinessLayer.Models.GameModel.Characters;
using BusinessLayer.Models.GameModel.Characters.Enemy;
using BusinessLayer.Models.GameModel.Characters.Vessels;
using BusinessLayer.Service.DisplayService;

namespace BusinessLayer.Service.GameEngineService
{
    public class GameEngineService(IDisplayService display, ICharacterFactory<IVessel> vesselFactory, IBulletFactory bulletFactory, ICharacterFactory<IEnemy> enemyFactory, int enemySpawnInterval, int tickDelayMs) : IGameEngineService
    {

        #region Fields
        private IList<IGameElement> _elements = [];
        private IVessel _vessel = null!;
        private int _score;
        private int _enemySpawnCounter;
        #endregion

        #region Public Methods
        public async Task RunGameLoop()
        {
            display.Initialize();
            _vessel = vesselFactory.Generate();
            _elements.Add(_vessel);

            while (_vessel.Health > 0)
            {
                _enemySpawnCounter++;
                if (_enemySpawnCounter % enemySpawnInterval == 0)
                    _elements.Add(enemyFactory.Generate());

                Update();
                display.Draw(_elements, _score, _vessel.Health);
                await Task.Delay(tickDelayMs);
            }

            display.ShowGameOver(_score);
            await display.EnterNameForLeaderBoard(_score);
        }
        #endregion

        #region Private Methods
        private void Update()
        {
            _elements.ToList().ForEach(elem => elem.Behaviour());
            HandleVesselShooting();
            CheckBulletEnemyCollisions();
            CheckEnemyReachedVessel();
            RemoveDeadElements();
        }

        private void HandleVesselShooting()
        {
            if (!_vessel.Shoot) return;

            _elements.Add(bulletFactory.Generate(_vessel.PositionX, _vessel.PositionY));
            _vessel.Shoot = false;
        }

        private void CheckBulletEnemyCollisions()
        {
            var bullets = _elements.OfType<IBulletModel>().Where(b => !b.HasHit).ToList();
            var enemies = _elements.OfType<IEnemy>().Where(e => e.IsAlive).ToList();

            foreach (var bullet in bullets)// implement null check
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.OccupiesPosition(bullet.PositionX, bullet.PositionY))
                    {
                        bullet.HasHit = true;
                        enemy.Health--;
                        if (enemy.Health <= 0)
                        {
                            enemy.IsAlive = false;
                            _score++;
                        }
                    }
                }
            }
        }

        private void CheckEnemyReachedVessel()
        {
            var enemies = _elements.OfType<IEnemy>().Where(e => e.IsAlive).ToList();
            foreach (var enemy in enemies)
            {
                if (enemy.PositionY >= _vessel.PositionY)
                {
                    _vessel.Health--;
                    enemy.IsAlive = false;
                }
            }
        }

        private void RemoveDeadElements()
        {
            _elements = [.. _elements.Where(elem => !(elem is IBulletModel bullet && bullet.HasHit) && !(elem is IEnemy enemy && !enemy.IsAlive))];
        }
        #endregion
    }
}
