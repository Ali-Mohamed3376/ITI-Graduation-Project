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
        private readonly IUnitOfWork unitOfWork;

        public UserController(IConfiguration configuration,
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
                Token = token,
                Role = user.Role.ToString()
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
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var claimsResult = manager.AddClaimsAsync(user, claims).Result;

            if (!claimsResult.Succeeded)
            {
                return BadRequest(claimsResult.Errors);
            }

            return Ok("Register Succeded!!!");
        }


        #endregion

        #region Forget Password

        [HttpPost]
        [Route("Forget_Password")]
        public async Task<ActionResult> Forget_Password([FromForm] string email)
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

            var random = new Random();
            var code = random.Next(10000, 99999).ToString();
            user.Code = code;
            unitOfWork.Savechanges();

            await mailingService.SendEmailAsync(email, "Reset Password", $"Your Code is {code}");

            var response = new
            {
                message = "Reset Password Email has been Sent Successfully!!!"
            };

            return Ok(response);
        }
        #endregion

        #region Check Code For User
        [HttpPost]
        [Route("Check_Code")]
        public async Task<ActionResult> Check_Code([FromBody] ConfirmCodeDto confirmCodeDto)
        {
            if (string.IsNullOrEmpty(confirmCodeDto.Email))
            {
                return BadRequest("Email is Invalid!!!!");
            }

            User? user = await manager.FindByEmailAsync(confirmCodeDto.Email);
            if (user is null)
            {
                return NotFound("User not found with the given email.");
            }

            if (user.Code != confirmCodeDto.Code)
            {
                return BadRequest("Invalid Code!!!");
            }

            var response = new
            {
                message = "Code is Valid"
            };

            return Ok(response);

        }
        #endregion

        #region Reset Password

        //[HttpGet]
        //[Route("Reset_Password")]
        //public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        //{
        //    //User? user = await manager.FindByEmailAsync(userResetPasswordDto);
        //    //if (user is null)
        //    //{
        //    //    return NotFound("User Not Found!!!");
        //    //}

        //    //if (userResetPasswordDto.NewPassword != userResetPasswordDto.ConfirmPassword)
        //    //{
        //    //    return BadRequest("Passwored dosen't Match Confirmation!");
        //    //}

        //    //var token = await manager.GeneratePasswordResetTokenAsync(user);
        //    //var encodedToken = Encoding.UTF8.GetBytes(token);
        //    //var validToken = WebEncoders.Base64UrlEncode(encodedToken);

        //    //var result = await manager.ResetPasswordAsync(user, validToken, userResetPasswordDto.NewPassword);

        //    //if (!result.Succeeded)
        //    //{
        //    //    return BadRequest(result.Errors);
        //    //}

        //    return Ok("Password has been Reset Successfully!!!");
        //}

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