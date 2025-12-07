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