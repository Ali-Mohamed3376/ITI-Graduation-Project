using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Project.DAL;
//[PrimaryKey("ProductId", "OrderId")] another way to declare the composite primary key
public class OrderProductDetails
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
