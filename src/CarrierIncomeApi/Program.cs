using CarrierIncomeApi.Infrastructure.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var host = builder.Host;
var services = builder.Services;
var environment = builder.Environment;

/* Configuration */
configuration
    .AddEnvironmentVariables()
    .Build();

/* Host */
host.UseSerilog(
    (context, config) =>
    {
        if (context.Configuration.GetValue<bool?>("Serilog:UseSelfLog") == true)
            Serilog.Debugging.SelfLog.Enable(Console.Error);

        config.ReadFrom.Configuration(context.Configuration);
    });

/* Services */
services.AddOptions();

services
    .AddControllersWithViews()
    .AddJsonOptions(environment.IsDevelopment()
        ? JsonSerializationOptions.Development
        : JsonSerializationOptions.Default);

services.AddHealthChecks();
services.AddSwagger(configuration);

services.AddRepositories();

var application = builder.Build();

application.UseRouting();
application.UseCors(policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
});

application.UseSwagger(configuration, environment);
application.MapControllers();

application.Run();
