using Accounts;
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
        .AddSingleton<UserRepository>()
        .AddGraphQLServer()
        .AddQueryType<Query>();
}

void ConfigureMiddleware(IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    if (environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
}

void ConfigureEndpoints(IEndpointRouteBuilder app, IServiceProvider services, IWebHostEnvironment environment)
{
    //app.MapGraphQLHttp("/graphql").WithOptions(new GraphQLHttpOptions
    //{
    //    EnableGetRequests = false
    //});

    //app.MapGraphQLSchema("/graphql/schema");

    app.MapGraphQL();
}