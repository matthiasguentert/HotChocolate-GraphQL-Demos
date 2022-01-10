using SubscriptionsServer.Model;

namespace SubscriptionsServer.Resolvers
{
    public class Subscription
    {
        // A subscribing client can only listen to a single topic 

        [Subscribe]
        public Book BookAdded([EventMessage] Book book)
        {
            return book;
        }

        [Subscribe]
        public Book BookModified([EventMessage] Book book)
        {
            return book;
        }

        [Subscribe]
        public Book BookDeleted([EventMessage] Book book)
        {
            return book;
        }
    }
}
