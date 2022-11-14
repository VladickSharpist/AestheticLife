using System.Reflection;
using Aesthetic.CQRS.Extensions;
using AestheticsLife.Core.Extensions;
using AestheticsLife.DataAccess.Training;
using AestheticsLife.DataAccess.Training.Extensions;
using AestheticsLife.Training.Service.Extensions;
using Logic.Shared.Extensions;
using Microservices.Shared.Extensions;
using Microservices.Shared.Middlewares;
using RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTrainingDataAccess(builder.Configuration)
    .AddMapper()
    .AddHelpers(builder.Configuration)
    .AddCurrentUser()
    .AddTokenServices()
    .AddCqrs()
    .ConfigureJwt(builder.Configuration)
    .ConfigureRabbit(Assembly.GetExecutingAssembly())
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
    .AddHealthChecks()
    .AddDbContextCheck<AestheticsLifeTrainingDbContext>()
    .Services
    .AddSwaggerGen();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CurrentUserSetterMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.ApplyHealthChecks();
});
app.Run();