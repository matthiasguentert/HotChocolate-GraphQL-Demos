using Microsoft.EntityFrameworkCore;
using SubscriptionsServer.Data;
using SubscriptionsServer.Resolvers;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration);
ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app, app.Services, app.Environment);
ConfigureEndpoints(app, app.Services, app.Environment);

app.Run();

void ConfigureConfiguration(ConfigurationManager configuration) { }
void ConfigureServices(IServiceCollection services)
{
    services.AddPooledDbContextFactory<BookContext>(options =>
    {
        options.UseInMemoryDatabase("graphqldb");
        options.EnableSensitiveDataLogging();
    });

    services.AddControllers();

    services
        .AddGraphQLServer()
        .AddInMemorySubscriptions()
        .AddSubscriptionType<Subscription>()
        .AddQueryType<Query>();
}

void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    app.UseDeveloperExceptionPage();

    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<BookContext>>().CreateDbContext();
        context.Database.EnsureCreated();
    }

    app.UseRouting();
    app.UseWebSockets();
}

void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    app.MapControllers();
    app.MapGraphQL();
}