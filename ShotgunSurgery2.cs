/*public class TaskItem
{
    public string Status { get; set; } // "New", "InProgress", "Done"
}

public class TaskController
{
    public bool CanEdit(TaskItem task)
    {
        return task.Status == "New" || task.Status == "InProgress";
    }
}

public class TaskReportService
{
    public bool IsCompleted(TaskItem task)
    {
        return task.Status == "Done";
    }
}

public class NotificationService
{
    public string GetStatusText(TaskItem task)
    {
        if (task.Status == "New")
            return "������ �������";
        if (task.Status == "InProgress")
            return "������ � ������";
        if (task.Status == "Done")
            return "������ ���������";

        return "����������� ������";
    }
}*/
public enum TaskStatus
{
    New,
    InProgress,
    Done
}
public class TaskItem
{
    public TaskStatus Status { get; set; }
    public TaskItem()
    {
        Status = TaskStatus.New;
    }
    public bool CanEdit() => Status == TaskStatus.New
        || Status == TaskStatus.InProgress;

    public bool IsCompleted() => Status == TaskStatus.Done;
    public string GetStatusText()
    {
        return Status switch
        {
            TaskStatus.New => "������ �������",
            TaskStatus.InProgress => "������ � ������",
            TaskStatus.Done => "������ ���������",
            _ => "����������� ������"
        };
    }
    public void Start() => Status = TaskStatus.InProgress;
    public void Finish() => Status = TaskStatus.Done;
}
public class TaskController
{
    public bool CanEdit(TaskItem task) => task.CanEdit();
}

public class TaskReportService
{
    public bool IsCompleted(TaskItem task) => task.IsCompleted();
}

public class NotificationService
{
    public string GetStatusText(TaskItem task) => task.GetStatusText();
}

/*public class Invoice
{
    public decimal AmountUsd { get; set; }
}

public class InvoiceService
{
    public decimal GetAmountInKzt(Invoice inv, decimal rateUsdKzt)
    {
        return inv.AmountUsd * rateUsdKzt;
    }
}

public class ReportService
{
    public decimal GetTotalInKzt(IEnumerable<Invoice> invoices, decimal rateUsdKzt)
    {
        decimal total = 0;
        foreach (var inv in invoices)
        {
            total += inv.AmountUsd * rateUsdKzt; // ����� �� �� �������
        }
        return total;
    }
}

public class ExportService
{
    public string ExportInvoice(Invoice inv, decimal rateUsdKzt)
    {
        var amountKzt = inv.AmountUsd * rateUsdKzt; // � �����
        return $"Invoice: {inv.AmountUsd} USD ({amountKzt} KZT)";
    }
}*/
public class Invoice
{
    public decimal AmountUsd { get; }

    public Invoice(decimal amountUsd)
    {
        if (amountUsd <= 0)
            throw new ArgumentException("Amount must be positive");

        AmountUsd = amountUsd;
    }

    public decimal ToKzt(decimal rateUsdKzt)
    {
        // ��� ����� �������� ��������, ���������� � �.�.
        return Math.Round(AmountUsd * rateUsdKzt, 2);
    }
}

public class InvoiceService
{
    public decimal GetAmountInKzt(Invoice inv, decimal rateUsdKzt)
        => inv.ToKzt(rateUsdKzt);
}

public class ReportService
{
    public decimal GetTotalInKzt(IEnumerable<Invoice> invoices, decimal rateUsdKzt)
    {
        return invoices.Sum(i => i.ToKzt(rateUsdKzt));
    }
}

public class ExportService
{
    public string ExportInvoice(Invoice inv, decimal rateUsdKzt)
    {
        var amountKzt = inv.ToKzt(rateUsdKzt);
        return $"Invoice: {inv.AmountUsd} USD ({amountKzt} KZT)";
    }
}


/*public class OrderService
{
    public decimal CalculateTotal(decimal amount)
    {
        var tax = amount * 0.12m;   // ��� 12%
        return amount + tax;
    }
}

public class InvoiceService
{
    public decimal CalculateInvoiceTotal(decimal amount)
    {
        var tax = amount * 0.12m;   // ��� 12%
        return amount + tax;
    }
}

public class ReportingService
{
    public decimal CalculateTax(decimal amount)
    {
        return amount * 0.12m;      // ��� 12%
    }
}*/

public interface ITaxService
{
    decimal CalculateTax(decimal amount);
}
public class TaxService : ITaxService
{
    private const decimal TaxRate = 0.12m;
    public decimal CalculateTax(decimal amount)
    {
        return TaxRate * amount;
    }
}
public class OrderService
{
    private readonly ITaxService _taxService;
    public OrderService(ITaxService taxService)
    {
        _taxService = taxService;
    }
    public decimal CalculateTax(decimal amount) 
    {
        var tax = _taxService.CalculateTax(amount);
        return tax + amount;
    }
}
public class InvoiceService
{
    private readonly ITaxService _taxService;
    public InvoiceService(ITaxService taxService)
    {
        _taxService = taxService;
    }
    public decimal CalculateInvoiceTotal(decimal amount)
    {
        var tax = _taxService.CalculateTax(amount);
        return amount + tax;
    }
}
public class ReportingService
{
    private readonly ITaxService _taxService;
    public ReportingService(ITaxService taxService)
    {
        _taxService = taxService;
    }
    public decimal CalculateTax(decimal amount)
    {
        return _taxService.CalculateTax(amount);
    }
}

