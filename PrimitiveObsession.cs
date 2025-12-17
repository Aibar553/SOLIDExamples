/*public class User
{
    public string Email;
}*/

public sealed class Email
{
    public string Value { get; }
    public Email(string value)
    {
        if(string.IsNullOrEmpty(value) || !value.Contains("@"))
            throw new ArgumentException("Invalid email");
        Value = value;
    }
}
public class User { public Email Email { get; set; } }

/*public class Contact
{
    public string Phone;
}*/

public sealed class Phone
{
    public string Value { get; }
    public Phone(string value)
    {
        var digits = new string(raw.Where(char.IsDigit).ToArray());
        if (digits.Length is < 10 or > 15) throw new ArgumentException();
        Value = digits;
    }
}
public class Contact { public Phone Phone { get; set; } }


/*public class Product { public decimal Price; }
public decimal Total(Product p, decimal tax) => p.Price + p.Price * tax;*/

public readonly struct Money
{
    public decimal Amount { get; }
    public Money(decimal amount) => Amount = amount;
    public Money Add(Money amount) => new(Amount + amount);
    public Money Mul(decimal k) => new(Amount * k);
}
public class Product { public Money Price { get; set; } }


/*public decimal ApplyDiscount(decimal price, decimal percent) 
 * => price * (1 - percent);*/

public readonly struct Percent
{
    public decimal Value { get; }
    public Percent(decimal v)
    {
        if (v < 0m || v > 1m) throw new ArgumentOutOfRangeException();
        Value = v;
    }
}



/*public bool Overlaps(DateTime s1, DateTime e1, DateTime s2, DateTime e2)
    => s1 <= e2 && s2 <= e1;*/

public readonly struct DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }
    public DateRange(DateTime start, DateTime end)
    {
        if (end < start) throw new ArgumentException();
        (Start, End) = (start, end);
    }
    public bool Overlaps(DateRange other) => Start <= other.End && other.Start <= End;
}

/*
Деньги как decimal vs Money
public class Order
{
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public decimal GetTotal()
    {
        // везде decimal, нет проверок валюты, логики, округления и т.д.
        return Price - Discount;
    }
}
*/

public readonly struct Money
{
    public decimal Amount {get;}
    public Money(decimal amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }
    public static Money operator +(Money left, Money right) =>
        new Money(left.Amount + right.Amount);
    
    public static Money operator +(Money left, Money right) =>
        new Money(left.Amount - right.Amount);

    public override string ToString() => Amount.ToString("0.00");
}
public class Order
{
    public Money Price {get; set;}
    public Money Discount {get; set;}
    public Money GetTotal() => Price - Discount;
}

/*
Email как string vs Email
public class User
{
    public string Email { get; set; }  // любая строка, даже "asdf"
}
*/
public readonly struct UserEmail
{
    public string Email {get;}
    public UserEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email", nameof(email));
        Email = email;
    }
    public override string ToString() => Email;
}
public class User
{
    public UserEmail Email {get;set;}
    public User(Email email)
    {
        Email = email;
    }
}

/*Координаты как два int vs отдельный тип
Плохо
public class PointDrawer
{
    public void Draw(int x, int y)
    {
        Console.WriteLine($"Draw point at ({x}, {y})");
    }
}*/

public readonly struct Point
{
    public int X {get;}
    public int Y {get;}

    public Point(int x, int y)
    {
        X = x; Y = y;
    }
    public override string ToString() => $"({X}, {Y})";
}
public class PointDrawer
{
    public void Draw(Point point)
    {
        Console.WriteLine($"Draw point at {point}");
    }
}

/*
Статус заказа как string vs enum/тип
Плохо
public class Order
{
    public string Status { get; set; } 
    // "NEW", "new", "Done", "Closed", "1" - что угодно
}
*/

public enum OrderStatus
{
    New, Done, Closed, Cancelled
}
public class Order
{
    public OrderStatus Status { get; set; }
}


