using Aesthetic.CQRS.Extensions;
using AestheticsLife.DataAccess.Training.Extensions;
using AestheticsLife.Training.Service.Extensions;
using Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTrainingDataAccess(builder.Configuration)
    .AddMapper()
    .AddCqrs()
    .ConfigureJwt(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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