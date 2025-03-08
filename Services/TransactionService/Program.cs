using Microsoft.OpenApi.Models;
using TransactionService.API.Middlewares;
using TransactionService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json and environment variables
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); 

// Configure Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TransactionService API",
        Version = "v1",
        Description = "Service for managing tickets in the cinema."
    });

    // Configure security for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
app.UseApplicationBuilderExtensions();  

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use custom middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Set service URL from environment variable or configuration
app.Urls.Add(Environment.GetEnvironmentVariable("TRANSACTION_SERVICE_URL") ?? 
             builder.Configuration["Service:Url"] ?? "http://localhost:5006");

app.Run();