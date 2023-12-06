using Final.Project.API.Responses;
using Final.Project.BL;
using Final.Project.DAL;
using Google.Apis.Auth;
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
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;

        public UserController(IConfiguration configuration,
                                 UserManager<User> manager,
                                 ILogger<UserController> logger,
                                 IMailingService mailingService,
                                 IUnitOfWork unitOfWork, IUserService userService)
        {
            this.configuration = configuration;
            this.manager = manager;
            this.logger = logger;
            this.mailingService = mailingService;
            this.userService = userService;
        }
        #region LoginWithGoogle
        [HttpPost]
        [Route("LoginWithGoogle")]
        public async Task<ActionResult<TokenDto>> LoginWithGoogle(LoginCred credential)
        {
            //validateAsync used to validate  Google Sign-In token
            //this code take the credential jwt that send from Angular and return the data
            //of user like his email,first name,last name, id
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential.Credential);
                if (payload is null)
                {
                    return BadRequest();
                }

                LoginDto loginCredientials = new LoginDto
                {
                    Email = payload.Email,
                    Password = $"{payload.Email}+212154484844545445"
                };
                User? user = manager.FindByEmailAsync(loginCredientials.Email).Result;
                if (user is null) { return NotFound("Invalid Email!"); }

                // Check On Password
                bool isValiduser = manager.CheckPasswordAsync(user, loginCredientials.Password).Result;
                if (!isValiduser)
                {
                    return BadRequest("Invalid Password!");
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
            catch (Exception ex)
            {
                logger.LogError($"Exception: {ex.Message}");
                return BadRequest(ex.Message);

            }
        }
        #endregion

        #region RegisterWithGoogle

        [HttpPost]
        [Route("RegisterWithGoogle")]
        public async Task<ActionResult<TokenDto>> Register([FromBody] LoginCred credential)
        {

            try
            {

                //validateAsync used to validate  Google Sign-In token
                //this code take the credential jwt that send from Angular and return the data
                //of user like his email,first name,last name, id
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential.Credential);
                if (payload is null)
                {
                    return BadRequest();
                }

                //now i will create that user in our database
                // i will put password for all gmail user that equal email+any string
                User user = new User
                {
                    FName = payload.GivenName,
                    LName = payload.FamilyName,
                    Email = payload.Email,
                    UserName = payload.Email,
                    Role = Role.Customer,

                };

                var result = await manager.CreateAsync(user, $"{payload.Email}+212154484844545445");
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

                var claimsResult = await manager.AddClaimsAsync(user, claims);

                if (!claimsResult.Succeeded)
                {
                    return BadRequest(claimsResult.Errors);
                }



                string? secretKey = configuration.GetValue<string>("SecretKey");
                byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
                SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
                JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

                return new TokenDto
                {
                    Token = token,
                    Role = user.Role.ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion


          
        #region Login Old Version
        //[HttpPost]
        //[Route("Login")]
        //public ActionResult<TokenDto> Login(LoginDto loginCredientials)
        //{
        //    // Search by Email and check if user found or Not 
        //    User? user = manager.FindByEmailAsync(loginCredientials.Email).Result;
        //    if (user is null) { return NotFound("Invalid Email!"); }

        //    // Check On Password
        //    bool isValiduser = manager.CheckPasswordAsync(user, loginCredientials.Password).Result;
        //    if (!isValiduser)
        //    {
        //        return BadRequest("Invalid Password!");
        //    }

        //    // Get claims
        //    List<Claim> claims = manager.GetClaimsAsync(user).Result.ToList();

        //    // get Secret Key
        //    string? secretKey = configuration.GetValue<string>("SecretKey");
        //    byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
        //    SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

        //    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
        //    JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

        //    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

        //    var currentUser = manager.GetUserAsync(User).Result;
        //    return new TokenDto
        //    {
        //        Token = token,
        //        Role = user.Role.ToString()
        //    };
        //}
        #endregion

        #region Register Old Version

        //[HttpPost]
        //[Route("Register")]
        //public async Task<ActionResult<TokenDto>> Register([FromBody] RegisterDto credentials)
        //{

        //    User user = new User
        //    {
        //        FName = credentials.FName,
        //        LName = credentials.LName,
        //        Email = credentials.Email,
        //        UserName= credentials.Email,
        //        Role = Role.Customer,

        //    };

        //    var result = await manager.CreateAsync(user, credentials.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Role, user.Role.ToString()),
        //    };

        //    var claimsResult = await manager.AddClaimsAsync(user, claims);

        //    if (!claimsResult.Succeeded)
        //    {
        //        return BadRequest(claimsResult.Errors);
        //    }



        //    string? secretKey = configuration.GetValue<string>("SecretKey");
        //    byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
        //    SymmetricSecurityKey key = new SymmetricSecurityKey(keyAsBytes);

        //    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    DateTime exp = DateTime.Now.AddDays(20);//expire after 20days
        //    JwtSecurityToken jwtSecurity = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: exp);

        //    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

        //    return new TokenDto
        //    {
        //        Token = token,
        //        Role = user.Role.ToString()
        //    };
        //}


        #endregion

        #region Forget Password Old Version

        //[HttpPost]
        //[Route("Forget_Password")]
        //public async Task<ActionResult> Forget_Password([FromForm] string email)
        //{

        //    if (string.IsNullOrEmpty(email))
        //    {
        //        return BadRequest("Email is Invalid!!!!");
        //    }

        //    User? user = await manager.FindByEmailAsync(email);
        //    if (user is null)
        //    {
        //        return NotFound("User not found with the given email.");
        //    }

        //    var random = new Random();
        //    var code = random.Next(10000, 99999).ToString();
        //    user.Code = code;
        //    unitOfWork.Savechanges();

        //    await mailingService.SendEmailAsync(email, "Reset Password", $"Your Code is {code}");

        //    var response = new
        //    {
        //        message = "Reset Password Email has been Sent Successfully!!!"
        //    };

        //    return Ok(response);
        //}

        #endregion
        
        #region Check Code For User Old Version
        //[HttpPost]
        //[Route("Check_Code")]
        //public async Task<ActionResult> Check_Code([FromBody] ConfirmCodeDto confirmCodeDto)
        //{
        //    if (string.IsNullOrEmpty(confirmCodeDto.Email))
        //    {
        //        return BadRequest("Email is Invalid!!!!");
        //    }

        //    User? user = await manager.FindByEmailAsync(confirmCodeDto.Email);
        //    if (user is null)
        //    {
        //        return NotFound("User not found with the given email.");
        //    }

        //    if (user.Code != confirmCodeDto.Code)
        //    {
        //        return BadRequest("Invalid Code!!!");
        //    }

        //    var response = new
        //    {
        //        message = "Code is Valid"
        //    };

        //    return Ok(response);

        //}
        #endregion
        
        #region Reset Password Old Version

        //[HttpPost]
        //[Route("Reset_Password")]
        //public async Task<ActionResult> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        //{
        //    User? user = await manager.FindByEmailAsync(userResetPasswordDto.Email);
        //    if (user is null)
        //    {
        //        return NotFound("user not found!!!");
        //    }

        //    if (userResetPasswordDto.NewPassword != userResetPasswordDto.ConfirmNewPassword)
        //    {
        //        return BadRequest("passwored dosen't match confirmation!");
        //    }

        //    var token = await manager.GeneratePasswordResetTokenAsync(user);

        //    var result = await manager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    var response = new
        //    {
        //        message = "Password has been Reset Successfully!!!"
        //    };

        //    return Ok(response);
        //}

        #endregion

        #region New Login Version
        [HttpPost("Login")]
        public async Task<ActionResult<UserManagerResponse>> Login(LoginDto loginCredientials)
        {
            var result = await userService.Login(loginCredientials);
            return result.IsSuccess ? Ok(result) : BadRequest(result);    
        }
        #endregion

        #region New Register Version

        [HttpPost("Register")]
        public async Task<ActionResult<UserManagerResponse>> Register([FromBody] RegisterDto credentials)
        {
            var result = await userService.Register(credentials);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Forget Password Version
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<UserManagerResponse>> ForgetPassword([FromForm] string email)
        {
            var result = await userService.Forget_Password(email);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Check Code For User Version
        [HttpPost("CheckCode")]
        public async Task<ActionResult<UserManagerResponse>> CheckCode([FromBody] ConfirmCodeDto confirmCodeDto)
        {
            var result = await userService.Check_Code(confirmCodeDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Reset Password Version

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<UserManagerResponse>> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            var result = await userService.Reset_Password(userResetPasswordDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        #endregion

        #region Sending Email

        [HttpPost]
        [Route("SendEmail")]
        public async Task<ActionResult<UserManagerResponse>> SendEmail([FromForm] MailRequestDto mailRequestDto)
        {
            var result = await mailingService.SendEmailAsync(mailRequestDto.ToEmail, mailRequestDto.Subject, mailRequestDto.Body) ;
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        #endregion

    }

    public class LoginCred
    {
        public string Credential { get; set; } = string.Empty;
    }
}