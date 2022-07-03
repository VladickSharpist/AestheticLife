using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.Web.Core.Extensions;
using AestheticLife.Web.FileUploader.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddFileUploaderWebMapper()
    .AddUnitOfWork<AestheticLifeDbContext>()
    .AddIdentity(builder.Configuration)
    .AddServices()
    .ApplyCors()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app.ApplyMiddlewares(builder);
app.MigrateDbContext<AestheticLifeDbContext>().Run();