using AestheticLife.Web.Core.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ApplyCors();
builder.Services.AddControllers();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder.Services.GetUsingCors());
app.UseHttpsRedirection();

app.UseAuthorization();
await app.UseOcelot();

app.MapControllers();

app.Run();