/*public class Customer 
{ 
    public string City; 
    public string Street; 
    public string Zip; 
}
*/

public sealed class Address
{
    public string City { get; }
    public string Street { get; }
    public string Zip { get; }

    public Address(string city, string street, string zip)
    {
        City = city?.Trim() ?? throw new ArgumentNullException();
        Street = street?.Trim() ?? throw new ArgumentNullException();
        Zip = zip?.Trim() ?? throw new ArgumentNullException();
    }
    public override string ToString() => $"{City}, {Street}, {Zip}";
}
public class Customer { public Address Address { get; set; } }


/*public class Address
{
    public string CountryCode;
}*/

public sealed class Country
{
    public string Value;
    public Country(string value)
    {
        if (string.IsNullOrEmpty(Value))
            throw new ArgumentException("Invalid value");
        Value = value;
    }
}
public class Address { public Country Country { get; set; } }

/*public void Move(int x, int y) {  ...  }*/
// семантический тип
public readonly record struct Point(int X, int Y);
public void Move(Point delta) { }



/*public class Order { public int Id; }*/

public class Identify
{
    public int Value;
    public Identify(int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException();
        Value = value;
    }
}
public class Order { public Identify Identify { get; set; } }



/*public decimal Convert(decimal amount, string currency) { }*/

public enum Currency { USD, EUR, KZT }

public readonly struct MoneyC
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    public MoneyC(decimal amount, Currency c) => (Amount, Currency) = (amount, c);
}
