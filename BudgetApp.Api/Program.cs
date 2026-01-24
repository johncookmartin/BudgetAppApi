using BudgetApp.Application;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices<ApiAssemblyMarker>(builder.Configuration);

//Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BudgetApp",
        Version = "v1"
    });

});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Dev",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000", "http://localhost:3001")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Dev");

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetApp v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseCors("Prod");
}

await app.ServiceInitialization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public sealed class ApiAssemblyMarker { }