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
    /// Name of the CORS policy which defines the allowed origins.
    /// </summary>
    private readonly string AllowedOriginsCorsPolicy = nameof(AllowedOriginsCorsPolicy);

    /// <summary>
    /// Origins which are allowed to access this API.
    /// </summary>
    private readonly string[] AllowedOrigins = [
        "https://vibegrow.pro",
        "http://localhost:5173",
        "http://127.0.0.1:5173"
    ];

    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// 
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy(
                AllowedOriginsCorsPolicy,
                policy => policy.WithOrigins(AllowedOrigins)
            );
        });
        services.AddDbContext<DatabaseContext>();
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
        }
        else
        {
            app.UseHttpsRedirection();
        }

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

        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DatabaseContext>();
            context.Database.EnsureCreated();
        }

        app.UseRouting();
        app.UseCors(AllowedOriginsCorsPolicy);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

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
