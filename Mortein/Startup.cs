using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mortein;

/// <summary>
/// Configure the application's startup.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="configuration"></param>
public class Startup(IConfiguration configuration)
{
    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration { get; } = configuration;

    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// 
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("openapi", new OpenApiInfo()
            {
                Title = "Mortein API",
                Version = Assembly.GetExecutingAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version,
                Description = "RESTful API for the Mortein backend application",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    /// <summary>
    /// Configure the HTTP request pipeline.
    /// </summary>
    /// 
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "{documentName}.json";
                });
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Mortein API Docs";
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/openapi.json", "Mortein API");
            });
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", () => "Hello World!")
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Index",
                    Description = "Ping application."
                }
            );
        });
    }
}
