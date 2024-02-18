using ExampleApi.Structures;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Database
{
    public class PostDataContext : DbContext
    {
        public PostDataContext(DbContextOptions<PostDataContext> opt) : base(opt) {}
        public DbSet<Post> Posts { get; set; }
    }
}