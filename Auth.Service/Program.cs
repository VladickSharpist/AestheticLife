using AestheticLife.Auth.Services.Extensions;
using AestheticsLife.Core.Extensions;
using Auth.Service.Extensions;
using DataAccess.Auth.Extensions;
using MassTransit;
using Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthDataAccess(builder.Configuration)
    .AddMapper()
    .AddHelpers(builder.Configuration)
    .ConfigureJwt(builder.Configuration)
    .AddAuthenticationServices()
    .AddMassTransit(x =>
    {
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