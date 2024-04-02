using dotnet_core_blogs_architecture.blogs;
using dotnet_core_blogs_architecture.blogs.Mediator.Post.Commands.Create;
using dotnet_core_blogs_architecture.Data.Data;
using dotnet_core_blogs_architecture.infrastructure;
using dotnet_core_blogs_architecture.infrastructure.Cache;
using dotnet_core_blogs_architecture.infrastructure.Data;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for demo purposes, replace with Redis in production
builder.Services.AddSingleton(sp =>

{
    var cache = sp.GetRequiredService<IDistributedCache>();
    // Configure time window and max requests here or load from configuration
    var timeWindow = TimeSpan.FromMinutes(1); // Example: 1 minute
    var maxRequests = 100; // Example: 100 requests per minute
    return new RateLimitMiddleware(httpContext => Task.CompletedTask, cache);
});
builder.Services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton(new RedisCacheService(redisConnectionString));
//builder.Services.AddScoped<IDbUserContext, DbUserContext>();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Handler));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RateLimitMiddleware>();

app.MapControllers();

app.Run();
