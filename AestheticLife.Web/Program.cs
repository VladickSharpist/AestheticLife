using Aesthetic.SignalR.Services.Abstractions.Extensions;
using Aesthetic.SignalR.Services.Abstractions.Hubs;
using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.Web.Core.Extensions;
using AestheticLife.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddWebMapper()
    .AddUnitOfWork<AestheticLifeDbContext>()
    .AddIdentity(builder.Configuration)
    .AddWebSockets()
    .AddServices()
    .AddSignalR(opt =>
    {
        opt.EnableDetailedErrors = true;
    })
    .Services
    .AddValidator()
    .ApplyCors()
    .AddControllers().Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.MapHub<NotificationHub>("/notifications");
app.ApplyMiddlewares(builder);

app.MigrateDbContext<AestheticLifeDbContext>().Run();