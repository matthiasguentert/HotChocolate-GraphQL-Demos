using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;
using StrawberryShake.Extensions;

namespace SubscriptionClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddBookClient()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7081/graphql"))
                .ConfigureWebSocketClient(c => c.Uri = new Uri("ws://localhost:7081/graphql"));

            var services = serviceCollection.BuildServiceProvider();

            var client = services.GetRequiredService<IBookClient>();

            var result = await client.BooksQuery.ExecuteAsync();
            result.EnsureNoErrors();

            foreach (var book in result.Data?.Books)
                Console.WriteLine(book.Title);

            var observable = client.OnBookAdded.Watch();
            observable.Subscribe(r =>
            {
                Console.WriteLine(r.Data?.OnBookAdded.Title);
            });

            Console.WriteLine("listening...");
            Console.ReadLine();
        }
    }
}
