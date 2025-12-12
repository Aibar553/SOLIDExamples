/*public enum PaymentType
{
    Card,
    PayPal,
    Crypto   // 1) добавили новый тип
}

public class PaymentService
{
    public void Pay(PaymentType type, decimal amount)
    {
        Console.WriteLine($"Начинаем оплату на сумму {amount}...");

        if (type == PaymentType.Card)
        {
            Console.WriteLine("Проверка карты...");
            Console.WriteLine("Списание денег с банковской карты");
        }
        else if (type == PaymentType.PayPal)
        {
            Console.WriteLine("Редирект на PayPal...");
            Console.WriteLine("Подтверждение оплаты через PayPal");
        }
        else if (type == PaymentType.Crypto)   // 2) ещё один if
        {
            Console.WriteLine("Подключение к крипто-кошельку...");
            Console.WriteLine("Подтверждение транзакции в блокчейне");
        }
        else
        {
            Console.WriteLine("Неизвестный тип оплаты!");
        }

        Console.WriteLine("Оплата завершена.\n");
    }
}*/
public interface IPaymentMethod
{
    void Pay(decimal amount);
}
public class CardPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"[Card] Оплата {amount}");
        Console.WriteLine("Проверка карты...");
        Console.WriteLine("Списание денег с банковской карты");
    }
}

public class PayPalPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"[PayPal] Оплата {amount}");
        Console.WriteLine("Редирект на PayPal...");
        Console.WriteLine("Подтверждение оплаты через PayPal");
    }
}

// Новая оплата криптой — добавляем новый класс, не трогая старые:
public class CryptoPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"[Crypto] Оплата {amount}");
        Console.WriteLine("Подключение к крипто-кошельку...");
        Console.WriteLine("Подтверждение транзакции в блокчейне");
    }
}
public class PaymentService
{
    private readonly IPaymentMethod _paymentMethod;

    public PaymentService(IPaymentMethod paymentMethod)
    {
        _paymentMethod = paymentMethod;
    }

    public void Pay(decimal amount)
    {
        Console.WriteLine("Начинаем оплату...");
        _paymentMethod.Pay(amount);   // Делегируем стратегию
        Console.WriteLine("Оплата завершена.\n");
    }
}
class Program
{
    static void Main()
    {
        // Оплата картой
        var cardService = new PaymentService(new CardPayment());
        cardService.Pay(1000m);

        // Оплата через PayPal
        var payPalService = new PaymentService(new PayPalPayment());
        payPalService.Pay(500m);

        // Оплата криптовалютой
        var cryptoService = new PaymentService(new CryptoPayment());
        cryptoService.Pay(0.05m);
    }
}

public interface INotificationMethod
{
    void Send(string to, string message);
}
public class SMSNotification : INotificationMethod
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[SMS] To: {to}");
        Console.WriteLine("Подключаемся к SMS-шлюзу...");
        Console.WriteLine("Отправляем SMS: " + message);
    }
}
public class EmailNotification : INotificationMethod
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[Email] To: {to}");
        Console.WriteLine("Открываем SMTP-соединение...");
        Console.WriteLine("Отправляем email: " + message);
    }
}
public class PushNotification : INotificationMethod
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"[Push] To: {to}");
        Console.WriteLine("PUSH-соединение...");
        Console.WriteLine("Отправляем push: " + message);
    }
}
public class NotificationService
{
    private readonly INotificationMethod _notificationMethod;

    public NotificationService(INotificationMethod notificationMethod)
    {
        _notificationMethod = notificationMethod;
    }
    public void Send(string to, string message)
    {
        Console.WriteLine("Отправляем уведомление" + to);
        _notificationMethod.Send(to, message);
        Console.WriteLine("Отправлено ");
    }
}
class Program
{
    static void Main()
    {
        var sms = new NotificationService(new SMSNotification());
        sms.Send("Nigga", "Shit");
        var email = new NotificationService(new EmailNotification());
        email.Send("Barcelona", "WINNER UCL");
        var push = new NotificationService(new PushNotification());
        push.Send("Nigga", "SS");
    }
}



/*public class DiscountCalculator
{
    public decimal GetDiscount(string customerStatus, decimal amount, int itemsCount)
    {
        decimal discount = 0m;

        if (customerStatus == "New")
        {
            if (itemsCount > 3)
                discount = amount * 0.05m;
        }
        else if (customerStatus == "Regular")
        {
            discount = amount * 0.10m;
            if (itemsCount > 5)
                discount += amount * 0.05m;
        }
        else if (customerStatus == "VIP")
        {
            discount = amount * 0.20m;
            if (amount > 1000m)
                discount += amount * 0.10m;
        }

        return discount;
    }
}
*/
public interface IDiscountMethod
{
    public decimal GetDiscount(decimal amount);
}
public class NewStatus : IDiscountMethod
{
    public decimal GetDiscount(decimal amount)
    {
        decimal discount = 0m;
        discount = amount * 0.05m;
        return discount;
    }
}
public class RegularStatus : IDiscountMethod
{
    public decimal GetDiscount(decimal amount)
    {
        decimal discount = 0.10m;
        discount = amount * 0.05m;
        return discount;
    }
}
public class VIPStatus : IDiscountMethod
{
    public decimal GetDiscount(decimal amount)
    {
        decimal discount = 0.20m;
        discount = amount * 0.05m;
        return discount;
    }
}