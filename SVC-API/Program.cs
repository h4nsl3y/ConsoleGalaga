using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.BL.AppUserBL;
using BusinessLayer.BL.PlayerBL;
using BusinessLayer.BL.ScoreRecordBL;
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

    containerBuilder.RegisterType<LeaderBoardService>().As<ILeaderBoardService>().InstancePerLifetimeScope();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVC-API v1");
});

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
