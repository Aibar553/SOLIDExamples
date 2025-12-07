/* class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; }
    public decimal Amount { get; set; }
}

// Конкретный репозиторий для SQL-базы
public class SqlOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine("[SQL] Сохраняем заказ в базу...");
        // Логика сохранения в БД
    }
}

// Конкретный сервис email-уведомлений
public class EmailNotificationService
{
    public void SendEmail(string to, string text)
    {
        Console.WriteLine($"[Email] To: {to}");
        Console.WriteLine("Открываем SMTP-соединение...");
        Console.WriteLine("Отправляем письмо: " + text);
    }
}

// Сервис оформления заказа (жёстко завязан на конкретные классы)
public class OrderService
{
    private readonly SqlOrderRepository _repository = new SqlOrderRepository();
    private readonly EmailNotificationService _emailService = new EmailNotificationService();

    public void PlaceOrder(Order order)
    {
        Console.WriteLine("Оформляем заказ...");

        // Сохранить заказ
        _repository.Save(order);

        // Отправить уведомление
        _emailService.SendEmail(order.CustomerEmail,
            $"Ваш заказ №{order.Id} на сумму {order.Amount} оформлен");

        Console.WriteLine("Заказ оформлен.\n");
    }
}

class Program
{
    static void Main()
    {
        var order = new Order
        {
            Id = 1,
            CustomerEmail = "user@example.com",
            Amount = 1500m
        };

        var service = new OrderService();
        service.PlaceOrder(order);
    }
}*/
public interface IOrderRepository
{
    void Save(Order order);
}
public interface INotificationService
{
    void Notify(string to, string message);
}
public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine("[SQL] Сохраняем заказ в базу...");
    }
}

public class EmailNotificationService : INotificationService
{
    public void Notify(string to, string message)
    {
        Console.WriteLine($"[Email] To: {to}");
        Console.WriteLine("Открываем SMTP-соединение...");
        Console.WriteLine("Отправляем письмо: " + message);
    }
}

// Допустим, позже ты добавишь:
public class SmsNotificationService : INotificationService
{
    public void Notify(string to, string message)
    {
        Console.WriteLine($"[SMS] To: {to}");
        Console.WriteLine("Подключаемся к SMS-шлюзу...");
        Console.WriteLine("Отправляем SMS: " + message);
    }
}
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notificationService;

    public OrderService(IOrderRepository repository, INotificationService notificationService)
    {
        _repository = repository;
        _notificationService = notificationService;
    }

    public void PlaceOrder(Order order)
    {
        Console.WriteLine("Оформляем заказ...");

        _repository.Save(order);

        _notificationService.Notify(
            order.CustomerEmail,
            $"Ваш заказ №{order.Id} на сумму {order.Amount} оформлен");

        Console.WriteLine("Заказ оформлен.\n");
    }
}


/*public class ConsoleLogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine("[INFO] " + message);
    }

    public void LogError(string message)
    {
        Console.WriteLine("[ERROR] " + message);
    }
}

public class ReportService
{
    private readonly ConsoleLogger _logger = new ConsoleLogger();

    public void GenerateDailyReport()
    {
        _logger.LogInfo("Начали генерацию ежедневного отчёта");

        // Какая-то логика генерации
        Console.WriteLine("Генерируем отчёт за день...");

        _logger.LogInfo("Ежедневный отчёт успешно сгенерирован");
    }

    public void GenerateMonthlyReport()
    {
        _logger.LogInfo("Начали генерацию месячного отчёта");

        // Какая-то логика генерации
        Console.WriteLine("Генерируем отчёт за месяц...");

        _logger.LogInfo("Месячный отчёт успешно сгенерирован");
    }
}

class Program
{
    static void Main()
    {
        var service = new ReportService();
        service.GenerateDailyReport();
        service.GenerateMonthlyReport();
    }
}
*/


public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
}

// Консольный логгер (реализация интерфейса)
public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine("[INFO] " + message);
    }

    public void LogError(string message)
    {
        Console.WriteLine("[ERROR] " + message);
    }
}

public class ReportService
{
    private readonly ILogger _logger;

    // Принимаем абстракцию, а не конкретный класс
    public ReportService(ILogger logger)
    {
        _logger = logger;
    }

    public void GenerateDailyReport()
    {
        _logger.LogInfo("Начали генерацию ежедневного отчёта");

        // Какая-то логика генерации
        Console.WriteLine("Генерируем отчёт за день...");

        _logger.LogInfo("Ежедневный отчёт успешно сгенерирован");
    }

    public void GenerateMonthlyReport()
    {
        _logger.LogInfo("Начали генерацию месячного отчёта");

        // Какая-то логика генерации
        Console.WriteLine("Генерируем отчёт за месяц...");

        _logger.LogInfo("Месячный отчёт успешно сгенерирован");
    }
}