/*public class ReportRequest
{
    public DateTime From { get; set; }
    public DateTime To   { get; set; }
}

// где-то в коде:
if (request.From > request.To)
{
    // ой, мы забыли проверить, и всё упало
}*/

public readonly struct DateRange
{
    public DateTime From {get; set;}
    public DateTime To {get; set;}

    public DateRange(DateTime from, DateTime to)
    {
        if (from > to)
            throw new ArgumentException("From must be <= To");
        From = from; To = to;
    }
    public override string ToString() => $"{from} (dd:mm:yyyy) {to} (dd:mm:yyyy)";
}
public class ReportRequest
{
    public DateRange Period { get; }
    public ReportRequest(DateRange period)
    {
        Period = period;
    }
}

/*
Возраст пользователя
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; } // primitive obsession

    public bool IsAdult()
    {
        return Age >= 18;
    }
}
*/
public class User
{
    public int Value{get;}
    public User(int value)
    {
        if(value <= 0 || value >= 120)
            throw new ArgumentException("Invalid age");
        Value = value;
    }
    public bool IsAdult() => Value >= 18;
    public override string ToString() => Value;
}
public class Person
{
    public User Age;
    public Person(User age)
    {
        Age = age;
    }
    public bool IsAdult()
    {
        return Age.IsAdult();
    }
}

/*
Код продукта (артикул)
public class Product
{
    public string Name { get; set; }
    public string Code { get; set; } // например "ABC-123"

    public bool IsSpecial()
    {
        return Code != null && Code.StartsWith("VIP-");
    }
}
*/

public class ProductCode
{
    public string Value {get;}
    public ProductCode(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Invalid");
        Value = value;
    }
    public bool IsVip()
    {
        return Value.StartsWith("VIP-");
    }
    public override string ToString() => Value;
}
public class Product
{
    public ProductCode Code{get;}
    public Product(ProductCode code)
    {
        Code = code;
    }
    public bool IsSpecial()
    {
        return Code.IsVip();
    }
}

/*
URL ресурса
public class LinkService
{
    public void Open(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("url is empty");

        if (!url.StartsWith("http"))
            throw new ArgumentException("url must start with http or https");

        Console.WriteLine($"Opening {url}");
    }
}
*/

public sealed class UrlService
{
    public string Value{get;}
    public UrlService(string value)
    {
        if(string.IsNullOrWhiteSpace(value) || !value.StartsWith("http"))
            throw new ArgumentException("Invalid");
        Value = value;
    }
    public override string ToString() => Value;
}
public class LinkService
{
    public void Open(string url)
    {
        Console.WriteLine($"Opening {url}");
    }
}

/*public class RetryPolicy
{
    public int RetryCount { get; }

    public RetryPolicy(int retryCount)
    {
        if (retryCount < 0 || retryCount > 10)
            throw new ArgumentOutOfRangeException(nameof(retryCount));

        RetryCount = retryCount;
    }
}*/

public readonly struct Policy
{
    public int Value {get;}
    public Policy(int value)
    {
        if (value < 0 || value > 10)
            throw new ArgumentOutOfRangeException(nameof(value), 
            "RetryCount must be between 0 and 10");
        Value = value;
    }
    public override string ToString() => Value.ToString();
}
public class RetryPolicy
{
    public RetryCount RetryCount { get; }

    public RetryPolicy(RetryCount retryCount)
    {
        RetryCount = retryCount;
    }
}

/*
public class TaskProgress
{
    public int Percent { get; private set; }

    public void SetPercent(int percent)
    {
        if (percent < 0 || percent > 100)
            throw new ArgumentOutOfRangeException(nameof(percent));

        Percent = percent;
    }
}
*/

public class Task
{
    public int Value{get;}

    public Task(int value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;

    }
    public override string ToString() => Value;
}
public class TaskProgress
{
    public Task Percent{ get; }
    public void SetPercent(Task percent)
    {
        Percent = percent;
    }
}