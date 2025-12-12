/*public class User
{
    public string Email;
}*/

public sealed class Email
{
    public string Value { get; }
    public Email(string value)
    {
        if(string.IsNullOrEmpty(value)) || !value.Contains("@"))
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
public Money Total(Product p, decimal tax) => p.Price.Add(p.Price.Mul(tax));


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
public decimal ApplyDiscount(Money price, Percent p) => price.Mul(1 - p.Value).Amount;


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