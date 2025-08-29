using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<PizzaSpecial> Specials { get; set; }
}
