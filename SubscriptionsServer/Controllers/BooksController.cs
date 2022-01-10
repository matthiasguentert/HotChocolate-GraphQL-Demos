using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionsServer.Data;
using SubscriptionsServer.Model;
using SubscriptionsServer.Resolvers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubscriptionsServer.Controllers
{
    [Route("books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly ITopicEventSender _sender;

        public BooksController(IDbContextFactory<BookContext> factory, ITopicEventSender sender)
        {
            _context = factory.CreateDbContext();
            _sender = sender;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var books = _context.Books
                .Include(b => b.Author);

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Single(b => b.BookId == id);

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostAsync([FromBody] Book book)
        {
            var result = _context.Books.Add(book);
            _context.SaveChanges();

            // Fire event
            await _sender.SendAsync(nameof(Subscription.OnBookAdded), book);

            return Ok(result.Entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Put(int id, [FromBody] Book book)
        {
            var bookFromDb = _context.Books
                .Include(b => b.Author)
                .Single(b => b.BookId == id);

            bookFromDb.Title = book.Title;
            bookFromDb.Author.Name = book.Author.Name;
            await _context.SaveChangesAsync();

            await _sender.SendAsync(nameof(Subscription.OnBookModified), book);

            return Ok(bookFromDb);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteAsync(int id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .Single(b => b.BookId == id);

            var result = _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            await _sender.SendAsync(nameof(Subscription.OnBookDeleted), book);

            return Ok(result.Entity);
        }
    }
}
