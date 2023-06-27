using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
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

            DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
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
                Role = Role.Customer,
            };

            var result = manager.CreateAsync(user, credentials.Password).Result;
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Role", user.Role.ToString()),
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

        #region Forget Password

        [HttpPost]
        [Route("Forget_Password")]
        public async Task<ActionResult> Forget_Password(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is Invalid!!!!");
            }


            User? user = await manager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("User not found with the given email.");
            }

            var token = await manager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);


            //var callBackURL = Url.Page("/ResetPassword", pageHandler: null,  validToken,  Request.Scheme);

            string backUrl = $"{configuration.GetValue<string>("AppURL")}/Reset_Password?email={email}&token={validToken}";

            await mailingService.SendEmailAsync(email, "Reset Password", "<h1>Follow this Instruction to Reset Password</h1>" + $"To Reset Password <a href='{backUrl}'>Click here</a>");

            return Ok("Reset Password Email has been Sent Successfully!!!");
        }
        #endregion

        #region Reset Password

        [HttpPost]
        [Route("Reset_Password")]
        public async Task<ActionResult> ResetPassword([FromForm] UserResetPasswordDto userResetPasswordDto)
        {
            User? user = await manager.FindByEmailAsync(userResetPasswordDto.Email);
            if (user is null)
            {
                return NotFound("User Not Found!!!");
            }

            if (userResetPasswordDto.NewPassword != userResetPasswordDto.ConfirmPassword)
            {
                return BadRequest("Passwored dosen't Match Confirmation!");
            }

            var result =await manager.ResetPasswordAsync(user, userResetPasswordDto.Token, userResetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password has been Reset Successfully!!!");
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
