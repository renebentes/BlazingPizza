using BlazingPizza.Services;

namespace BlazingPizza.Pages;

public partial class Index(
    PizzaStoreService pizzaStoreService,
    OrderState orderState)
{
    private List<PizzaSpecial> _specials = [];

    public Order Order => orderState.Order;

    protected override async Task OnInitializedAsync()
        => _specials = await pizzaStoreService.GetPizzaSpecialsAsync();
}
