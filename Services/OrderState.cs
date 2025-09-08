using BlazingPizza.Model;

namespace BlazingPizza.Services;

public class OrderState
{
    public Pizza ConfiguringPizza { get; private set; } = new();
    public Order Order { get; private set; } = new();
    public bool ShowingConfigureDialog { get; private set; }

    public void CancelConfigurePizzaDialog()
    {
        ConfiguringPizza = new();

        ShowingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog()
    {
        Order.Pizzas.Add(ConfiguringPizza);
        ConfiguringPizza = new();

        ShowingConfigureDialog = false;
    }

    public void RemoveConfiguredPizza(Pizza pizza)
        => Order.Pizzas.Remove(pizza);

    public void ResetOrder()
        => Order = new Order();

    public void ShowConfigurePizzaDialog(PizzaSpecial special)
    {
        ConfiguringPizza = new Pizza()
        {
            Special = special,
            SpecialId = special.Id,
            Size = Pizza.DefaultSize,
            Toppings = [],
        };

        ShowingConfigureDialog = true;
    }
}
