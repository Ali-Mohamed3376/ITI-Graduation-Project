

using Final.Project.DAL;
using System.ComponentModel;

namespace Final.Project.BL;

public class ProductDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public decimal PriceAfter => Price - (Price * Discount / 100);
}
