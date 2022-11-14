using AestheticsLife.Core.Extensions;
using AestheticsLife.File.Services.Extensions;
using RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHelpers(builder.Configuration);
builder.Services
    .AddFileService()
    .ConfigureRabbit();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();