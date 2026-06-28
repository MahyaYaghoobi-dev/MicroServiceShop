using Asp.Versioning;
using Basket.Application.Features.Queries.GetBasketByUserName;
using Basket.Application.Mappings;
using Basket.Core.Repositories;
using Basket.Core.Settings;
using Basket.Infrastructure.Services;
using Mapster;
using MapsterMapper;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "Basket.Api", Description = "basket api", Version = "v1" }));

// DI Repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetBasketByUserNameQuery).Assembly);
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ...";
});

// Mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapperProfile).Assembly);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// API Versioning
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

// Redis Settings
builder.Services.Configure<RedisSettings>(
    builder.Configuration.GetSection(nameof(RedisSettings)));

// Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    var settings = builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
    options.Configuration = settings?.ConnectionString;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();