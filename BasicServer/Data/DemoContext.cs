using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var posts = new List<Post>
            {
                new Post { PostId = 1, BlogId = 1, Title = "Post1", Content = "Content" },
                new Post { PostId = 2, BlogId = 1, Title = "Post2", Content = "Content" },
                new Post { PostId = 3, BlogId = 2, Title = "Post2", Content = "Content" },
                new Post { PostId = 4, BlogId = 2, Title = "Post2", Content = "Content" },
            };
            var blogs = new List<Blog>
            {
                new Blog { BlogId = 1, Url = "https://www.azureblue.io" },
                new Blog { BlogId = 2, Url = "https://foobar.com" }
            };

            builder.Entity<Blog>().HasData(blogs);
            builder.Entity<Post>().HasData(posts);
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
