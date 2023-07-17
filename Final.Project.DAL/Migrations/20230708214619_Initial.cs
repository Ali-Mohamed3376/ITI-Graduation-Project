using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Final.Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultAddress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliverdDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_UserAddresses_UserAddressId",
                        column: x => x.UserAddressId,
                        principalTable: "UserAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProductsCarts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductsCarts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_UserProductsCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProductsCarts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "wishLists",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishLists", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_wishLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishLists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductDetails",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProductDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.UserId, x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Dell" },
                    { 3, "HP" },
                    { 4, "Lenovo" },
                    { 5, "ASUS" },
                    { 6, "Acer" },
                    { 7, "Microsoft" },
                    { 8, "MSI" },
                    { 9, "Razer" },
                    { 10, "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryID", "Description", "Discount", "Model", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 4, "Processor AMD Ryzen™ 7 5800H(8C / 16T, 3.2 / 4.4GHz, 4MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA® GeForce RTX™ 3060 6GB GDDR6, Boost Clock 1425 / 1702MHz, TGP 130W\r\nMemory\r\n2x 8GB SO-DIMM DDR4-3200\r\nUp to 32GB DDR4-3200 offering\r\nStorage\r\n1TB SSD M.2 2280 PCIe® 3.0x4 NVMe®\r\n", 0m, "82JQ00TQED", "LENOVO Legion 5 Pro", 46999m },
                    { 2, 5, "Processor: Intel® Core™ i7-1260P 12th Generation 12C / 16T Processor 2.1 GHz (18M Cache, up to 4.7 GHz, 4P+8E cores)\r\nGraphics: \"Intel® Iris Xe Graphics\"\r\nMemory: 16GB LPDDR5 on board\r\nStorage: 1TB M.2 NVMe™ PCIe® 3.0 SSD\r\nDisplay: 14.0-inch, 2.8K (2880 x 1800) OLED 16:10 aspect ratio, 0.2ms response time, 90Hz refresh rate, 400nits, 600nits HDR peak brightness, 100% DCI-P3 /touch screen, (Screen-to-body ratio)90%", 0m, "UX3402ZA-OLED007W", "Asus ZenBook 14 UX3402ZA", 43499m },
                    { 3, 4, "Processor\r\nAMD Ryzen 5 5600H (6C / 12T, 3.3 / 4.2GHz, 3MB L2 / 16MB L3)\r\nGraphics\r\nNVIDIA GeForce RTX 3050 Ti 4GB GDDR6, Boost Clock 1485 / 1597.5MHz, TGP 85W\r\nMemory\r\n1x 8GB SO-DIMM DDR4-3200\r\nStorage\r\n256GB SSD M.2 2242 PCIe 3.0x4 NVMe + 1TB HDD\r\nDisplay\r\n15.6\" FHD (1920x1080) IPS 250nits Anti-glare, 45% NTSC, 120Hz\r\nOperating System\r\nWindows 11 Home, English\r\nKeyboard\r\nWhite Backlit, English (US)", 0m, "LENOVO IdeaPad Gaming", "LENOVO IdeaPad Gaming", 27999m },
                    { 4, 3, "AMD Ryzen™ 7 5700U (up to 4.3 GHz max boost clock, 8 MB L3 cache, 8 cores, 16 threads) 1 2 \r\nIntegrated,AMD Radeon™ Graphics .8 GB DDR4-3200 MHz RAM (1 x 8 GB) 512 GB PCIe® NVMe™ M.2 SSD\r\n39.6 cm (15.6\") diagonal, FHD (1920 x 1080), micro-edge, anti-glare, 250 nits, 45% NTSC 3\r\nFull-size, jet black keyboard with numeric keypad", 0m, "eq2009ne", "NOTEBOOK-HP-AMD-15s", 16666m },
                    { 5, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 50m, "MacBook Air", "Apple MacBook Air", 40000m },
                    { 6, 1, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 20m, "MacBook Pro", "Apple MacBook Pro", 80000m },
                    { 7, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 0m, "XPS 13", "Dell XPS 13", 40000m },
                    { 8, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 13m, "Inspiron 15", "Dell Inspiron 15", 35000m },
                    { 9, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 15m, "Spectre x360", "HP Spectre x360", 25000m },
                    { 10, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 60m, "Pavilion 14", "HP Pavilion 14", 15000m },
                    { 11, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 10m, "MacBook Air", "Apple MacBook Air", 28000m },
                    { 12, 1, "The Apple MacBook Pro is a high-performance laptop loved by professionals.", 12m, "MacBook Pro", "Apple MacBook Pro", 30000m },
                    { 13, 1, "The Apple iMac is a sleek and powerful all-in-one desktop computer.", 0m, "iMac", "Apple iMac", 16000m },
                    { 14, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 90m, "XPS 13", "Dell XPS 13", 14000m },
                    { 15, 2, "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.", 18m, "Inspiron 15", "Dell Inspiron 15", 30000m },
                    { 16, 2, "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.", 20m, "G5 Gaming Desktop", "Dell G5 Gaming Desktop", 38000m },
                    { 17, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 19m, "Spectre x360", "HP Spectre x360", 26000m },
                    { 18, 3, "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.", 0m, "Pavilion 14", "HP Pavilion 14", 6000m },
                    { 19, 3, "The HP EliteBook 840 is a business laptop with top-notch security features.", 80m, "EliteBook 840", "HP EliteBook 840", 50000m },
                    { 20, 1, "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.", 15m, "MacBook Air", "Apple MacBook Air", 18000m },
                    { 21, 2, "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.", 5m, "XPS 13", "Dell XPS 13", 13000m },
                    { 22, 3, "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.", 10m, "Spectre x360", "HP Spectre x360", 12000m },
                    { 23, 4, "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.", 6m, "ThinkCentre M720", "Lenovo ThinkCentre M720", 15000m },
                    { 24, 5, "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.", 60m, "ROG Strix G15", "ASUS ROG Strix G15", 80000m },
                    { 25, 6, "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.", 15m, "Aspire TC", "Acer Aspire TC", 18000m },
                    { 26, 2, "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.", 10m, "Inspiron 27", "Dell Inspiron 27", 22000m },
                    { 27, 5, "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.", 15m, "ZenBook Pro", "ASUS ZenBook Pro", 28000m },
                    { 28, 3, "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.", 80m, "Pavilion Gaming Desktop", "HP Pavilion Gaming Desktop", 15000m },
                    { 29, 4, "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.", 12m, "Legion Y540", "Lenovo Legion Y540", 20000m },
                    { 30, 1, "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.", 20m, "iMac", "Apple iMac", 24000m },
                    { 31, 2, "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.", 10m, "G5 Gaming Laptop", "Dell G5 Gaming Laptop", 18000m },
                    { 32, 3, "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.", 15m, "Envy 15", "HP Envy 15", 16000m },
                    { 33, 4, "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.", 50m, "IdeaCentre 5", "Lenovo IdeaCentre 5", 8990m },
                    { 34, 5, "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.", 0m, "VivoBook S15", "ASUS VivoBook S15", 9990m },
                    { 35, 10, "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.", 10m, "Galaxy Book Pro", "Samsung Galaxy Book Pro", 14990m },
                    { 36, 2, "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.", 20m, "Alienware Aurora R10", "Dell Alienware Aurora R10", 28000m },
                    { 37, 3, "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.", 15m, "Omen 15", "HP Omen 15", 17999m },
                    { 38, 1, "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.", 10m, "MacBook Air", "Apple MacBook Air", 12990m },
                    { 39, 9, "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.", 15m, "Blade 15", "Razer Blade 15", 23990m },
                    { 40, 4, "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.", 60m, "ThinkPad X1 Carbon", "Lenovo ThinkPad X1 Carbon", 18990m },
                    { 41, 5, "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.", 0m, "ROG Zephyrus G14", "ASUS ROG Zephyrus G14", 17000m },
                    { 42, 8, "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.", 19m, "GS66 Stealth", "MSI GS66 Stealth", 23999m },
                    { 43, 8, "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.", 17m, "Prestige 14", "MSI Prestige 14", 15990m },
                    { 44, 7, "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.", 14m, "Surface Laptop 4", "Microsoft Surface Laptop 4", 23000m },
                    { 45, 7, "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.", 0m, "Surface Pro 7", "Microsoft Surface Pro 7", 20000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductDetails_ProductId",
                table: "OrderProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserAddressId",
                table: "Orders",
                column: "UserAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductsCarts_ProductId",
                table: "UserProductsCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_wishLists_ProductId",
                table: "wishLists",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "OrderProductDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "UserProductsCarts");

            migrationBuilder.DropTable(
                name: "wishLists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
