using BlazingPizza.Model;

using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<Pizza> Pizzas { get; set; }

    public DbSet<PizzaSpecial> Specials { get; set; }

    public DbSet<Topping> Toppings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.DeliveryAddress);

        modelBuilder
            .Entity<PizzaTopping>()
            .HasKey(pst => new { pst.PizzaId, pst.ToppingId });

        modelBuilder
            .Entity<PizzaTopping>()
            .HasOne<Pizza>()
            .WithMany(ps => ps.Toppings);

        modelBuilder
            .Entity<PizzaTopping>()
            .HasOne(pst => pst.Topping)
            .WithMany();
    }
}
