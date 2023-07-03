using Final.Project.DAL;

namespace Final.Project.BL;

public class OrderDetailsDto
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliverdDate { get; set; } = null;
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<ProductsInOrder> ProductsInOrder { get; set; } = new HashSet<ProductsInOrder>();
}

public class ProductsInOrder
{
    public int Quantity { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public string ProductModel { get; set; } = string.Empty;
}