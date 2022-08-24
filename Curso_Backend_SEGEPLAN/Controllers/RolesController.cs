using Curso_Backend_SEGEPLAN.DTOs.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityRole[]>> Get()
        {
            try
            {
                var allRolesIdentity = await this._roleManager.Roles.ToArrayAsync();

                return Ok(allRolesIdentity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{rolId}")]
        public async Task<ActionResult<IdentityUser>> Get([FromRoute] string rolId)
        {
            try
            {
                var roleIdentityById = await this._roleManager.FindByIdAsync(rolId);

                if (roleIdentityById == null)
                    return NotFound("Rol no encontrado");

                return Ok(roleIdentityById);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole>> Create([FromBody] RolCreateRequest rolCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rol = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = rolCreateRequest.Name,
                    NormalizedName = rolCreateRequest.Name
                };

                var creationRolResult = await this._roleManager.CreateAsync(rol);

                if (creationRolResult.Succeeded)
                    return Created($"{this._configuration["HostURL"]}Roles/{rol.Id}", rol);

                return BadRequest(creationRolResult.Errors);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{rolId}")]
        public async Task<ActionResult> Delete([FromRoute] string rolId)
        {
            try
            {
                var findRolById = await this._roleManager.FindByIdAsync(rolId);

                if (findRolById == null)
                    return NotFound("Rol no encontrado");

                var userDeleted = await this._roleManager.DeleteAsync(findRolById);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("AssignRoleToUser")]
        public async Task<ActionResult> AssignRoleToUser([FromBody] AssingRoleToUserRequest assingRoleToUserRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userIdentity = await this._userManager.FindByIdAsync(assingRoleToUserRequest.UserId);
                var addRoleToUser = await this._userManager.AddToRoleAsync(userIdentity, assingRoleToUserRequest.RoleName);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
