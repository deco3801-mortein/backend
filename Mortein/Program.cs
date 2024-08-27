using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("openapi", new OpenApiInfo()
    {
        Title = "Mortein API",
        Version = Assembly.GetExecutingAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version,
        Description = "RESTful API for the Mortein backend application",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
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

app.MapGet("/", () => "Hello World!")
    .WithOpenApi(operation => new(operation)
    {
        Summary = "Index",
        Description = "Ping application."
    });

app.Run();
