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
// ������������� ���
public readonly record struct Point(int X, int Y);




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


/*
Телефон
Плохой код
public class Customer
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; } // тут везде string

    public bool HasKazakhstanPhone()
    {
        return PhoneNumber != null && PhoneNumber.StartsWith("+7");
    }
}*/
public readonly struct Customers
{
    public string Name {get;}
    public string PhoneNumber {get;}
    public Customers(string name, string phonenumber)
    {
        Name = name; PhoneNumber = phonenumber;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name", nameof(name));
        if (string.IsNullOrWhiteSpace(phonenumber) || !phonenumber.StartsWith("+7"))
            throw new ArgumentException("Invalid phone", nameof(phonenumber));
        Email = email;
    }
    public bool IsKazakhstan()
    {
        return Value.StartsWith("+7");
    }
    public override string ToString()
    {
        Name; PhoneNumber;
    }
}
public class Client
{
    public Customers Name{get;}
    public Customers PhoneNumber { get; set; }

    public bool HasKazakhstanPhone()
    {
        return PhoneNumber != null && PhoneNumber.IsKazakhstan();
    }
}


/*Полный адрес
Плохой код
public class ShippingService
{
    public void Ship(string country, string city, string street, string postalCode)
    {
        Console.WriteLine($"Ship to: {country}, {city}, {street}, {postalCode}");
    }
}*/
public class Address
{
    public string Country {get;}
    public string City {get;}
    public string Street {get;}
    public string PostalCode {get;}
    public Address(string country, string city, string street, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country required", nameof(country));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City required", nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street required", nameof(street));
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code required", nameof(postalCode));

        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public override string ToString()
        => $"{Country}, {City}, {Street}, {PostalCode}";
}

public class ShippingService
{
    public void Ship(Address address)
    {
        Console.WriteLine($"Ship to: {address}");
    }
}

/*
Имя пользователя (логин)
Плохой код
public class AuthService
{
    public bool Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is empty");

        if (username.Length < 3)
            throw new ArgumentException("Username too short");

        // логика регистрации...
        return true;
    }
}*/
public readonly struct UserService
{
    public string UserName {get;}

    public UserService(string username)
    {
        UserName = username; Password = password;
        if(string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is empty");
        if (username.Length < 3)
            throw new ArgumentException("Username too short");
    }
    public override string ToString() => UserName;
}
public class AuthService
{
    public bool Register(UserService username, string password)
    {
        // Здесь уже не надо проверять username — он валиден по определению
        Console.WriteLine($"Registering user: {username}");
        return true;
    }
}


/*Процент скидки
Плохой код
public class DiscountService
{
    public decimal ApplyDiscount(decimal price, int discountPercent)
    {
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent));

        var discount = price * discountPercent / 100m;
        return price - discount;
    }
}*/
public readonly struct DiscountPercent
{
    public int Value { get; }
    public Discount(int value)
    {
        if(value < 0 || value > 100)
            throw new ArgumentOutOfRangeException(nameof(value),
               "Must be between 0 and 100");
        Value = value;
    }
    public decimal ApplyTo(decimal price)
    {
        var discount = price * Value / 100m;
        return price - discount;
    }
    public override string ToString() => $"{Value}";
}
public class DiscountService
{
    public decimal ApplyDiscount(decimal price, DiscountPercent discount)
    {
        return discount.ApplyTo();
    }
}


/*Диапазон страниц
Плохой код
public class Pagination
{
    public List<int> GetPageNumbers(int fromPage, int toPage)
    {
        if (fromPage < 1 || toPage < 1)
            throw new ArgumentException("Pages must be positive");

        if (fromPage > toPage)
            throw new ArgumentException("fromPage must be <= toPage");

        var pages = new List<int>();
        for (int i = fromPage; i <= toPage; i++)
        {
            pages.Add(i);
        }

        return pages;
    }
}*/
public readonly struct PageRange
{
    public int From {get;}
    public int To {get;}
    public PageRange(int from, int to)
    {
        if (from < 1 || to < 1)
            throw new ArgumentException("Pages must be >= 1");

        if (from > to)
            throw new ArgumentException("From must be <= To");

        From = from;
        To   = to;
    }
    public override string ToString() => $"{From}-{To}";
}
public class Pagination
{
    public List<int> GetPageNumbers(PageRange range)
    {
        var pages = new List<int>();
        for(int i = range.From; i <= range.To; i++)
        {
            pages.Add(i);
        }
        return pages;
    }
}

/*
public class Timer
{
    public void Start(int seconds)
    {
        if (seconds <= 0)
            throw new ArgumentOutOfRangeException(nameof(seconds));

        Console.WriteLine($"Starting timer for {seconds} seconds");
    }
}
*/
public class Seconds
{
    public int Value {get;}
    public Seconds(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
    public override string ToString() =>  $"{Value} sec";
}
public class Timer
{
    public void Start(Seconds seconds)
    {
        Console.WriteLine($"Starting timer for {seconds} seconds");
    }
}

/*
public class OrderService
{
    public void CancelOrder(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentOutOfRangeException(nameof(orderId));

        Console.WriteLine($"Cancel order #{orderId}");
    }
}
*/
public class Order
{
    public int Id {get;}
    public Order(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        Id = id;
    }
    public override string ToString() => $"{Id} id";
}
public class OrderService
{
    public void CancelOrder(Order id)
    {
        Console.WriteLine($"Cancel order #{id}");
    }
}

/*public class FileExporter
{
    public void Export(string filePath, string content)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path is empty", nameof(filePath));

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, content);
    }
}*/
public sealed class FilePath
{
    public string Value { get; }

    public FilePath(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("File path is empty", nameof(value));

        Value = value;
    }

    public string GetDirectory()
    {
        return Path.GetDirectoryName(Value)
               ?? throw new InvalidOperationException("No directory in path");
    }

    public override string ToString() => Value;
}

public class FileExporter
{
    public void Export(FilePath filePath, string content)
    {
        Directory.CreateDirectory(filePath.GetDirectory());
        File.WriteAllText(filePath.Value, content);
    }
}
