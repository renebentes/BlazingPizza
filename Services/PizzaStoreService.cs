using BlazingPizza.Data;
using BlazingPizza.Model;

using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Services;

public class PizzaStoreService(PizzaStoreContext dbContext)
{
    public async Task<List<PizzaSpecial>> GetPizzaSpecialsAsync()
        => [.. (await dbContext
            .Specials
            .AsNoTracking()
            .ToListAsync())
            .OrderByDescending(s => s.BasePrice)];
}
