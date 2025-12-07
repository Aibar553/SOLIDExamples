/*public class TxtFileExporter
{
    public void Export(string fileName, string content)
    {
        Console.WriteLine($"[TXT EXPORT] Сохраняем отчёт в файл {fileName}.txt");
        // Тут могла бы быть реальная запись в файл:
        // File.WriteAllText(fileName + ".txt", content);
        Console.WriteLine("Содержимое:");
        Console.WriteLine(content);
    }
}

public class ReportExporter
{
    private readonly TxtFileExporter _txtExporter = new TxtFileExporter();

    public void ExportDailyReport()
    {
        string fileName = "daily_report";
        string content = "Отчёт за день: ...";

        Console.WriteLine("Готовим ежедневный отчёт к экспорту...");

        _txtExporter.Export(fileName, content);

        Console.WriteLine("Ежедневный отчёт экспортирован.\n");
    }

    public void ExportMonthlyReport()
    {
        string fileName = "monthly_report";
        string content = "Отчёт за месяц: ...";

        Console.WriteLine("Готовим месячный отчёт к экспорту...");

        _txtExporter.Export(fileName, content);

        Console.WriteLine("Месячный отчёт экспортирован.\n");
    }
}

class Program
{
    static void Main()
    {
        var exporter = new ReportExporter();
        exporter.ExportDailyReport();
        exporter.ExportMonthlyReport();
    }
}
*/
public interface IFileExporter
{
    void Export(string fileName, string content);
}

public class TxtFileExporter : IFileExporter
{
    public void Export(string fileName, string content)
    {
        Console.WriteLine($"[TXT EXPORT] Сохраняем отчёт в файл {fileName}.txt");
        Console.WriteLine("Содержимое:");
        Console.WriteLine(content);
    }
}

public class CsvFileExporter : IFileExporter
{
    public void Export(string fileName, string content)
    {
        Console.WriteLine($"[CSV EXPORT] Сохраняем отчёт в файл {fileName}.csv");
        Console.WriteLine("Содержимое (в формате CSV):");
        Console.WriteLine(content);
    }
}

public class ReportExporter
{
    private readonly IFileExporter _fileExporter;

    public ReportExporter(IFileExporter fileExporter)
    {
        _fileExporter = fileExporter;
    }

    public void ExportDailyReport()
    {
        string fileName = "daily_report";
        string content = "Отчёт за день: ...";

        Console.WriteLine("Готовим ежедневный отчёт к экспорту...");

        _fileExporter.Export(fileName, content);

        Console.WriteLine("Ежедневный отчёт экспортирован.\n");
    }

    public void ExportMonthlyReport()
    {
        string fileName = "monthly_report";
        string content = "Отчёт за месяц: ...";

        Console.WriteLine("Готовим месячный отчёт к экспорту...");

        _fileExporter.Export(fileName, content);

        Console.WriteLine("Месячный отчёт экспортирован.\n");
    }
}

class Program
{
    static void Main()
    {
        // Экспорт в TXT
        IFileExporter txtExporter = new TxtFileExporter();
        var txtReportExporter = new ReportExporter(txtExporter);
        txtReportExporter.ExportDailyReport();

        // Экспорт в CSV
        IFileExporter csvExporter = new CsvFileExporter();
        var csvReportExporter = new ReportExporter(csvExporter);
        csvReportExporter.ExportMonthlyReport();
    }
}

/*public class Order
{
    public decimal Amount { get; set; }
}

public class HolidayDiscountCalculator
{
    public decimal CalculateDiscount(decimal amount)
    {
        // Новогодняя скидка 10%
        return amount * 0.10m;
    }
}

public class CheckoutService
{
    private readonly HolidayDiscountCalculator _discountCalculator = new HolidayDiscountCalculator();

    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Оформляем заказ. Сумма без скидки: {order.Amount}");

        decimal discount = _discountCalculator.CalculateDiscount(order.Amount);
        decimal finalAmount = order.Amount - discount;

        Console.WriteLine($"Скидка: {discount}");
        Console.WriteLine($"Итоговая сумма к оплате: {finalAmount}\n");
    }
}

class Program
{
    static void Main()
    {
        var order = new Order { Amount = 1000m };

        var checkout = new CheckoutService();
        checkout.ProcessOrder(order);
    }
}
*/
public class Order
{
    public decimal Amount { get; set; }
}

public interface IDiscountCalculator
{
    decimal CalculateDiscount(decimal amount);
}

public class HolidayDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount * 0.10m; // 10%
    }
}

public class StudentDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount * 0.15m; // 15%
    }
}

public class VipDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount * 0.20m; // 20%
    }
}

public class NoDiscountCalculator : IDiscountCalculator
{
    public decimal CalculateDiscount(decimal amount)
    {
        return 0m;
    }
}

public class CheckoutService
{
    private readonly IDiscountCalculator _discountCalculator;

    public CheckoutService(IDiscountCalculator discountCalculator)
    {
        _discountCalculator = discountCalculator;
    }

    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Оформляем заказ. Сумма без скидки: {order.Amount}");

        decimal discount = _discountCalculator.CalculateDiscount(order.Amount);
        decimal finalAmount = order.Amount - discount;

        Console.WriteLine($"Скидка: {discount}");
        Console.WriteLine($"Итоговая сумма к оплате: {finalAmount}\n");
    }
}

class Program
{
    static void Main()
    {
        var order = new Order { Amount = 2000m };

        IDiscountCalculator holiday = new HolidayDiscountCalculator();
        var holidayCheckout = new CheckoutService(holiday);
        holidayCheckout.ProcessOrder(order);

        IDiscountCalculator student = new StudentDiscountCalculator();
        var studentCheckout = new CheckoutService(student);
        studentCheckout.ProcessOrder(order);

        IDiscountCalculator vip = new VipDiscountCalculator();
        var vipCheckout = new CheckoutService(vip);
        vipCheckout.ProcessOrder(order);

        IDiscountCalculator noDiscount = new NoDiscountCalculator();
        var noDiscountCheckout = new CheckoutService(noDiscount);
        noDiscountCheckout.ProcessOrder(order);
    }
}