/*
// OrdersController
if (user.Role == "Admin" || user.Role == "Manager") { /* approve  } else return Forbid();

// ReportsController
//if (user.Role == "Admin") Export(); else return Forbid();

// UserService
/*bool CanEdit(User u) => u.Role == "Admin" || u.Role == "Editor";
}*/

public enum Permission { ApproveOrder, ExportReport, EditUser }

public interface IAuthorization
{
    bool Has(User user, Permission p);
}
public sealed class RoleBasedAuth : IAuthorization
{
    private static readonly Dictionary<string, Permission[]> Map = new()
    {
        ["Admin"]   = new[] { Permission.ApproveOrder, Permission.ExportReport, Permission.EditUser },
        ["Manager"] = new[] { Permission.ApproveOrder },
        ["Editor"]  = new[] { Permission.EditUser }
    };

    public bool Has(User u, Permission p) => Map.TryGetValue(u.Role, out var ps) && ps.Contains(p);
}
/*string BuildNumber(DateTime d, int seq) => $"{d:yyyy-MM}/INV-{seq:D5}"; // UI
// ...
var num = $"{DateTime.UtcNow:yyyy-MM}/INV-{seq:D5}"; // Service
// ...
cell.Value = $"{date:yyyy-MM}/INV-{id:D5}"; // Report
*/

public interface IInvoiceNumbering { string Next(DateTime utcNow, int seq); }

public sealed class YearMonthNumbering : IInvoiceNumbering
{
    public string Next(DateTime now, int seq) => $"{now:yyyy-MM}/INV-{seq:D5}";
}

public sealed class NumberingService
{
    private readonly IInvoiceNumbering _strategy;
    public NumberingService(IInvoiceNumbering s) => _strategy = s;
    public string Next(int seq) => _strategy.Next(DateTime.UtcNow, seq);
}

// Везде используем NumberingService → меняем формат в одном месте


/*
Одинаковое сообщение об ошибке везде

return "NotFound";
throw new Exception("NotFound");
logger.Log("NotFound");
*/
public static class Messages
{
    public const string NotFound = "NotFound";
}

/*
Константа лимита дублируется
❌
public class A { public const int Max = 100; }
public class B { public const int Max = 100; }
*/
public static class Maximum
{
    public const int Max = 100;
}

/*
Формат даты копипастом
❌
d.ToString("dd/MM/yyyy"); // в 10 файлах
*/

public static class DataSet
{
    public const string Default = "dd/MM/yyyy";
}

/*
“VIP” как magic string везде
❌
if (type == "VIP") price *= 0.8m;
*/
public enum CustomerType
{
    Regular, Vip
}

/*
bool Valid(string e) => e.Contains("@"); // в разных сервисах
*/

public interface IEmailValidator
{
    bool IsValid(string e);
}


/*
Feature flags: “новый флаг” требует правок в UI + API + worker
❌ До (строки флага везде)
if (flags.Contains("NewMenu")) ShowNewMenu();
if (flags.Contains("NewMenu")) return Ok(NewMenu());
if (flags.Contains("NewMenu")) EnqueueNewMenuJob();
*/
public enum Feature
{
    NewMenu, FastCheckout
}
public interface IFeatureFlags
{
    bool IsOn(Feature f);
}

/*
Endpoint URLs: смена base URL ломает кучу клиентов
❌ До
await http.GetStringAsync("https://api.v1.company.com/users");
await http.GetStringAsync("https://api.v1.company.com/orders");
*/

public interface IApiRoutes { string Users(); string Orders(); }
public class ApiRoutes : IApiRoutes
{
    private readonly string _base;
    public ApiRoutes(string baseUrl) => _base = baseUrl.TrimEnd('/');
    public string Users() => $"{_base}/users";
    public string Orders() => $"{_base}/orders";
}

/*JSON contract: переименовали поле → правим 10 парсеров
❌ До
var id = doc["user_id"].GetInt32();*/

public sealed class UserDto
{
    [System.Text.Json.Serialization.JsonPropertyName("user_id")]
    public int UserId {get; set;}
}

/*Нормализация строки (trim/upper) размазана по репозиториям
❌ До
var city = input.Trim().ToUpperInvariant(); */
public sealed class CityName
{
    public string Value {get;}
    public CityName(string raw) => Value = raw?.Trim().ToUpperInvariant() 
        ?? throw new ArgumentNullException();
}

/*“Статусы заказа” строками: добавили новый статус → правки повсюду
❌ До
if (status == "Paid") Ship();
if (status == "Paid") ShowBadge("PAID");
if (status == "Paid") WriteAudit("paid");*/

public enum Status
{
    New, Paid, Shipped, Cancelled
}