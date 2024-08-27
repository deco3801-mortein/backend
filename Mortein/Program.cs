using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Mortein API",
        Version = "0.0.1",
        Description = "RESTful API for the Mortein backend application",
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
