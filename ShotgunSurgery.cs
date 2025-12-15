/*public class Order
{
    public decimal Total { get; set; }
}

public class OrderController
{
    public decimal GetFinalAmount(Order order)
    {
        // ������ 10% ���� total > 1000
        if (order.Total > 1000)
            return order.Total * 0.9m;

        return order.Total;
    }
}

public class ReportService
{
    public decimal GetDiscountedAmount(Order order)
    {
        // �� �� ������
        if (order.Total > 1000)
            return order.Total * 0.9m;

        return order.Total;
    }
}

public class PromoService
{
    public decimal PreviewDiscount(Order order)
    {
        // ����� �� �� ������
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
            return Total * 0.85m; // 15% ������

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
        // ����� 10%
        var tax = e.GrossSalary * 0.10m;
        return e.GrossSalary - tax;
    }
}

public class ReportGenerator
{
    public decimal GetNetForReport(Employee e)
    {
        // ��� �� ������
        var tax = e.GrossSalary * 0.10m;
        return e.GrossSalary - tax;
    }
}

public class BonusService
{
    public decimal GetNetAfterBonus(Employee e, decimal bonus)
    {
        var gross = e.GrossSalary + bonus;
        // ����� �� ��
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


/*public class OrderController
{
    public decimal GetTotal(decimal amount, bool isVip)
    {
        if (isVip)
            amount *= 0.9m;   // 10% ������

        return amount;
    }
}

public class DiscountBanner
{
    public string GetMessage(bool isVip)
    {
        if (isVip)
            return "You have 10% VIP discount!";

        return "Become VIP to get 10% discount!";
    }
}

public class EmailService
{
    public string BuildVipOffer(bool isVip)
    {
        if (!isVip)
            return "Become VIP and get 10% discount on all orders!";

        return "Thank you for being VIP! Enjoy your 10% discount.";
    }
}
*/
public interface IVipPolicy
{
    decimal ApplyDiscount(decimal discount, bool isVip);
    string GetBannerMessage(bool isVip);
    string GetEmailOffer(bool isVip);
}
public class VipPolicy : IVipPolicy
{
    private const decimal VipDiscount = 0.10m;

    public decimal ApplyDiscount(decimal amount, bool isVip)
    {
        return isVip ? amount * (1 - VipDiscount) : amount;
    }

    public string GetBannerMessage(bool isVip)
    {
        return isVip
            ? "You have 10% VIP discount!"
            : "Become VIP to get 10% discount!";
    }

    public string GetEmailOffer(bool isVip)
    {
        return isVip
            ? "Thank you for being VIP! Enjoy your 10% discount."
            : "Become VIP and get 10% discount on all orders!";
    }
}
public class OrderController
{
    private readonly IVipPolicy _vip;

    public OrderController(IVipPolicy vip)
    {
        _vip = vip;
    }

    public decimal GetTotal(decimal amount, bool isVip)
    {
        return _vip.ApplyDiscount(amount, isVip);
    }
}

public class DiscountBanner
{
    private readonly IVipPolicy _vip;

    public DiscountBanner(IVipPolicy vip)
    {
        _vip = vip;
    }

    public string GetMessage(bool isVip)
    {
        return _vip.GetBannerMessage(isVip);
    }
}

public class EmailService
{
    private readonly IVipPolicy _vip;

    public EmailService(IVipPolicy vip)
    {
        _vip = vip;
    }

    public string BuildVipOffer(bool isVip)
    {
        return _vip.GetEmailOffer(isVip);
    }
}
/*
Налоги по странам (VAT/подоходный/продажный) — Strategy + Provider
public class InvoiceService {
    public decimal Vat(decimal net, string c) =>
        c switch { "KZ"=>net*0.12m, "US"=>0m, "EU"=>net*0.20m, _=>throw new() };
}
public class PayrollService {
    public decimal IncomeTax(decimal gross, string c) =>
        c switch { "KZ"=>gross*0.10m, "US"=>gross*0.22m, "EU"=>gross*0.15m, _=>throw new() };
}
public class PricingService {
    public decimal ApplySalesTax(decimal price, string c) =>
        c switch { "KZ"=>price, "US"=>price*1.08m, "EU"=>price*1.20m, _=>throw new() };
}
*/
public interface ITaxPolicy
{
    decimal Vat(decimal net); 
    decimal IncomeTax(decimal gross); 
    decimal SalesMult(decimal price);
}
public interface ITaxPolicyProvider { ITaxPolicy For(string country); }

public sealed class KzTax : ITaxPolicy {
    public decimal Vat(decimal net)=>net*0.12m;
    public decimal IncomeTax(decimal gross)=>gross*0.10m;
    public decimal SalesMult(decimal price)=>1.00m;
}
public sealed class UsTax : ITaxPolicy {
    public decimal Vat(decimal net)=>0m;
    public decimal IncomeTax(decimal gross)=>gross*0.22m;
    public decimal SalesMult(decimal price)=>1.08m;
}
public sealed class EuTax : ITaxPolicy {
    public decimal Vat(decimal net)=>net*0.20m;
    public decimal IncomeTax(decimal gross)=>gross*0.15m;
    public decimal SalesMult(decimal price)=>1.20m;
}

public sealed class TaxProvider : ITaxPolicyProvider {
    private readonly Dictionary<string,ITaxPolicy> _map;
    public TaxProvider(IEnumerable<(string code, ITaxPolicy p)> items) =>
        _map = items.ToDictionary(x=>x.code.ToUpperInvariant(), x=>x.p);
    public ITaxPolicy For(string country) => _map[country.ToUpperInvariant()];
}

public class InvoiceService {
    private readonly ITaxPolicyProvider _p; public InvoiceService(ITaxPolicyProvider p)=>_p=p;
    public decimal Vat(decimal net, string c)=>_p.For(c).Vat(net);
}