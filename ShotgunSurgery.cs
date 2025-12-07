/*public class Order
{
    public string Status { get; set; } // "New", "Paid", "Cancelled"
}
public class NotificationService
{
    public void SendStatusEmail(Order order)
    {
        if (order.Status == "New")
        {
            Console.WriteLine("Email: Your order is received.");
        }
        else if (order.Status == "Paid")
        {
            Console.WriteLine("Email: Your payment was successful.");
        }
        else if (order.Status == "Cancelled")
        {
            Console.WriteLine("Email: Your order was cancelled.");
        }
    }
}

public class ReportService
{
    public string GetOrderLabel(Order order)
    {
        if (order.Status == "New")
        {
            return "New order";
        }
        else if (order.Status == "Paid")
        {
            return "Paid order";
        }
        else if (order.Status == "Cancelled")
        {
            return "Cancelled order";
        }

        return "Unknown";
    }
}*/
public enum OrderStatus
{
    New,
    Paid,
    Cancelled
}
public class Order
{
    public OrderStatus Status { get; set; }
}
public static class OrderStatusMessages
{
    public static string GetEmailText(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.New       => "Your order is received.",
            OrderStatus.Paid      => "Your payment was successful.",
            OrderStatus.Cancelled => "Your order was cancelled.",
            _                     => "Status changed."
        };
    }

    public static string GetLabel(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.New       => "New order",
            OrderStatus.Paid      => "Paid order",
            OrderStatus.Cancelled => "Cancelled order",
            _                     => "Unknown"
        };
    }
}
public class NotificationService
{
    public void SendStatusEmail(Order order)
    {
        string text = OrderStatusMessages.GetEmailText(order.Status);
        Console.WriteLine($"Email: {text}");
    }
}

public class ReportService
{
    public string GetOrderLabel(Order order)
    {
        return OrderStatusMessages.GetLabel(order.Status);
    }
}
