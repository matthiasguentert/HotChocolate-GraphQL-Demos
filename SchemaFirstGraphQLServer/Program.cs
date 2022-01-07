using SchemaFirstGraphQLServer;
using SchemaFirstGraphQLServer.Data;
using SchemaFirstGraphQLServer.Resolvers;

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
    services.AddSingleton<PersonRepository>();

    services
        .AddGraphQLServer()
        .AddDocumentFromFile("./Schema/Schema.graphql")
        .AddQueryType<Query>()
        .AddResolver<GenderType>();
    
}

void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
}

void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    app.MapGraphQL();
}