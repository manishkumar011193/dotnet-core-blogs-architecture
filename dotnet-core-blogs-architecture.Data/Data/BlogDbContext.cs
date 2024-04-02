using dotnet_core_blogs_architecture.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_blogs_architecture.Data.Data
{
    public class BlogDbContext : DbContext
    { 
        public BlogDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    } 
}
