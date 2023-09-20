using Ouijjane.Village.Infrastructure.Extensions;
using Ouijjane.Village.Application.Extensions;
using Ouijjane.Village.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddWebServices(builder.Environment);


var app = builder.Build();

await app.UseWebMiddleware(app.Environment);

app.Run();
