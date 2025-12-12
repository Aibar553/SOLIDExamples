/*public class Product
{
    public string Name { get; }
    public decimal Price { get; }
    public int DiscountPercent { get; }

    public Product(string name, decimal price, int discountPercent)
    {
        Name = name;
        Price = price;
        DiscountPercent = discountPercent;
    }
}

public class ReceiptPrinter
{
    public string GetProductLine(Product product)
    {
        // ❌ МЕТОД СИЛЬНО ЛЕЗЕТ В ДАННЫЕ Product
        decimal discountAmount = product.Price * product.DiscountPercent / 100m;
        decimal finalPrice = product.Price - discountAmount;

        return $"{product.Name}: {finalPrice} (discount {product.DiscountPercent}%)";
    }
}*/
public class Product
{
    public string Name { get; }
    public decimal Price { get; }
    public int DiscountPercent { get; }

    public Product(string name, decimal price, int discountPercent)
    {
        Name = name;
        Price = price;
        DiscountPercent = discountPercent;
    }
    public string GetProductLine()
    {
        decimal discountAmount = price * discountPercent / 100m;
        decimal finalPrice = price - discountAmount;
        return $"{Name}:{Price}:(discount){DiscountPercent}";
    }
}
public class ReceiptPrinter
{
    public string GetProductLine(Product product)
    {
        product.GetProductLine();
    }
}

/*public class Customer
{
    public string Name { get; set; }
    public int LoyaltyPoints { get; set; }
    public string Status { get; set; } // "Regular", "Gold", "Platinum"
}

public class Order
{
    public Customer Customer { get; set; }
    public decimal Amount { get; set; }

    public decimal CalculateDiscount()
    {
        decimal discount = 0m;

        if (Customer.Status == "Gold")
            discount += Amount * 0.10m;
        else if (Customer.Status == "Platinum")
            discount += Amount * 0.15m;

        if (Customer.LoyaltyPoints > 1000)
            discount += Amount * 0.05m;

        return discount;
    }
}*/

public class Customer
{
    public string Name { get; set; }
    public int LoyaltyPoints { get; set; }
    public string Status { get; set; }
    public decimal CalculateDiscount(decimal amount)
    {
        decimal discount = 0m;
        if (Customer.Status == "Gold")
            discount += amount * 0.10m;
        else if (Customer.Status == "Platinum")
            discount += amount * 0.15m;

        if (Customer.LoyaltyPoints > 1000)
            discount += amount * 0.05m;

        return discount;
    }
}
public class Order
{
    public decimal CalculateDiscount(Customer customer)
    {
        customer.CalculateDiscount(amount);
    }
}




/*public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UserReport
{
    public string BuildLine(User user)
    {
        var fullName = user.FirstName + " " + user.LastName;
        var age = DateTime.Today.Year - user.BirthDate.Year;
        if (user.BirthDate.Date > DateTime.Today.AddYears(-age)) age--;

        return $"{fullName}, age: {age}";
    }
}*/

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string BuildLine()
    {
        var fullName = FirstName + " " + LastName;
        var age = DateTime.Today.Year - BirthDate.Year;
        if (BirthDate.Date > DateTime.Today.AddYears(-age)) age--;

        return $"{fullName}, age: {age}";
    }
}
public class UserReport
{
    public string BuildLine(User user)
    {
        user.BuildLine();
    }
}
