using AestheticLife.Auth.Services.Abstractions.Extesions;
using AestheticLife.DataAccess;
using AestheticLife.DataAccess.Extensions;
using AestheticLife.Web.Core.Extensions;
using AestheticLife.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDatabase(builder.Configuration)
    .AddWebMapper()
    .AddAuthServicesMapper()
    .AddUnitOfWork<AestheticLifeDbContext>()
    .AddCustomRepositories()
    .AddInfrastructure(builder.Configuration)
    .AddServices()
    .AddIdentity(builder.Configuration)
    .AddValidator()
    .ApplyCors()
    .AddControllers().Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(builder.Services.GetUsingCors());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDbContext<AestheticLifeDbContext>().Run();