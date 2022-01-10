using Microsoft.EntityFrameworkCore;
using SubscriptionsServer.Model;

namespace SubscriptionsServer.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Jon Skeet" }
            };

            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Book A", AuthorId = 1 }
            };

            builder.Entity<Author>().HasData(authors);
            builder.Entity<Book>().HasData(books);
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
