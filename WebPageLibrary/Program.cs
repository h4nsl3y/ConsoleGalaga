using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.SetIsOriginAllowed(origin =>
            {
                if (string.Equals(origin, "null", StringComparison.OrdinalIgnoreCase))
                    return true;

                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                    return false;

                return uri.Host == "localhost"
                    || uri.Host == "127.0.0.1";
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(corsPolicyName);

var galagaDistPath = Path.Combine(
    app.Environment.ContentRootPath,
    "UI",
    "GalagaUI",
    "dist"
);

if (!Directory.Exists(galagaDistPath))
{
    Directory.CreateDirectory(galagaDistPath);
}

var galagaFileProvider = new PhysicalFileProvider(galagaDistPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = galagaFileProvider,
    RequestPath = "/galaga"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = galagaFileProvider,
    RequestPath = ""
});

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = galagaFileProvider
});

app.Run();
