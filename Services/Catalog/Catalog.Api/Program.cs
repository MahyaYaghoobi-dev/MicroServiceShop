using Asp.Versioning;
using Catalog.Application.Features.Queries.GetAllProducts;
using Catalog.Application.Mappings;
using Catalog.Core.Repositories;
using Catalog.Core.Settings;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;

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


//mapster config
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapperProfile).Assembly); 

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();


//mediatr config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly);
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODEzNjIyNDAwIiwiaWF0IjoiMTc4MjE0NDU3NSIsImFjY291bnRfaWQiOiIwMTllZjAxNzdkM2U3YTI0YjM5N2E4MjkzNTdmMDVkOCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa3ZyMWdhMGIyeXB5dGZ4MHFiNG14aGh6Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.cQThoMRRp9HFovrbSiV6xr1JVaykxwpazSBKS3aJ34IdT_f_Z-2HgwWT3miCP5Bxn2s3EvJcNaK7mYgpJ9sLN6YFxUqVAquGmBWR_9Wq9zH0xrH2ShwCROZC1mhHtFQKtcWCIMN8rMwxxTSvs7Op1DuLW-1A2jU8GlyJMnSJeW_5yyArsbVqY8UtC7HsVCrPlHuykse1cWbL4s8Z-S5hdnv51mRbeCEZ9Yu6vgtKKOh3KyJH3WZ2Jsf85k4iWh-xQUnrMsy5b5-3MwoU2GgyiLvQ86JrzhuowUSKOmogOBGSJUF382CjtV4h3ztxpMrCNGAbggkAEOxRmqMSBK4tjA";  
});


//versioning config
builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


var app = builder.Build();

//seed data
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();
//     CatalogContextSeed.SeedData(context.Products, context.Brands, context.Types);
// }




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
