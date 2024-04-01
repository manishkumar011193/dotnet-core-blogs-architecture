using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for demo purposes, replace with Redis in production
//builder.Services.AddSingleton<RateLimitMiddleware>(sp =>
//{
//    var cache = sp.GetRequiredService<IDistributedCache>();
//    var timeWindow = TimeSpan.FromMinutes(1); // Example: 1 minute
//    var maxRequests = 100; // Example: 100 requests per minute
//    return new RateLimitMiddleware(httpContext => Task.CompletedTask, cache, timeWindow, maxRequests);
//});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddScoped<IReadRepository<User>, ReadRepository<User>>();
builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<RateLimitMiddleware>();

app.MapControllers();

app.Run();
