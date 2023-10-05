using Ouijjane.Village.Infrastructure.Extensions;
using Ouijjane.Village.Application.Extensions;
using Ouijjane.Village.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigurations();//TODO: .RegisterSerilog();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Environment);


var app = builder.Build();

//TODO: redo it in a clean way
await app.UseWebMiddleware(app.Environment);

app.Run();

public partial class Program { }