/*public class Customer
{
    public string Name { get; }
    public string City { get; }
    public string Country { get; }

    public Customer(string name, string city, string country)
    {
        Name = name;
        City = city;
        Country = country;
    }
}

public class Order
{
    public Customer Customer { get; }
    public decimal TotalPrice { get; }

    public Order(Customer customer, decimal totalPrice)
    {
        Customer = customer;
        TotalPrice = totalPrice;
    }

    // ❌ Подозрительный метод
    public string GetShippingLabel()
    {
        // МЕТОД СИЛЬНО ЛЕЗЕТ В ДАННЫЕ Customer
        return $"{Customer.Name}\n{Customer.City}\n{Customer.Country}";
    }
}*/
public class Customer
{
    public string Name { get; }
    public string City { get; }
    public string Country { get; }

    public Customer(string name, string city, string country)
    {
        Name = name;
        City = city;
        Country = country;
    }

    // ✅ Новый метод – логика рядом с данными
    public string GetAddressLabel()
    {
        return $"{Name}\n{City}\n{Country}";
    }
}
public class Order
{
    public Customer Customer { get; }
    public decimal TotalPrice { get; }

    public Order(Customer customer, decimal totalPrice)
    {
        Customer = customer;
        TotalPrice = totalPrice;
    }

    public string GetShippingLabel()
    {
        // ✅ Больше не шарится по полям Customer, просто просит его.
        return Customer.GetAddressLabel();
    }
}
/*public class BankAccount
{
    public string OwnerName { get; }
    public decimal Balance { get; private set; }
    public bool IsVip { get; }

    public BankAccount(string ownerName, decimal balance, bool isVip)
    {
        OwnerName = ownerName;
        Balance = balance;
        IsVip = isVip;
    }
}

public class BankService
{
    // ??? Подозрительный метод
    public decimal CalculateMonthlyFee(BankAccount account)
    {
        decimal fee = 0m;

        if (account.IsVip)
        {
            fee = 0m;
        }
        else if (account.Balance < 1000m)
        {
            fee = 10m;
        }
        else
        {
            fee = 5m;
        }

        return fee;
    }
}*/
public class BankAccount
{
    public string OwnerName { get; }
    public decimal Balance { get; private set; }
    public bool IsVip { get; }

    public BankAccount(string ownerName, decimal balance, bool isVip)
    {
        OwnerName = ownerName;
        Balance = balance;
        IsVip = isVip;
    }
    public decimal CalculateMonthlyFee()
    {
        decimal fee = 0m;

        if (IsVip)
        {
            fee = 0m;
        }
        else if (Balance < 1000m)
        {
            fee = 10m;
        }
        else
        {
            fee = 5m;
        }

        return fee;
    }
}

public class BankService
{
    public decimal CalculateMonthlyFee()
    {
        return BankAccount.CalculateMonthlyFee();
    } 
}

/*public class BankAccount
{
    public string Number { get; }
    public decimal Balance { get; private set; }
    public bool IsBlocked { get; private set; }

    public BankAccount(string number, decimal initialBalance)
    {
        Number = number;
        Balance = initialBalance;
        IsBlocked = false;
    }

    public void Block()
    {
        IsBlocked = true;
    }
}

public class ATM
{
    // ??? Подозрительный метод
    public bool Withdraw(BankAccount account, decimal amount)
    {
        if (account.IsBlocked)
        {
            Console.WriteLine("Account is blocked");
            return false;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Invalid amount");
            return false;
        }

        if (account.Balance < amount)
        {
            Console.WriteLine("Insufficient funds");
            return false;
        }

        account.GetType()
               .GetProperty("Balance")
               .SetValue(account, account.Balance - amount);

        Console.WriteLine($"Withdrawn {amount}, new balance: {account.Balance}");
        return true;
    }
}*/

public class BankAccount
{
    public string Number { get; }
    public decimal Balance { get; private set; }
    public bool IsBlocked { get; private set; }

    public BankAccount(string number, decimal initialBalance)
    {
        Number = number;
        Balance = initialBalance;
        IsBlocked = false;
    }

    public void Block()
    {
        IsBlocked = true;
    }
    public bool Withdraw(decimal amount)
    {
        if (IsBlocked)
        {
            Console.WriteLine("Account is blocked");
            return false;
        }
        if(amount <= 0)
        {
            Console.WriteLine("Invalid amount");
            return false;
        }
        if(Balance < amount)
        {
            Console.WriteLine("Insufficient funds");
            return false;
        }
        BankAccount.GetType()
        .GetProperty("Balance")
        .SetValue(BankAccount, BankAccount.Balance - amount);

        Console.WriteLine($"Withdrawn {amount}, new balance: {BankAccount.Balance}");
        return true;
    }
}