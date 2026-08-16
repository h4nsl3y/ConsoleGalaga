using BusinessLayer.BL.PlayerBL;
using BusinessLayer.BL.ScoreRecordBL;
using BusinessLayer.Factory.BulletFactory;
using BusinessLayer.Factory.CharacterFactory;
using BusinessLayer.Factory.CharacterFactory.EnemyFactory;
using BusinessLayer.Factory.CharacterFactory.VesselFactory;
using BusinessLayer.Models.GameModel.Characters.Enemy;
using BusinessLayer.Models.GameModel.Characters.Vessels;
using BusinessLayer.Service.DisplayService;
using BusinessLayer.Service.GameEngineService;
using BusinessLayer.Service.LeaderBoardService;
using DataLayer.DAL.PlayerDAL;
using DataLayer.DAL.ScoreRecordDAL;
using DataLayer.DBConnection;
using Helpers.LoggerHelper;

const int width = 30;
const int height = 6;
const int tickDelayMs = 200;
const int enemySpawnInterval = 10;
const string connectionString = "Server=LOCALHOST;Database=GALAGA;User Id=UserId;Password=sql@PassWord;TrustServerCertificate=True;";
(int, int) hBorder = (0, width);
(int, int) vBorder = (0, height);

ILogger _logger = Logger.Instance;

IDBConnection dBConnection = new SQLExecutor(_logger, connectionString);
IPlayerDAL playerDAL = new PlayerDAL(dBConnection);
IScoreRecordDAL scoreRecordDAL = new ScoreRecordDAL(dBConnection);

IPlayerBL _playerBL = new PlayerBL(playerDAL, _logger);
IScoreRecordBL _scoreRecordBL = new ScoreRecordBL(scoreRecordDAL, _logger);

ILeaderBoardService leaderBoardService = new LeaderBoardService(_playerBL, _scoreRecordBL, _logger);
ICharacterFactory<IVessel> vesselFactory = new VesselFactory(hBorder, vBorder);
IBulletFactory bulletFactory = new BulletFactory(hBorder, vBorder);
ICharacterFactory<IEnemy> enemyFactory = new EnemyFactory(hBorder, vBorder);

IDisplayService display = new DisplayService(leaderBoardService, "GALAGA", "Press any key to start...", width, height);
IGameEngineService game = new GameEngineService(display, vesselFactory, bulletFactory, enemyFactory, enemySpawnInterval, tickDelayMs);
await game.RunGameLoop();