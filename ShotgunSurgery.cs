/*public class Order
{
    public decimal Total { get; set; }
}

public class OrderController
{
    public decimal GetFinalAmount(Order order)
    {
        // скидка 10% если total > 1000
        if (order.Total > 1000)
            return order.Total * 0.9m;

        return order.Total;
    }
}

public class ReportService
{
    public decimal GetDiscountedAmount(Order order)
    {
        // та же логика
        if (order.Total > 1000)
            return order.Total * 0.9m;

        return order.Total;
    }
}

public class PromoService
{
    public decimal PreviewDiscount(Order order)
    {
        // снова та же логика
        if (order.Total > 1000)
            return order.Total * 0.9m;

        return order.Total;
    }
}*/
public class Order
{
    public decimal Total { get; set; }

    public decimal GetDiscountedTotal()
    {
        if (Total > 2000)
            return Total * 0.85m; // 15% скидка

        return Total;
    }
}

public class OrderController
{
    public decimal GetFinalAmount(Order order) => order.GetDiscountedTotal();
}

public class ReportService
{
    public decimal GetDiscountedAmount(Order order) => order.GetDiscountedTotal();
}

public class PromoService
{
    public decimal PreviewDiscount(Order order) => order.GetDiscountedTotal();
}
/*public class Employee
{
    public decimal GrossSalary { get; set; }
}

public class PayrollService
{
    public decimal CalculateNet(Employee e)
    {
        // налог 10%
        var tax = e.GrossSalary * 0.10m;
        return e.GrossSalary - tax;
    }
}

public class ReportGenerator
{
    public decimal GetNetForReport(Employee e)
    {
        // тот же расчёт
        var tax = e.GrossSalary * 0.10m;
        return e.GrossSalary - tax;
    }
}

public class BonusService
{
    public decimal GetNetAfterBonus(Employee e, decimal bonus)
    {
        var gross = e.GrossSalary + bonus;
        // снова то же
        var tax = gross * 0.10m;
        return gross - tax;
    }
}
*/
public class Employee
{
    public decimal GrossSalary { get; set; }

    public decimal GrossSalaryTotal()
    {
        var tax = GrossSalary * 0.10m;
        return GrossSalary - tax;
    }
    public decimal GrossSalaryTotalBonus(decimal bonus)
    {
        var gross = GrossSalary + bonus;
        var tax = gross * 0.10m;
        return gross - tax;
    }
}
public class TaxSettings
{
    public const decimal DefaultTaxRate = 0.10m;
}
public class PayrollService
{
    public decimal CalculateNet(Employee e) => e.GrossSalaryTotal();
}
public class ReportGenerator
{
    public decimal GetNetForReport(Employee e) => e.GrossSalaryTotal();
}
public class BonusService
{
    public decimal GetNetAfterBonus(Employee e) => e.GrossSalaryTotalBonus(bonus);
}
