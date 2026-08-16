using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.BL.AppUserBL;
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
using DataLayer.DAL.AppUserDAL;
using DataLayer.DAL.PlayerDAL;
using DataLayer.DAL.ScoreRecordDAL;
using DataLayer.DBConnection;
using Helpers.LoggerHelper;
using ILogger = Helpers.LoggerHelper.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.SetIsOriginAllowed(origin =>
            {
                if (origin == "null")
                    return true;

                var uri = new Uri(origin);

                return uri.Host == "localhost"
                    || uri.Host == "127.0.0.1";
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllersWithViews();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterInstance((Logger)Logger.Instance).As<ILogger>().SingleInstance();
    containerBuilder.RegisterType<SQLExecutor>().As<IDBConnection>().WithParameter("connString", connectionString).InstancePerLifetimeScope();

    containerBuilder.RegisterType<PlayerDAL>().As<IPlayerDAL>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ScoreRecordDAL>().As<IScoreRecordDAL>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AppUserDAL>().As<IAppUserDAL>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<PlayerBL>().As<IPlayerBL>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ScoreRecordBL>().As<IScoreRecordBL>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AppUserBL>().As<IAppUserBL>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<VesselFactory>().As<ICharacterFactory<IVessel>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<EnemyFactory>().As<ICharacterFactory<IEnemy>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<BulletFactory>().As<IBulletFactory>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<LeaderBoardService>().As<ILeaderBoardService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DisplayService>().As<IDisplayService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<GameEngineService>().As<IGameEngineService>().InstancePerLifetimeScope();
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
