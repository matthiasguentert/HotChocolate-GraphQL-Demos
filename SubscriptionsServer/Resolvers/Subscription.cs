using SubscriptionsServer.Model;

namespace SubscriptionsServer.Resolvers
{
    public class Subscription
    {
        // A subscribing client can only listen to a single topic 

        [Subscribe]
        public Book OnBookAdded([EventMessage] Book book)
        {
            return book;
        }

        [Subscribe]
        public Book OnBookModified([EventMessage] Book book)
        {
            return book;
        }

        [Subscribe]
        public Book OnBookDeleted([EventMessage] Book book)
        {
            return book;
        }
    }
}
