using System.Reflection;
using Aesthetic.CQRS.Extensions;
using AestheticLife.Auth.Services.Extensions;
using AestheticsLife.Core.Extensions;
using AestheticsLife.DataAccess.User.Extensions;
using AestheticsLife.User.Service.Extensions;
using MassTransit;
using Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUserDataAccess(builder.Configuration)
    .AddMapper()
    .AddHelpers(builder.Configuration)
    .ConfigureJwt(builder.Configuration)
    .AddCqrs()
    .AddMassTransit(x =>
    {
        x.AddConsumers(Assembly.GetExecutingAssembly());
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("rabbitmq-broker", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ConfigureEndpoints(context);
        });
    })
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
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
app.MapControllers();
app.Run();