using AdminProjectsDemo.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;    
            this._configuration = configuration;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<AuthenticationResponse>> CreateAccount([FromBody] UserCredentials userCredentials)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser()
            {
                UserName = userCredentials.Email,
                Email = userCredentials.Email
            };

            var creationUserResult = await this._userManager.CreateAsync(user, userCredentials.Password);

            if (creationUserResult.Succeeded)
                return this.BuildToken(userCredentials);

            return BadRequest(creationUserResult.Errors);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await this._signInManager.PasswordSignInAsync(userCredentials.Email, 
                                                                       userCredentials.Password, 
                                                                       isPersistent: false, 
                                                                       lockoutOnFailure: false);

            if (loginResult.Succeeded)
                return BuildToken(userCredentials);

            return BadRequest("Invalid Login");
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", userCredentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiration
            };
        }
    }
}
