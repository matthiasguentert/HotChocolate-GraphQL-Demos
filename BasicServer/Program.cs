using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Data;

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
    services.AddPooledDbContextFactory<DemoContext>(options =>
    {
        options.UseInMemoryDatabase("graphqldb");
        options.EnableSensitiveDataLogging();
    });

    services
        .AddGraphQLServer()
        .AddQueryType<Query>();
}
void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<IDbContextFactory<DemoContext>>().CreateDbContext();
        context.Database.EnsureCreated();
    }
}
void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    app.MapBananaCakePop("/graphql/ui").WithOptions(new GraphQLToolOptions
    {
        Enable = environment.IsDevelopment()
    });

    app.MapGraphQLHttp("/graphql").WithOptions(new GraphQLHttpOptions
    {
        EnableGetRequests = false
    });

    app.MapGraphQLSchema("/graphql/schema"); // /graph?sdl per default
}