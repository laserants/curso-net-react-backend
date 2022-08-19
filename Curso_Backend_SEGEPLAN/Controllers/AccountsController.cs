using Curso_Backend_SEGEPLAN.DTOs.Requests;
using Curso_Backend_SEGEPLAN.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public IConfiguration _configuration;

        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentialsRequest userCredentialsRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new IdentityUser()
                {
                    UserName = userCredentialsRequest.Email,
                    Email = userCredentialsRequest.Email
                };

                var creationUserResult = await this._userManager.CreateAsync(user, userCredentialsRequest.Password);

                if (creationUserResult.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, "Invitado");

                    var tokenResponse = this.BuildToken(userCredentialsRequest);

                    return Created($"{this._configuration["HostURL"]}/Accounts/{user.Id}", tokenResponse);
                }

                return BadRequest(creationUserResult.Errors);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        private AuthenticationResponse BuildToken(UserCredentialsRequest userCredentialsRequest)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", userCredentialsRequest.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var securityToken = new JwtSecurityToken(issuer: null,
                                                     audience: null,
                                                     claims: claims,
                                                     expires: expiration,
                                                     signingCredentials: credentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiration
            };
        }
    }
}
