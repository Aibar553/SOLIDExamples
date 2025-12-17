/*public class Order 
{ 
    public List<Item> Items; 
    public decimal Discount; 
}
public decimal Total(Order o) => 
    o.Items.Sum(i => i.Price) * (1 - o.Discount);*/

public class Order
{
    public List<Item> Items = new();
    public decimal Discount;
    public decimal Total() => Items.Sum(i => i.Price) 
        * (1 - Discount);
}
/*public class Employee { public decimal Salary; }
public decimal Annual(Employee e) => e.Salary * 12;*/

public class Employee
{
    public decimal Salary;
    public decimal Annual() => Salary * 12;
}

/*public class Product 
{ 
    public decimal Price; 
    public decimal Discount; 
    public decimal Tax; 
}
public decimal FinalPrice(Product p) => 
    (p.Price * (1 - p.Discount)) * (1 + p.Tax);*/

public class Product
{
    public decimal Price;
    public decimal Discount;
    public decimal Tax;
    public decimal FinalPrice()
    {
        return Price * (1 - Discount) * (1 + Tax);
    }
}

/*public class Cart 
{ 
    public List<Item> Items; 
}
public void Add(Cart c, Item i) => c.Items.Add(i);
public decimal Total(Cart c) => c.Items.Sum(x => x.Price);*/

public class Cart
{
    public List<Item> Items;
    public void Add(Item i)
    {
        Items.Add(i);
    }
    public decimal Total()
    {
        Items.Sum(x => x.Price);
    }
}


/*public class BankAccount { public decimal Balance; }
public void Withdraw(BankAccount a, decimal amt)
{
    if (a.Balance < amt) throw new InvalidOperationException();
    a.Balance -= amt;
}*/

public class BankAccount
{
    public decimal Balance;
    public void Withdraw(decimal amt)
    {
        if(Balance < amt)
            throw new InvalidOperationException();
        Balance -= amt;
    }
}

/*public class Rectangle 
{ 
    public int W; 
    public int H; 
}
public int Area(Rectangle r) => r.W * r.H;
public int Perimeter(Rectangle r) => 2 * (r.W + r.H);*/

public class Rectangle
{
    public int W;
    public int H;
    public int Area()
    {
        return W * H;
    }
    public int Perimeter()
    {
        2 * (W + H);
    }
}

/*public class Student { public List<int> Grades; }
public double Gpa(Student s) => s.Grades.Average();
public bool Passed(Student s, double thr) => Gpa(s) >= thr;*/

public class Student
{
    public List<int> Grades;
    public double Gpa()
    {
        return Grades.Count == 0 ? 0 : Grades.Average();
    }
    public bool Passed(double thr)
    {
        return Gpa() >= thr;
    }
}

/*public class Address { public string City, Zip, Street; }
public bool CanDeliver(Address a) => a.City == "Almaty" && a.Zip.StartsWith("05");
public string Format(Address a) => $"{a.City}, {a.Street}, {a.Zip}";*/

public class Address
{
    public string City, Zip, Street;
    public bool CanDeliver()
    {
        return City = "Shymkent" && Zip.StartsWith("02") == true;
    }
    public string Format()
    {
        return $"{City}, {Street}, {Zip}";
    }
}

/*public class Booking { public DateTime Start, End; }
public bool Overlaps(Booking a, Booking b) => a.Start <= b.End && b.Start <= a.End;*/

public class Booking
{
    public DateTime Start, End;
    public bool Overlaps(Booking other)
    {
        return Start <= other.End && other.Start <= End;
    }
}


/*public class LoanApplication { public decimal Income, Debts; public int Age; }
public bool Approved(LoanApplication a) => a.Age >= 21 && a.Income > a.Debts * 2;*/

public class LoanApplication
{
    public decimal Income, Debts;
    public int Age;
    public bool Application()
    {
        return Age >= 21 && Income > Debts * 2;
    }
}

