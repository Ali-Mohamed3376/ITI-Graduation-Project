using Microsoft.AspNetCore.Identity;
namespace Final.Project.DAL;
public class User : IdentityUser
{
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public Role Role { get; set; }
    public IEnumerable<UserProductsCart> UsersProductsCarts { get; set; } = new HashSet<UserProductsCart>();
    public IEnumerable<User> Users { get; set; } = new HashSet<User>();

}

public enum Role
{
    Admin, Customer
}


