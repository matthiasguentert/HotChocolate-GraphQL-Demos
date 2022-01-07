using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1
{
    public class Query
    {
        [UseDbContext(typeof(DemoContext))]
        public IQueryable<Blog> GetBlogs([ScopedService] DemoContext context)
        {
            var blogs = context.Blogs.Include(b => b.Posts);

            return blogs;
        }

        [UseDbContext(typeof(DemoContext))]
        public Task<Blog> GetBlogById([ScopedService] DemoContext context, int id)
        {
            var blog = context.Blogs
                .Include(b => b.Posts)
                .SingleAsync(b => b.BlogId == id);

            return blog;
        }

        [UseDbContext(typeof(DemoContext))]
        public IQueryable<Post> GetPosts([ScopedService] DemoContext context)
        {
            var posts = context.Posts.AsQueryable();

            return posts;
        }

        [UseDbContext(typeof(DemoContext))]
        public Task<Post> GetPostById([ScopedService] DemoContext context, int id)
        {
            var post = context.Posts.SingleAsync(p => p.PostId == id);  

            return post;
        }
    }
}
