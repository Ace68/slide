var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

This is not something that we're used to."
"Previously the default folder structure of a .NET Web API project consisting of a "
"Program.cs file (with the Main method to run the API) and a "
"Startup.cs file (with the ConfigureServices and Configure methods to configure the API)."
"The project also includes a Controllers folder with a controller file, 
"containing the endpoints of the application."

WebApplication
│   appsettings.json
│   Program.cs
│   Startup.cs
│   WebApplication.csproj
│
├───Configuration/Extensions
│       ServiceCollection.cs
│       ApplicationBuilder.cs
├───Controllers
│       ...
├───Commands (Domains)
│       ...
├───Queries (ReadModel)
│       ...
├───Models/DTOs
│       ...
├───Interfaces
│       ...
├───Infrastructure
│   


// Single File API
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IPayment, PaymentService>();

var app = builder.Build();
app.MapPost("/carts", () => {
    ...
});
app.MapPut("/carts/{cartId}", () => {
    ...
});
app.MapGet("/orders", () => {
    ...
});
app.MapPost("/orders", () => {
    ...
});

app.Run();

//Controllers
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IPayment, PaymentService>();

var app = builder.Build();
app.MapControllers();
app.Run();

// Module
public static class OrdersModule
{
    public static IServiceCollection RegisterOrdersModule(this IServiceCollection services)
    {
        services.AddSingleton(new OrderConfig());
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<ICustomersRepository, CustomersRepository>();
        services.AddScoped<IPayment, PaymentService>();
        return services;
    }

    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/orders", () => {
            ...
        });
        endpoints.MapPost("/orders", () => {
            ...
        });
        return endpoints;
    }
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterOrdersModule();

var app = builder.Build();
app.MapOrdersEndpoints();
app.Run();

// IModule
public interface IModule
{
    IServiceCollection RegisterModule(IServiceCollection builder);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}

public static class ModuleExtensions
{
    // this could also be added into the DI container
    static readonly List<IModule> registeredModules = new List<IModule>();

    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModule(builder.Services);
            registeredModules.Add(module);
        }

        return builder;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        foreach (var module in registeredModules)
        {
            module.MapEndpoints(app);
        }
        return app;
    }

    private static IEnumerable<IModule> DiscoverModules()
    {
        return typeof(IModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
            .Select(Activator.CreateInstance)
            .Cast<IModule>();
    }
}