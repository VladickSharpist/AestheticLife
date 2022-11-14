using System.Reflection;
using Aesthetic.CQRS.Extensions;
using AestheticLife.Auth.Services.Extensions;
using AestheticsLife.Core.Extensions;
using AestheticsLife.DataAccess.User;
using AestheticsLife.DataAccess.User.Extensions;
using AestheticsLife.User.Service.Extensions;
using MassTransit;
using Microservices.Shared.Extensions;
using RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUserDataAccess(builder.Configuration)
    .AddMapper()
    .AddHelpers(builder.Configuration)
    .ConfigureJwt(builder.Configuration)
    .AddCqrs()
    .ConfigureRabbit(Assembly.GetExecutingAssembly())
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
    .AddHealthChecks()
    .AddDbContextCheck<AestheticLifeUserDbContext>()
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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.ApplyHealthChecks();
});
app.Run();