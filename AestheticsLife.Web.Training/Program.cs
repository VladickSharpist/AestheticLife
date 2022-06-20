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
    .AddServices()
    .AddIdentity(builder.Configuration)
    .AddValidator()
    .ApplyCors()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ }

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(builder.Services.GetUsingCors());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDbContext<AestheticLifeDbContext>().Run();