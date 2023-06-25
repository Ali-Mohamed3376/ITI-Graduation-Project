using Final.Project.API;
using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region Default Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Repos Services
builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IUserProductsCartRepo, UserProdutsCartRepo>();
builder.Services.AddScoped<IUserAddressRepo, UserAddressRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrdersDetailsRepo, OrdersDetailsRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();





#endregion

#region Managers

//Abdo
builder.Services.AddScoped<IUserProductsCartsManager, UserProductsCartsManager>();
builder.Services.AddScoped<IOrdersManager, OrdersManager>();
builder.Services.AddScoped<IUserAddressesManager, UserAddressesManager>();



//Ali

builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));
builder.Services.AddTransient<IMailingService, MailingService>();







//Adel






//Reham Ahmed
builder.Services.AddScoped<IProductsManager, ProductsManager>();
builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();





//RehamSayed

builder.Services.AddScoped<IUsersManager, UsersManager>();
builder.Services.AddScoped<IReviewsManager, ReviewsManager>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();




//Ahmed



#endregion

#region Database

builder.Services.AddDbContext<ECommerceContext>(options => options

    //.UseSqlServer(@"Server=DESKTOP-85Q5KQD\SS17;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));

    .UseSqlServer(@"Server=.;Database=E-CommerceDB;Trusted_Connection=true;Encrypt=false"));


#endregion

#region Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = false;


}).AddEntityFrameworkStores<ECommerceContext>()
  .AddDefaultTokenProviders();
#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "UserScema"; // for handling Authentication
    options.DefaultChallengeScheme = "UserScema"; // for handling Challenge
}).AddJwtBearer("UserScema", options =>
{

    // Access secretkey to use it to validate Requests
    string? stringKey = builder.Configuration.GetValue<string>("SecretKey");
    byte[] keyASBytes = Encoding.ASCII.GetBytes(stringKey!);
    SymmetricSecurityKey key = new SymmetricSecurityKey(keyASBytes);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = key,
        ValidateIssuer = false, 
        ValidateAudience = false,
    };
});


#endregion

#region Authorization

#endregion


var app = builder.Build();

#region Middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion


app.Run();
