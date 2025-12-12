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