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
