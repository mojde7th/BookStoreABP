using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
 
namespace TodoApp.Application
{
    public class AccountAppService:IAccountAppService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountAppService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountAppService(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, ILogger<AccountAppService> logger, 
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> LoginAsync(LoginDto input)
        {
            var context = _contextAccessor.HttpContext;
            _logger.LogInformation("Log in User:{Username}",
                input.Username);
            var user= await _userManager.FindByNameAsync
                (input.Username);
            if (user == null)
            {
                _logger.LogWarning("User not Found {Username}"
                    , input.Username);
                throw new UnauthorizedAccessException
                    ("Invalid Login Moj");
            }
            var result = await _signInManager.PasswordSignInAsync
                (input.Username, input.Password, false, false);
            if (result.Succeeded) { 
            _logger.LogInformation("login successful Mojsuccess",
                input.Username);
                var token=GenerateMyJWt(user);
                var options = new CookieOptions
                {
                    Domain = "localhost",
                    Expires = DateTime.Now.AddDays(-1),
                    Secure = true,
                    HttpOnly = true
                };
                
               context.Response.Cookies.Append(".AspNetCore.Identity.Application", "", options);
                return token;
                
            }
            _logger.LogWarning("Invalid Login",input.Username);
            throw new UnauthorizedAccessException("Invalid Login");
        }

        private string GenerateMyJWt(IdentityUser user) {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration["Jwt:Key"]));
            var creds=new SigningCredentials(key,SecurityAlgorithms
                .HmacSha256);
            var token=new JwtSecurityToken(
                
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
