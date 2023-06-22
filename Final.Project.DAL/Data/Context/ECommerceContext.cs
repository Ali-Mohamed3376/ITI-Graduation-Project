using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;

// Edit IdentityDbContext to change  Id Column In IdentityUser Table From String To int
// See User Class to Understand What We Change !!!
//public class ECommerceContext : IdentityDbContext<User, IdentityRole<int>, int>
public class ECommerceContext : IdentityDbContext<User>
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<OrderProductDetails> OrderProductDetails => Set<OrderProductDetails>();
    public DbSet<UserProductsCart> UserProductsCarts => Set<UserProductsCart>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region User


        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FName)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(e => e.LName)
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .IsRequired();

            entity.Property(e => e.Role)
                  .IsRequired();

            entity.Property(e => e.City)
                  .IsRequired(false);

            entity.Property(e => e.Street)
                  .HasMaxLength(30)
                  .IsRequired(false);
        });



        #endregion

        #region Product

        builder.Entity<Product>(entity =>
        {

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.Price)
                 .IsRequired();

            entity.Property(e => e.Description)
                .IsRequired();

            entity.Property(e => e.Image)
                .IsRequired();

            entity.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryID);
        });


        #endregion

        #region Category

        builder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });


        #endregion

        #region Order
        builder.Entity<Order>(entity =>
        {

            entity.Property(e => e.UserId)
            .IsRequired();

            entity.Property(e => e.OrderStatus)
           .IsRequired();

            entity.Property(e => e.OrderDate)
           .IsRequired();

            entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId);





        });
        #endregion

        #region OrderDetails
        builder.Entity<OrderProductDetails>(entity =>
        {
            entity.Property(e=>e.Quantity)
                .IsRequired();

            entity.HasOne(e => e.Product)
                    .WithMany(e=>e.OrdersProductDetails)
                    .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.Order)
                   .WithMany(e => e.OrdersProductDetails)
                   .HasForeignKey(e => e.OrderId);

            entity.HasKey(e => new { e.OrderId, e.ProductId });

        });
        #endregion

        #region Cart
        builder.Entity<UserProductsCart>(entity =>
        {
            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.HasOne(e => e.User)
                    .WithMany(e => e.UsersProductsCarts)
                    .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.Product)
                   .WithMany(e => e.UsersProductsCarts)
                   .HasForeignKey(e => e.ProductId);

            entity.HasKey(e => new { e.UserId, e.ProductId });

        });
        #endregion

        #region UserAddress

        builder.Entity<UserAddress>(entity => {

            entity.HasKey(e => e.Id);

            entity.Property(e => e.City)
                  .HasMaxLength(100)      
                  .IsRequired();

            entity.Property(e => e.Street)
               .HasMaxLength(200)
               .IsRequired();

            entity.HasOne(e => e.User)
                  .WithMany(u => u.UserAddresses)
                  .HasForeignKey(e => e.UserId);

        });

        #endregion

    }
}
