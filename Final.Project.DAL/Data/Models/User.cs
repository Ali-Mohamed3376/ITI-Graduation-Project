﻿using Microsoft.AspNetCore.Identity;
namespace Final.Project.DAL;
public class User : IdentityUser<int>
{
    //Changed DT of Id Column In IdentityUser Table From String To int
    //by adding new Keyword and override get and set
    public new int Id
    {
        get { return base.Id; }
        set { base.Id = value; }
    }
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public Role Role { get; set; }
    public IEnumerable<UserProductsCart> UsersProductsCarts { get; set; } = new HashSet<UserProductsCart>();
    public IEnumerable<User> Users { get; set; } = new HashSet<User>();
    public IEnumerable<UserAddress> UserAddresses { get; set; } = new HashSet<UserAddress>();

}

public enum Role
{
    Admin, Customer
}


