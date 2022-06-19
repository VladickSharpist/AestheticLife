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
    .AddServices()
    .AddIdentity(builder.Configuration)
    .ApplyCors()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();