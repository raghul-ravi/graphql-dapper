using GraphQL;
using GraphQL.Types;
using Microsoft.Data.SqlClient;
using System.Data;
using GraphQLDapperService.GraphQL.Queries;
using GraphQLDapperService.GraphQL.Schema;
using GraphQLDapperService.Repository;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IDbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register GraphQL services
builder.Services.AddScoped<EmployeeQuery>();
builder.Services.AddScoped<ISchema, AdventureWorksSchema>();

builder.Services.AddGraphQL(b => b
    .AddGraphTypes(typeof(AdventureWorksSchema).Assembly)
    .AddErrorInfoProvider(x=>x.ExposeExceptionDetails = true)
    .AddSchema<AdventureWorksSchema>()
    .AddSystemTextJson());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseGraphQLPlayground("/graphql");
app.UseGraphQL<ISchema>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
