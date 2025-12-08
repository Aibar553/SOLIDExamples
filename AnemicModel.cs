/*public class OrderItem
{
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public class Order
{
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal Discount { get; set; } // просто число, без логики
}
public class OrderService
{
    public void AddItem(Order order, string productName, decimal price, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Количество должно быть больше нуля");
            return;
        }

        var item = new OrderItem
        {
            ProductName = productName,
            UnitPrice = price,
            Quantity = quantity
        };

        order.Items.Add(item);
    }

    public decimal CalculateTotal(Order order)
    {
        decimal total = 0m;
        foreach (var item in order.Items)
        {
            total += item.UnitPrice * item.Quantity;
        }

        return total - order.Discount;
    }

    public void ApplyDiscount(Order order, decimal discount)
    {
        var total = CalculateTotal(order);
        if (discount < 0)
        {
            Console.WriteLine("Скидка не может быть отрицательной");
            return;
        }
        if (discount > total)
        {
            Console.WriteLine("Скидка не может быть больше суммы заказа");
            return;
        }

        order.Discount = discount;
    }
}

class Program
{
    static void Main()
    {
        var order = new Order();
        var service = new OrderService();

        service.AddItem(order, "Mouse", 5000m, 2);
        service.AddItem(order, "Keyboard", 15000m, 1);

        service.ApplyDiscount(order, 2000m);

        var total = service.CalculateTotal(order);
        Console.WriteLine($"Итого к оплате: {total}");
    }
}
*/


public class OrderItem
{
    public string ProductName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; private set; }

    public OrderItem(string productName, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Название товара обязательно", nameof(productName));
        if (unitPrice <= 0)
            throw new ArgumentException("Цена должна быть > 0", nameof(unitPrice));
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть > 0", nameof(quantity));

        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public decimal GetTotal() => UnitPrice * Quantity;

    public void ChangeQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Количество должно быть > 0", nameof(newQuantity));
        Quantity = newQuantity;
    }
}

public class Order
{
    private readonly List<OrderItem> _items = new List<OrderItem>();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public decimal Discount { get; private set; }

    public void AddItem(string productName, decimal unitPrice, int quantity)
    {
        var item = new OrderItem(productName, unitPrice, quantity);
        _items.Add(item);
    }

    public decimal GetTotalWithoutDiscount()
    {
        return _items.Sum(i => i.GetTotal());
    }

    public decimal GetTotal()
    {
        return GetTotalWithoutDiscount() - Discount;
    }

    public void ApplyDiscount(decimal discount)
    {
        if (discount < 0)
            throw new ArgumentException("Скидка не может быть отрицательной", nameof(discount));

        var totalWithoutDiscount = GetTotalWithoutDiscount();
        if (discount > totalWithoutDiscount)
            throw new ArgumentException("Скидка не может быть больше суммы заказа", nameof(discount));

        Discount = discount;
    }
}

/*public class BankAccount
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; set; }
}

public class BankAccountService
{
    public void Deposit(BankAccount account, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сумма пополнения должна быть > 0");
            return;
        }

        account.Balance += amount;
        Console.WriteLine($"Счёт {account.AccountNumber} пополнен на {amount}. Новый баланс: {account.Balance}");
    }

    public void Withdraw(BankAccount account, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сумма снятия должна быть > 0");
            return;
        }

        if (amount > account.Balance)
        {
            Console.WriteLine("Недостаточно средств на счёте");
            return;
        }

        account.Balance -= amount;
        Console.WriteLine($"Со счёта {account.AccountNumber} снято {amount}. Новый баланс: {account.Balance}");
    }

    public void Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сумма перевода должна быть > 0");
            return;
        }

        if (amount > from.Balance)
        {
            Console.WriteLine("Недостаточно средств для перевода");
            return;
        }

        from.Balance -= amount;
        to.Balance += amount;

        Console.WriteLine($"Перевод {amount} со счёта {from.AccountNumber} на {to.AccountNumber} выполнен.");
        Console.WriteLine($"Баланс отправителя: {from.Balance}, получателя: {to.Balance}");
    }
}*/
public class BankAccount
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public decimal Balance { get; set; }
}
/*public class User
{
    public string Email { get; set; }
    public bool IsActive { get; set; }
}

public class UserService
{
    public void Deactivate(User user)
    {
        user.IsActive = false;
        Console.WriteLine($"User {user.Email} deactivated");
    }
}*/
public class User
{
    public string Email { get; private set; }
    public bool IsActive { get; private set; } = true;

    public User(string email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("User already inactive");

        IsActive = false;
    }
}

public class UserService
{
    public void DeactivateUser(User user)
    {
        user.Deactivate();
        Console.WriteLine($"User {user.Email} deactivated");
    }
}
/*public class BankAccount
{
    public string Number { get; set; }
    public decimal Balance { get; set; }
}

public class BankService
{
    public void Withdraw(BankAccount account, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        if (account.Balance < amount)
            throw new InvalidOperationException("Not enough money");

        account.Balance -= amount;
    }
}*/
public class BankAccount
{
    public string Number { get; private set; }
    public decimal Balance { get; private set; }
    public BankAccount(string number, decimal balance)
    {
        Number = number ?? throw new ArgumentNullException(nameof(number));
        if (balance < 0)
            throw new ArgumentException("Initial balance cannot be negative");

        Balance = balance;
    }
    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be positive");
        }
        if (amount > Balance)
        {
            throw new InvalidOperationException("Not enough money");
        }
        Balance -= amount;
    }
}
public class BankService
{
    public void Withdraw(BankAccount account, decimal amount)
    {
        account.Withdraw(amount);
    }
}

/*public class Product
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountPercent { get; set; }
}

public class PriceCalculator
{
    public decimal CalculateFinalPrice(Product product)
    {
        var discount = product.BasePrice * product.DiscountPercent / 100m;
        return product.BasePrice - discount;
    }
}*/

public class Product
{
    public string Name { get; private set; }
    public decimal BasePrice { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public Product(string name, decimal basePrice, decimal discountPercent)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (basePrice < 0)
            throw new ArgumentException("BasePrice cannot be negative");
        if (discountPercent < 0 || discountPercent > 100)
            throw new ArgumentException("Discount must be between 0 and 100");
        BasePrice = basePrice;
        DiscountPercent = discountPercent;
    }
    public decimal CalculateFinalPrice()
    {
        var discount = BasePrice * DiscountPercent / 100m;
        return BasePrice - discount;
    }
}
public class PriceCalculator
{
    public decimal CalculateFinalPrice(Product product)
    {
        product.CalculateFinalPrice();
    }
}


