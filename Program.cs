using BlazingPizza;
using BlazingPizza.Data;
using BlazingPizza.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder
    .Services
    .AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

builder
    .Services
    .AddScoped<PizzaStoreService>();

builder
    .Services
    .AddScoped<OrderState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();
