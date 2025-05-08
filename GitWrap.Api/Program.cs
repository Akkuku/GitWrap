using GitWrap.Api.Configuration;
using GitWrap.Application;
using GitWrap.Infrastructure; 

var builder = WebApplication.CreateBuilder(args);

var configPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "Config");

builder.Configuration
    .SetBasePath(configPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services
    .AddControllers();

builder.Services
    .ConfigureInfrastructure(builder.Configuration)
    .ConfigureApplication();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();