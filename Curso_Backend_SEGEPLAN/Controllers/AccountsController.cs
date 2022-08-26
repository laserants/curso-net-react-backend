using Curso_Backend_SEGEPLAN.DTOs.Requests;
using Curso_Backend_SEGEPLAN.DTOs.Responses;
using Curso_Backend_SEGEPLAN.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IConfiguration _configuration;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityUser[]>> Get()
        {
            try
            {
                var allUsersIdentity = await this._userManager.Users.ToArrayAsync();

                return Ok(allUsersIdentity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{userId}")]        
        public async Task<ActionResult<IdentityUser>> GetById([FromRoute] string userId)
        {
            try
            {                
                var userIdenittyById = await this._userManager.FindByIdAsync(userId);

                if (userIdenittyById == null)
                    return NotFound("Usuario no encontrado");

                return Ok(userIdenittyById);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] AccountUpdateRequest accountUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var findUserById = await this._userManager.FindByIdAsync(accountUpdateRequest.Id);

                if (findUserById == null)
                    return NotFound("Usuario no encontrado");

                findUserById.UserName = accountUpdateRequest.UserName;

                var userUpdated = await this._userManager.UpdateAsync(findUserById);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete([FromRoute] string userId)
        {
            try
            {
                var findUserById = await this._userManager.FindByIdAsync(userId);

                if (findUserById == null)
                    return NotFound("Usuario no encontrado");

                var userDeleted = await this._userManager.DeleteAsync(findUserById);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentialsRequest userCredentialsRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var loginResult = await this._signInManager.PasswordSignInAsync(userCredentialsRequest.Email,
                                                                                userCredentialsRequest.Password,
                                                                                isPersistent: false,
                                                                                lockoutOnFailure: false);

                if (loginResult.Succeeded)
                    return Ok(this.BuildToken(userCredentialsRequest));

                return BadRequest("Login inválido");
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
