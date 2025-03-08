using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from Config/global.env
Env.Load("../../../Configs/global.env");
builder.Configuration.AddEnvironmentVariables();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot services
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
Console.WriteLine(app.Environment.IsDevelopment());

// Use developer exception page in development mode
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Routing middleware
app.UseRouting();

// Authorization middleware
app.UseAuthorization();

// Enable Ocelot middleware to forward requests
await app.UseOcelot();

// Get environment-specific URLs
var environmentUrls = Environment.GetEnvironmentVariable("API_GATEWAY_HOST") ?? "http://localhost:5000";
app.Urls.Add(environmentUrls);

app.Run();
