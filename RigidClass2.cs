/*public enum CustomerType
{
    Regular,
    Silver,
    Gold
}

public class DiscountCalculator
{
    public decimal CalculateDiscount(CustomerType type, decimal amount)
    {
        Console.WriteLine($"Расчёт скидки для суммы {amount}, тип клиента: {type}");

        decimal discount = 0m;

        if (type == CustomerType.Regular)
        {
            discount = 0m; // без скидки
        }
        else if (type == CustomerType.Silver)
        {
            discount = amount * 0.05m; // 5%
        }
        else if (type == CustomerType.Gold)
        {
            discount = amount * 0.10m; // 10%
        }
        else
        {
            Console.WriteLine("Неизвестный тип клиента!");
        }

        Console.WriteLine($"Скидка: {discount}\n");
        return discount;
    }
}

class Program
{
    static void Main()
    {
        var calc = new DiscountCalculator();

        calc.CalculateDiscount(CustomerType.Regular, 1000m);
        calc.CalculateDiscount(CustomerType.Silver, 1000m);
        calc.CalculateDiscount(CustomerType.Gold, 1000m);
    }
}
*/
public interface IDiscountMethod
{
    decimal CalculateDiscount(decimal amount);
}

// Обычный клиент — без скидки
public class RegularDiscount : IDiscountMethod
{
    public decimal CalculateDiscount(decimal amount)
    {
        return 0m;
    }
}

// Silver клиент — 5%
public class SilverDiscount : IDiscountMethod
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount * 0.05m;
    }
}

// Gold клиент — 10%
public class GoldDiscount : IDiscountMethod
{
    public decimal CalculateDiscount(decimal amount)
    {
        return amount * 0.10m;
    }
}

public class DiscountService
{
    private readonly IDiscountMethod _discountMethod;

    public DiscountService(IDiscountMethod discountMethod)
    {
        _discountMethod = discountMethod;
    }

    public decimal CalculateDiscount(decimal amount)
    {
        var discount = _discountMethod.CalculateDiscount(amount);
        Console.WriteLine($"Сумма: {amount}, скидка: {discount}");
        return discount;
    }
}

class Program
{
    static void Main()
    {
        var regular = new DiscountService(new RegularDiscount());
        regular.CalculateDiscount(1000m);   // 0

        var silver = new DiscountService(new SilverDiscount());
        silver.CalculateDiscount(5000m);    // 250

        var gold = new DiscountService(new GoldDiscount());
        gold.CalculateDiscount(10000m);     // 1000
    }
}