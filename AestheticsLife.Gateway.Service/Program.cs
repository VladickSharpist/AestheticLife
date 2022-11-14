using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt => opt
    .AddPolicy("AestheticsLifePolicy", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json");
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AestheticsLifePolicy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseWebSockets();
app.UseOcelot().Wait();

app.MapControllers();

app.Run();