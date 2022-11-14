using System.Reflection;
using Aesthetic.SignalR.Services.Abstractions.Extensions;
using Aesthetic.SignalR.Services.Abstractions.Hubs;
using Aesthetic.SignalR.Services.Extensions;
using Microservices.Shared.Extensions;
using RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureJwt(builder.Configuration)
    .AddSignalR().Services
    .AddSignalRUserIdProvider()
    .AddNotificationService()
    .ConfigureRabbit(Assembly.GetExecutingAssembly())
    .AddEndpointsApiExplorer().AddSwaggerGen()
    .AddCors(opt => opt.AddPolicy("CorsPolicy",
    builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:7098");
    }
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/hubs/notifications");
});

app.Run();