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