/*
Booking — правила отмены 
(бесплатно до 48ч, иначе штраф; non-refundable нельзя)
public class Booking
{
    public DateTime CheckIn;
    public bool NonRefundable;
    public decimal Price;
}
public decimal RefundAmount(Booking b, DateTime cancelAt)
{
    if (b.NonRefundable) return 0;
    var hours = (b.CheckIn - cancelAt).TotalHours;
    if (hours >= 48) return b.Price;          // полная
    if (hours >= 24) return b.Price * 0.5m;   // 50%
    return 0;                                  // нет возврата
}
*/

public class Booking
{
    public DateTime CheckIn;
    public bool NonRefundable;
    public decimal Price;
    public Booking(DateTime checkIn, bool nonRefundable, decimal price)
    {
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
        if (checkIn <= DateTime.UtcNow) throw new ArgumentException("Check-in in past");
        CheckIn = checkIn; NonRefundable = nonRefundable; Price = price;
    }
    public decimal RefundAmount(DateTime cancelAt)
    {
        if(NonRefundable) return 0;
        var hours = (CheckIn - cancelAt).TotalHours;
        if (hours >= 48) return Price;          // полная
        if (hours >= 24) return Math.Round(Price * 0.5m, 2);   // 50%
        return 0;                                  // нет возврата
    }
}
/*
Loyalty — расчёт баллов (мультипликатор по tier + месячный бонус)
❌ Анемично
public class LoyaltyAccount { public string Tier; public int Points; }
public int Earn(LoyaltyAccount a, decimal purchase)
{
    var basePts = (int)Math.Floor(purchase);
    var mult = a.Tier == "Gold" ? 2 : a.Tier == "Silver" ? 1.5m : 1m;
    return (int)(basePts * mult);
}
public int MonthlyBonus(LoyaltyAccount a, int monthSpend)
    => monthSpend >= 1000 ? 500 : 0;
*/

public enum Tier { Bronze, Silver, Gold }

public class LoyaltyAccount
{
    public Tier Tier { get; private set; }
    public int Points { get; private set; }

    public LoyaltyAccount(Tier tier, int initialPoints = 0)
    {
        if (initialPoints < 0) throw new ArgumentOutOfRangeException();
        Tier = tier; Points = initialPoints;
    }

    public int EarnFrom(decimal purchaseAmount)
    {
        if (purchaseAmount <= 0) return 0;
        var basePts = (int)Math.Floor(purchaseAmount);        // 1 балл = 1 у.е.
        decimal mult = Tier == Tier.Gold ? 2m : Tier == Tier.Silver ? 1.5m : 1m;
        var earned = (int)Math.Floor(basePts * mult);
        Points += earned;
        return earned;
    }

    public int ApplyMonthlyBonus(decimal monthlySpend)
    {
        var bonus = monthlySpend >= 1000m ? 500 : 0;
        Points += bonus;
        return bonus;
    }
}

/*Shipment — цена по зонам и весовым порогам (зоны A/B/C, шаги веса)
❌ Анемично
public class Shipment { public string Zone; public decimal WeightKg; }
public decimal CalcPrice(Shipment s)
{
    if (s.Zone == "A") return s.WeightKg <= 1 ? 
       1000 : 1000 + (s.WeightKg - 1) * 300;
    if (s.Zone == "B") return s.WeightKg <= 1 ? 
       1500 : 1500 + (s.WeightKg - 1) * 400;
    if (s.Zone == "C") return s.WeightKg <= 1 ? 
       2000 : 2000 + (s.WeightKg - 1) * 500;
    throw new ArgumentException("Unknown zone");
}*/

public enum Zone{ A, B, C }
public class Shipment
{
    public Zone Zone;
    public decimal WeightKg;
    public Shipment(Zone zone, decimal weightkg = 0)
    {
        Zone = zone; WeightKg = weightkg;
        if (weightkg < 0) throw new ArgumentOutOfRangeException();
    }
    public decimal CalcPrice()
    {
        var (basePrice, step) = Zone switch
        {
            Zone.A => (1000m, 300m),
            Zone.B => (1500m, 400m),
            Zone.C => (2000m, 500m),
            _ => throw new ArgumentOutOfRangeException()
        };
        if(WeightKg <= 1m) return basePrice;
        var extra = Math.Ceiling((double)(WeightKg - 1m));
        return basePrice + (decimal) extra * step;
    }
}