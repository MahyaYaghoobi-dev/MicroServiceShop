using Asp.Versioning;
using Basket.Application.Features.Queries.GetBasketByUserName;
using Basket.Application.Mappings;
using Basket.Core.Repositories;
using Basket.Core.Settings;
using Basket.Infrastructure.Services;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//swagger config
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "Basket.Api", Description = "basket api", Version = "v1" }));


//DI Repository
builder.Services.AddScoped<IBasketRepository, BasketRepository>();


//mediatr config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetBasketByUserNameQuery).Assembly);
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODEzNjIyNDAwIiwiaWF0IjoiMTc4MjE0NDU3NSIsImFjY291bnRfaWQiOiIwMTllZjAxNzdkM2U3YTI0YjM5N2E4MjkzNTdmMDVkOCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa3ZyMWdhMGIyeXB5dGZ4MHFiNG14aGh6Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.cQThoMRRp9HFovrbSiV6xr1JVaykxwpazSBKS3aJ34IdT_f_Z-2HgwWT3miCP5Bxn2s3EvJcNaK7mYgpJ9sLN6YFxUqVAquGmBWR_9Wq9zH0xrH2ShwCROZC1mhHtFQKtcWCIMN8rMwxxTSvs7Op1DuLW-1A2jU8GlyJMnSJeW_5yyArsbVqY8UtC7HsVCrPlHuykse1cWbL4s8Z-S5hdnv51mRbeCEZ9Yu6vgtKKOh3KyJH3WZ2Jsf85k4iWh-xQUnrMsy5b5-3MwoU2GgyiLvQ86JrzhuowUSKOmogOBGSJUF382CjtV4h3ztxpMrCNGAbggkAEOxRmqMSBK4tjA";  
});



//mapster config
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(MapperProfile).Assembly); 

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();


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


//redis setting - get connection string
builder.Services.Configure<RedisSettings>(
    builder.Configuration.GetSection(nameof(RedisSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    
    //swagger config
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
