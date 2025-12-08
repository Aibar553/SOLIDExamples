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
            return "Задача создана";
        if (task.Status == "InProgress")
            return "Задача в работе";
        if (task.Status == "Done")
            return "Задача завершена";

        return "Неизвестный статус";
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
            TaskStatus.New => "Задача создана",
            TaskStatus.InProgress => "Задача в работе",
            TaskStatus.Done => "Задача завершена",
            _ => "Неизвестный статус"
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
            total += inv.AmountUsd * rateUsdKzt; // снова та же формула
        }
        return total;
    }
}

public class ExportService
{
    public string ExportInvoice(Invoice inv, decimal rateUsdKzt)
    {
        var amountKzt = inv.AmountUsd * rateUsdKzt; // и снова
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
        // тут можно добавить комиссию, округление и т.п.
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
