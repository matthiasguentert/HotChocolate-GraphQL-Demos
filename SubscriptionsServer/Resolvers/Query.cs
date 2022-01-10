using Microsoft.EntityFrameworkCore;
using SubscriptionsServer.Data;
using SubscriptionsServer.Model;

namespace SubscriptionsServer.Resolvers
{
    public class Query
    {
        [UseDbContext(typeof(BookContext))]
        public IQueryable<Book> GetBooks([ScopedService] BookContext context)
        {
            var books = context.Books.Include(b => b.Author);

            return books;
        }
    }
}
