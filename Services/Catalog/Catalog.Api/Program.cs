using Catalog.Core.Repositories;
using Catalog.Core.Settings;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//swagger config
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "Catalog.Api", Description = "catalog api", Version = "v1" }));


//mongodb config
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<ICatalogContext, CatalogContext>();

//repositories DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();


var app = builder.Build();

//seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();
    CatalogContextSeed.SeedData(context.Products, context.Brands, context.Types);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    
    //swagger config
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");


app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
