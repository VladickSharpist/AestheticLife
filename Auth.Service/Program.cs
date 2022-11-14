using System.Reflection;
using AestheticLife.Auth.Services.Extensions;
using AestheticsLife.Core.Extensions;
using Auth.Service.Extensions;
using DataAccess.Auth;
using DataAccess.Auth.Extensions;
using Logic.Shared.Extensions;
using Microservices.Shared.Extensions;
using RabbitMq;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthDataAccess(builder.Configuration)
    .AddMapper()
    .AddHelpers(builder.Configuration)
    .ConfigureJwt(builder.Configuration)
    .AddTokenServices()
    .AddAuthenticationServices()
    .ConfigureRabbit()
    .AddControllers()
    .Services
    .AddEndpointsApiExplorer()
    .AddHealthChecks()
    .AddDbContextCheck<AuthDbContext>()
    .Services
    .AddSwaggerGen();
ConfigureLogging();
builder.Host.UseSerilog();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.ConfigureExceptionHandler();
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

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}