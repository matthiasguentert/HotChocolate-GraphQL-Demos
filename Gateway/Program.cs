using Gateway;
using HotChocolate.AspNetCore;

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
    services
        .AddGraphQLServer()
        .AddQueryType(d => d.Name("Query"))
        .AddRemoteSchema(WellKnownSchemaNames.Accounts, ignoreRootTypes: true)
        .AddRemoteSchema(WellKnownSchemaNames.Products, ignoreRootTypes: true)
        .AddRemoteSchema(WellKnownSchemaNames.Inventory, ignoreRootTypes: true)
        .AddRemoteSchema(WellKnownSchemaNames.Reviews, ignoreRootTypes: true)
        .AddTypeExtensionsFromFile("./Stiching.graphql");

    services.AddHttpClient(WellKnownSchemaNames.Accounts, c => c.BaseAddress = new Uri("https://localhost:7001/graphql"));
    services.AddHttpClient(WellKnownSchemaNames.Products, c => c.BaseAddress = new Uri("https://localhost:7002/graphql"));
    services.AddHttpClient(WellKnownSchemaNames.Inventory, c => c.BaseAddress = new Uri("https://localhost:7003/graphql"));
    services.AddHttpClient(WellKnownSchemaNames.Reviews, c => c.BaseAddress = new Uri("https://localhost:7004/graphql"));
}

void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
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

    //app.MapGraphQL();
}