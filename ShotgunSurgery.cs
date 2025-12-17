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

/*
Налоги по странам в разных сервисах (добавить страну = правки везде)
❌
decimal Vat(decimal net, string c) => 
    c switch { "KZ"=>net*0.12m, "EU"=>net*0.20m, _=>0m };
decimal Income(decimal gross, string c) => 
    c switch { "KZ"=>gross*0.10m, "EU"=>gross*0.15m, _=>0m };
*/

public interface ITaxPolicy
{
    decimal Vat(decimal net);
    decimal Income(decimal gross);
}

/*
Политика ретраев копируется (цикл + delay в каждом месте)
❌
for(int i=0;i<3;i++) try { return Do(); } catch { Thread.Sleep(200); }
*/

public interface IRetryPolicy
{
    T Execute<T>(Func<T> action);
}

/*
Скидки: VIP/BlackFriday/Mobile размазаны по коду
❌
if (vip) p*=0.9m;
if (bf) p*=0.8m;
if (mobile) p-=200m;
*/

public interface IDiscountPolicy
{
    decimal Apply(decimal price);
}

/*
Рабочие часы/календарь дублируются (заказы/чат/курьер)
❌
bool OpenForOrders(DateTime t)=> t.Hour>=9 && t.Hour<18;
bool OpenForChat(DateTime t)=> t.Hour>=10 && t.Hour<20;
*/

public interface ICalendar
{
    bool IsOpen(string service, DateTime when);
}

/*Формат отчёта (CSV/JSON/XML) дублируется в нескольких местах
❌
if (type=="CSV") return string.Join(",", data);
if (type=="JSON") return JsonSerializer.Serialize(data);*/

public interface IReportFormatter 
{ 
    string Format(IEnumerable<string> data); 
}

/*
Политика “пароль должен быть сложным” копируется по слоям
❌ До
bool Strong(string p)=>p.Length>=8 && p.Any(char.IsDigit) 
     && p.Any(char.IsUpper); // в API+UI+Batch
*/

public interface IPasswordPolicy { void Validate(string password); }

/*
Обработка ошибок: одно и то же “маппинг-правило” повторяется
❌ До
catch(SqlException ex) { return 500; }
catch(TimeoutException) { return 504; } // копия в 6 сервисах
*/

public interface IErrorMapper
{
    int ToHttpStatus(Exception ex);
}

/*
Логи аудита: формат записи/поля меняются → правим все места
❌ До
File.AppendAllText("audit.log", $"USER={u} ACTION={a} 
    TS={DateTime.UtcNow}\n");
*/

public interface IAuditLog { void Write(AuditEvent e); }
public record AuditEvent(string User, string Action, DateTime AtUtc);

/*Кэш-ключи руками: поменяли схему ключей → правим весь проект
❌ До
var key = $"user:{id}:profile"; // в N местах
cache.Set(key, value);*/

public interface ICacheKeys { string UserProfile(int id); }
public class CacheKeys : ICacheKeys { public string UserProfile(int id)=> $"user:{id}:profile"; }


/*Правила пагинации/сортировки: поменяли default page size → правки везде
❌ До
int pageSize = 20; // повторяется
query = query.Take(pageSize);*/

public sealed class PagingOptions { public int DefaultSize { get; init; } = 20; }
