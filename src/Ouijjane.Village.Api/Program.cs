var builder = WebApplication
                        .CreateBuilder(args)
                        .AddConfigurations()
                        .UseSerilog();

builder.Services
               .AddVillageApplicationServices()
               .AddVillageInfrastructureServices(builder.Configuration)
               .AddVillageWebServices(builder.Configuration);

var app = builder.Build();

await app.UseVillageMiddlewares();

app.Run();

public partial class Program { }