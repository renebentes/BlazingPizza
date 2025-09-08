using BlazingPizza.Data;
using BlazingPizza.Model;

using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Services;

public sealed class OrderService(PizzaStoreContext context)
{
    public async Task<int> PlaceOrderAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        order.CreatedTime = DateTime.Now;

        foreach (var pizza in order.Pizzas)
        {
            pizza.SpecialId = pizza.Special.Id;
        }

        context.Orders.Attach(order);
        await context.SaveChangesAsync();
        return order.OrderId;
    }

    public async Task<List<OrderWithStatus>> GetOrdersAsync()
    {
        var orders = await context.Orders
        .Include(o => o.Pizzas).ThenInclude(p => p.Special)
        .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
        .OrderByDescending(o => o.CreatedTime)
        .ToListAsync();

        return [.. orders.Select(o => OrderWithStatus.FromOrder(o))];
    }
}
