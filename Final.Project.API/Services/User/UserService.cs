using Final.Project.API.Controllers;
using Final.Project.API.Responses;
using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final.Project.API;
public class UserService : IUserService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<User> manager;
    private readonly ILogger<UserController> logger;
    private readonly IMailingService mailingService;
    private readonly IUnitOfWork unitOfWork;


    public UserService(IConfiguration configuration,
                                 UserManager<User> manager,
                                 ILogger<UserController> logger,
                                 IMailingService mailingService,
                                 IUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.manager = manager;
        this.logger = logger;
        this.mailingService = mailingService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<UserManagerResponse> Login(LoginDto loginCredientials)
    {
        // Search by Email and check if user found or Not 
        User? user = await manager.FindByEmailAsync(loginCredientials.Email);
        if (user is null)
        {
            return new UserManagerResponse
            {
                Message = "User Not Found",
                IsSuccess = false,

            };
        }

        // Check On Password
        bool isValiduser = await manager.CheckPasswordAsync(user, loginCredientials.Password);
        if (!isValiduser)
        {
            return new UserManagerResponse
            {
                Message = "Invalid Password!",
                IsSuccess = false,
            };
        }

        // Get claims
        List<Claim> claims = manager.GetClaimsAsync(user).Result.ToList();

        // get Secret Key
        string? secretKey = configuration.GetValue<string>("SecretKey");
        byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
        SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
        JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

        return new UserManagerResponse
        {
            Message = "User Loggedin Successfully",
            IsSuccess = true,
            Data = new TokenDto
            {
                Token = token,
                Role = user.Role.ToString(),
            },
        };
    }

    public async Task<UserManagerResponse> Register(RegisterDto registerCredientials)
    {
        User user = new User
        {
            FName = registerCredientials.FName,
            LName = registerCredientials.LName,
            Email = registerCredientials.Email,
            UserName = registerCredientials.Email,
            Role = Role.Customer,

        };

        var result = await manager.CreateAsync(user, registerCredientials.Password);
        if (!result.Succeeded)
        {
            return new UserManagerResponse
            {
                Errors = result.Errors,
                IsSuccess = false,
            };
        }

        List<Claim> claims = new List<Claim>()
         {
             new Claim(ClaimTypes.NameIdentifier, user.Id),
             new Claim(ClaimTypes.Role, user.Role.ToString()),
         };

        var claimsResult = await manager.AddClaimsAsync(user, claims);

        if (!claimsResult.Succeeded)
        {
            return new UserManagerResponse
            {
                Errors = claimsResult.Errors,
                IsSuccess = false,
            };
        }

        string? secretKey = configuration.GetValue<string>("SecretKey");
        byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
        SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
        JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

        return new UserManagerResponse
        {
            Message = "Register Successfully",
            IsSuccess = true,
            Data = new TokenDto
            {
                Token = token,
                Role = user.Role.ToString(),
            },
        };
    }
}
