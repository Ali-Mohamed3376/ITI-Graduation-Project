using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> manager;
        private readonly ILogger<UserController> logger;
        private readonly IMailingService mailingService;

        public UserController(IConfiguration configuration,
                                 UserManager<User> manager,
                                 ILogger<UserController> logger,
                                 IMailingService mailingService)
        {
            this.configuration = configuration;
            this.manager = manager;
            this.logger = logger;
            this.mailingService = mailingService;
        }

        #region Login
        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenDto> Login(LoginDto loginCredientials)
        {
            // Search by Email and check if user found or Not 
            User? user = manager.FindByNameAsync(loginCredientials.UserName).Result;
            if (user is null) { return BadRequest("User Not Found!!!"); }

            // Check On Password
            bool isValiduser = manager.CheckPasswordAsync(user, loginCredientials.Password).Result;
            if (!isValiduser)
            {
                return BadRequest("Invalid Password!!!");
            }

            // Get claims
            List<Claim> claims = manager.GetClaimsAsync(user).Result.ToList();


            // get Secret Key
            string? secretKey = configuration.GetValue<string>("SecretKey");
            byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
            SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            DateTime exp = DateTime.Now.AddMinutes(60);
            JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

            var currentUser = manager.GetUserAsync(User).Result;
            return new TokenDto
            {
                Token = token
            };
        }
        #endregion

        #region Register

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterDto credentials)
        {

            User user = new User
            {
                FName = credentials.FName,
                LName = credentials.LName,
                UserName = credentials.Email,
                Email = credentials.Email,
            };

            var result = manager.CreateAsync(user, credentials.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Role","Customer"),
            };

            var claimsResult = manager.AddClaimsAsync(user, claims).Result;

            if (!claimsResult.Succeeded)
            {
                return BadRequest(claimsResult.Errors);
            }

            return Ok("Register Succeded!!!");
        }


        #endregion

        #region Logout




        #endregion

        #region Reset Password

        [HttpPost]
        [Route("Reset_Password")]
        public ActionResult ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            User? user = manager.FindByEmailAsync(userResetPasswordDto.Email).Result;
            if (user is null)
            {
                return NotFound("User Not Found!!!");
            }

            var token = manager.GeneratePasswordResetTokenAsync(user).Result;


            var result = manager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword).Result;

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Reset Password Successfully!!!");
        }

        #endregion

        #region Forget Password

        [HttpPost]
        [Route("Forget_Password")]
        public async Task<ActionResult> Forget_Password(UserResetPasswordDto userResetPasswordDto)
        {
            if (string.IsNullOrEmpty(userResetPasswordDto.Email))
            {
                return BadRequest("Email is required.");
            }

            User? user = manager.FindByEmailAsync(userResetPasswordDto.Email).Result;
            if (user is null)
            {
                return NotFound("User not found with the given email.");
            }

            var token = manager.GeneratePasswordResetTokenAsync(user).Result;
            var result = manager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword).Result;

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return  Ok("Password Changed Successfully!!!");
        
        
        }
        #endregion

        #region Sending Email

        [HttpPost]
        [Route("Send_Email")]
        public async Task<ActionResult> SendEmail([FromForm] MailRequestDto mailRequestDto)
        {
           await mailingService.SendEmailAsync(mailRequestDto.ToEmail, mailRequestDto.Subject, mailRequestDto.Body);

            return Ok("Email Sending Successfully!!!"); 
        }


        #endregion
    }
}
