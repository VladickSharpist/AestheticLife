using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.Web.Core.Extensions;
using AestheticsLife.Web.Training.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddTrainingWebMapper()
    .AddUnitOfWork<AestheticLifeDbContext>()
    .AddIdentity(builder.Configuration)
    .AddServices()
    .AddValidator()
    .ApplyCors()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.ApplyMiddlewares(builder);

app.MigrateDbContext<AestheticLifeDbContext>().Run();