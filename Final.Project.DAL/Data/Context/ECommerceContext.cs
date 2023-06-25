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
    public DbSet<Review> Reviews => Set<Review>();

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

            
            entity.Property(e => e.OrderStatus)
           .IsRequired();

            entity.Property(e => e.OrderDate)
           .IsRequired();


            entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.NoAction);


            entity.HasOne(e => e.UserAddress)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserAddressId)
                    .OnDelete(DeleteBehavior.NoAction);












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
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.NoAction);



        });

        #endregion

        #region review
        builder.Entity<Review>(entity =>
        {
            entity.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(e => e.UserId);
            entity.HasOne(x => x.Product)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.ProductId);
            entity.HasKey(e => new { e.UserId, e.ProductId });
        });


        #endregion

        #region Category Seeding
        List<Category> categoryList = new List<Category>
        {
            new Category{Id=1,Name="Apple"},
            new Category{Id=2,Name="Dell"},
            new Category{Id=3,Name="HP"},
            new Category{Id=4,Name="Lenovo"},
            new Category{Id=5,Name="ASUS"},
            new Category{Id=6,Name="Acer"},
            new Category{Id=7,Name="Microsoft"},
            new Category{Id=8,Name="MSI"},
            new Category{Id=9,Name="Razer"},
            new Category{Id=10,Name="Samsung"},
        };
        builder.Entity<Category>().HasData(categoryList);
        #endregion
        builder.Entity<Category>().HasData(categoryList);

        #region Product Seeding  

        List<Product> ProductList = new List<Product>
        {
            new Product{Id=1,CategoryID=4,Name="LENOVO Legion 5 Pro",Model="82JQ00TQED",Price=46999 ,
                Description="Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n"},
            new Product{Id=2,CategoryID=5,Name="Asus ZenBook 14 UX3402ZA",Model="UX3402ZA-OLED007W",Price=43499,
                Description="Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%"},
            new Product{Id=3,CategoryID=4,Name="LENOVO IdeaPad Gaming",Model="LENOVO IdeaPad Gaming",Price=27999,
                Description="Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)"},
            new Product{Id=4,CategoryID=3,Name="NOTEBOOK-HP-AMD-15s",Model="eq2009ne",Price=16666,
                Description="AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad"}
        };
        builder.Entity<Product>().HasData(ProductList);
        #endregion

        #region Product Seeding  

        List<Product> ProductList = new List<Product>
        {
            new Product{Id=1,CategoryID=4,Name="LENOVO Legion 5 Pro",Model="82JQ00TQED",Price=46999 ,
                Description="Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n"},
            new Product{Id=2,CategoryID=5,Name="Asus ZenBook 14 UX3402ZA",Model="UX3402ZA-OLED007W",Price=43499,
                Description="Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%"},
            new Product{Id=3,CategoryID=4,Name="LENOVO IdeaPad Gaming",Model="LENOVO IdeaPad Gaming",Price=27999,
                Description="Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)"},
            new Product{Id=4,CategoryID=3,Name="NOTEBOOK-HP-AMD-15s",Model="eq2009ne",Price=16666,
                Description="AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad"}
        };
        builder.Entity<Product>().HasData(ProductList);
        #endregion
    }
}
