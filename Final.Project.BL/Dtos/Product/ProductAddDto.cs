using Microsoft.AspNetCore.Http;

namespace Final.Project.BL;

public class ProductAddDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public string Model { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public int CategoryID { get; set; }
}
