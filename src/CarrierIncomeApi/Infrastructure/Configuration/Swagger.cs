using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarrierIncomeApi.Infrastructure.Configuration;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var title = configuration.GetValue<string>("Swagger:Title") ?? string.Empty;

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = title,
                        Version = "v1"
                    });

                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (var xmlDoc in dir.EnumerateFileSystemInfos("*.xml"))
                {
                    options.IncludeXmlComments(xmlDoc.FullName);
                }

                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();
                options.IgnoreObsoleteProperties();
                options.SchemaFilter<EntityTitleSchemaFilter>();
                options.CustomSchemaIds(t => t.FullName);
            });

        return services;
    }

    public static IApplicationBuilder UseSwagger(
        this IApplicationBuilder app,
        IConfiguration configuration,
        IHostEnvironment environment
    )
    {
        var enabled = configuration.GetValue<bool>("Swagger:Enabled");
        var title = configuration.GetValue<string>("Swagger:Title") ?? string.Empty;

        if (enabled)
        {
            app.UseSwagger(options => { options.RouteTemplate = "apidocs/{documentName}/swagger.json"; });
            app.UseSwaggerUI(
                options =>
                {
                    options.DocumentTitle = title;
                    options.RoutePrefix = "apidocs";
                    options.SwaggerEndpoint("v1/swagger.json", $"{title} v1");
                    options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
                    options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                    options.DisplayRequestDuration();
                    options.InjectStylesheet("/swagger/custom.css");
                });
            app.UseReDoc(
                options =>
                {
                    options.DocumentTitle = title;
                    options.RoutePrefix = "redocs";
                    options.SpecUrl("/apidocs/v1/swagger.json");
                    options.ExpandResponses("200,201");
                });
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(environment.ContentRootPath, "Infrastructure", "Assets")),
                    RequestPath = "/swagger",
                });
        }

        return app;
    }
}

// Seems janky but its only for doc gen so ¯\_(ツ)_/¯
internal class EntityTitleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // TODO Filter interfaces
        // TODO Filter JSON Patch
        schema.Title = context.Type.Name;
    }
}
