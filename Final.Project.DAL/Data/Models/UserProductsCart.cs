namespace Final.Project.DAL;
public class UserProductsCart
{
    public int ProductId { get; set; }
    public int UserId { get; set; } 
    public int Quantity { get; set; }
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;


}
