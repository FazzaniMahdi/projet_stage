using JobHosting.Models;
using JobHosting.Models.Repositories;
using JobHosting.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

// not a job well done but a job nonetheless
// -yours truly, jhon darksoul

namespace JobHosting.Controllers
{
    [ApiController]
    [Route("Api/Authentication")]
    [AllowAnonymous]
    public class UserAccountsController : Controller
    {
        private readonly UserManager<UserAccount> jobHuntingUM;
        private readonly SignInManager<UserAccount> jobHuntingSM;
        private readonly IConfiguration configuration;
        private readonly JobHostingDbContext context;
        private readonly IHttpContextAccessor _http;
        public UserAccountsController(UserManager<UserAccount> jobHuntingUM, SignInManager<UserAccount> jobHuntingSM, IConfiguration configuration, JobHostingDbContext context, IHttpContextAccessor http)
        {
            this.jobHuntingSM = jobHuntingSM;
            this.jobHuntingUM = jobHuntingUM;
            this.configuration = configuration;
            this.context = context;
            _http = http;
        }
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<ActionResult<UserAccount>> SignUp([FromBody] SignUpViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid data provided");
                }
                var existingEmail = await context.UserAccounts.FirstOrDefaultAsync(user => user.Email == model.Email);
                if ( existingEmail == null)
                {
                    UserAccount user = new(model.Email, model.UserName, model.UserType);
                    Console.WriteLine("------------------User's Info: "+user.Email + " " + user.UserName + " " + user.UserType + " " + model.Password);
                    var res = await jobHuntingUM.CreateAsync(user, model.Password);
                    if (res.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, "SignUp successfull, please sign in to continue");
                    }
                    else
                    {
                        var errors = "";
                        foreach (var error in res.Errors)
                        {
                            errors += error.Description;
                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, errors);// $"An error has occured while creating the new user {errors}");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status409Conflict, "User with that email already exists");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // this should be client side since it's literally returning null
        [HttpPost("SignOut")]
        [Authorize]
        public async Task<ActionResult<UserAccount>> Logout()
        {
            /*try
            {
                Console.WriteLine(1);
                var handler = new JwtSecurityTokenHandler();
                var headers = Request.Headers;
                var authHeader = headers["Authorization"].ToString();

                Console.WriteLine(authHeader);
                var tokenString = authHeader.StartsWith("Bearer ") ? authHeader.Substring(7) : authHeader;
                Console.WriteLine(tokenString);
                var token = handler.ReadToken(tokenString);

                Console.WriteLine(3);

                if(token != null)
                {
                    return StatusCode(StatusCodes.Status202Accepted, "you have been signed out");
                }
                return Unauthorized();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }*/
            return null;
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<ActionResult<UserAccount>> SignIn([FromBody] LoginViewModel model)
        {
            try {
                var user = await context.UserAccounts.FirstOrDefaultAsync(user => user.UserName == model.UserName);
                if (user != null)
                {
                    var pwdValid = await jobHuntingUM.CheckPasswordAsync(user, model.Password);
                    if (pwdValid)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, configuration["JwtSettings:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim("Email", user.Email),
                            new Claim("Username", user.UserName),
                            new Claim("JHFullName",user.JHFullName),
                            new Claim("JHResume", user.JHResume),
                            new Claim("JLName", user.JListerName),
                            new Claim("JLWebsite", user.JListerWebsite),
                            new Claim("Type", user.UserType),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: configuration["JwtSettings:Issuer"],
                            audience: configuration["JwtSettings:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: creds);

                        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        return Ok(new { Token = tokenString, User= user });
                    }
                    else
                    {
                        return BadRequest("Wrong Password");
                    }
                }else
                {
                    return NotFound();
                }
            }catch(Exception e) {
                return BadRequest($"error {e.Message}");
            }
        }

        [HttpGet("UserInfo")]
        [Authorize]
        public ActionResult<UserAccount> GetCurrentUser()
        {   
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if(string.IsNullOrEmpty(authHeader) || authHeader.Length <= 16)
                {
                    return Unauthorized();
                }

                var handler = new JwtSecurityTokenHandler();
                var tokenString = authHeader.StartsWith("Bearer ") ? authHeader.Substring(7) : authHeader;
                var token = handler.ReadToken(tokenString) as JwtSecurityToken;

                if(token.ValidTo <  DateTime.UtcNow)
                {
                    return BadRequest("your session has expired");
                }
                
                // this comes from the token
                var userId = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                var user = context.UserAccounts.FirstOrDefault(user => user.Id == userId);
                Console.WriteLine(user);

                return Ok(new
                    { Token = token, User = user}
                );
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Users/{Id}")]
        [AllowAnonymous]
        public async Task<ActionResult<JobListing>> GetJobListing(string Id)
        {
            try
            {
                Console.WriteLine("id: " + Id);
                var res = await context.UserAccounts.FirstOrDefaultAsync(user => user.Id == Id);
                Console.WriteLine(res);
                if (res != null)
                    return Ok(res);
                else
                    return NotFound($"the user with the id {Id} does not exist");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error retrieving data from the db" + e.Message + "\n" + e);
            }
        }
    }
}
