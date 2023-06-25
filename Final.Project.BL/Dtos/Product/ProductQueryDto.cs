namespace Final.Project.DAL;
public class ProductQueryDto
{
    public int? CategotyId { get; set; }
    public string? ProductName { get; set; }
    public double? Rate { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
