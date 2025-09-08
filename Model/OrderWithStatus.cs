namespace BlazingPizza.Model;

public class OrderWithStatus
{
    public static readonly TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch
    public static readonly TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);

    public bool IsDelivered => StatusText == "Delivered";

    public Order Order { get; set; } = null!;

    public string StatusText { get; set; } = string.Empty;

    public static OrderWithStatus FromOrder(Order order)
    {
        // To simulate a real backend process, we fake status updates based on the amount
        // of time since the order was placed
        string statusText;
        var dispatchTime = order.CreatedTime.Add(PreparationDuration);

        statusText = DateTime.Now < dispatchTime
            ? "Preparing"
            : (DateTime.Now < dispatchTime + DeliveryDuration
            ? "Out for delivery"
            : "Delivered");

        return new OrderWithStatus
        {
            Order = order,
            StatusText = statusText
        };
    }
}
