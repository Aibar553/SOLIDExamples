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
/*public class CartItem
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Qty { get; set; }
}

public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}

public class CartService
{
    public void AddItem(ShoppingCart cart, string productName, decimal price, int qty)
    {
        cart.Items.Add(new CartItem
        {
            ProductName = productName,
            Price = price,
            Qty = qty
        });
    }

    public decimal GetTotal(ShoppingCart cart)
    {
        return cart.Items.Sum(i => i.Price * i.Qty);
    }
}*/
public class CartItem
{
    public string ProductName { get; }
    public decimal Price { get; }
    public int Qty { get; private set; }
    public CartItem(string productName, decimal price, int qty)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name required", nameof(productName));
        if (price <= 0)
            throw new ArgumentException("Price must be positive", nameof(price));
        if (qty <= 0)
            throw new ArgumentException("Qty must be positive", nameof(qty));

        ProductName = productName;
        Price = price;
        Qty = qty;
    }
    public void AddItem(int delta)
    {
        if (delta <= 0)
        {
            throw new ArgumentException("Delta must be positive");
        }
        Qty += delta;
    }
    public decimal GetTotal()
    {
        return Price * Qty;
    }
}
public class ShoppingCart
{
    private readonly List<CartItem> _items = new List<CartItem>();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public void AddItem(string productName, decimal price, int qty)
    {
        _items.Add(new CartItem(productName, price, qty));
    }
    public decimal GetTotal()
    {
        return _items.Sum(i => i.GetTotal);
    }
}
public class CartService
{
    public decimal Checkout(ShoppingCart cart)
    {
        var total = cart.GetTotal();
        return total;
    }
}


/*public class Booking
{
    public int Id { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public bool IsCancelled { get; set; }
}

public class BookingService
{
    public void Cancel(Booking booking)
    {
        booking.IsCancelled = true;
    }

    public void Reschedule(Booking booking, DateTime newFrom, DateTime newTo)
    {
        if (newFrom >= newTo)
            throw new ArgumentException("Invalid date range");

        booking.DateFrom = newFrom;
        booking.DateTo = newTo;
    }
}
*/
public class Booking
{
    public int Id { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public bool IsCancelled { get; set; }
    public Booking(int id, DateTime from, DateTime to)
    {
        if (from >= to)
            throw new ArgumentException("Invalid date range");
        Id = id;
        DateFrom = from;
        DateTo = to;
        IsCancelled = true;
    }
    public void Cancel()
    {
        IsCancelled = true;
    }
    public void Reschedule(DateTime newFrom, decimal newTo)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException("Cannot reschedule cancelled booking");
        }
        if (newFrom >= newTo)
        {
            throw new ArgumentException("Invalid date range");
        }
        DateFrom = newFrom;
        DateTo = newTo;
    }
}
public class BookingService
{
    public void CancelBooking(Booking booking)
    {
        booking.Cancel();
    }
    public void RescheduleBooking(Booking booking)
    {
        booking.Reschedule(newFrom, newTo);
    }
}