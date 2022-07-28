using AestheticLife.Web.Gateway.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => opt
    .AddPolicy("AestheticsLifePolicy", policy => 
        policy
            .WithOrigins("http://localhost:5050")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));
builder.Services.AddControllers();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AestheticsLifePolicy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<WebSocketsMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();
await app.UseOcelot();

app.MapControllers();

app.Run();