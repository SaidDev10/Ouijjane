using Ouijjane.Village.Infrastructure.Extensions;
using Ouijjane.Village.Application.Extensions;
using Ouijjane.Village.Api.Extensions;

var builder = WebApplication.CreateBuilder(args)
                            .AddConfigurations();

builder.Services
       .AddVillageApplicationServices()
       .AddVillageInfrastructureServices(builder.Configuration)
       .AddVillageWebServices(builder.Configuration);

var app = builder.Build();

await app.UseVillageWebServices();

app.Run();

public partial class Program { }