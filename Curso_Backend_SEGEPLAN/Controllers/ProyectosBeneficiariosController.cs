using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.ProyectosBeneficiarios.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.ProyetosBeneficiarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProyectosBeneficiariosController : ControllerBase
    {
        private readonly IProyectosBeneficiariosHandler _proyectosBeneficiariosHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProyectosBeneficiariosController(IProyectosBeneficiariosHandler proyectosBeneficiariosHandler, IConfiguration configuration, IMapper mapper)
        {
            this._proyectosBeneficiariosHandler = proyectosBeneficiariosHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProyectoBeneficiario[]>> Get()
        {
            try
            {
                var projectBeneficiaries = await this._proyectosBeneficiariosHandler.GetAsync();

                return Ok(projectBeneficiaries);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{proyectoId:int}/{beneficiarioId:int}")]
        public async Task<ActionResult<ProyectoBeneficiario>> GetByProjectIdAndBeneficiaryId([FromRoute] int proyectoId, [FromRoute] int beneficiarioId)
        {
            try
            {
                if (proyectoId <= 0 || beneficiarioId <= 0)
                    throw new ArgumentException("ProyectoId ó BeneficiarioI es inválido");

                var proyectoBeneficiario = await this._proyectosBeneficiariosHandler.GetByProjectIdAndBeneficiaryIdAsync(proyectoId, beneficiarioId);

                if (proyectoBeneficiario == null)
                    return NotFound();

                return Ok(proyectoBeneficiario);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProyectoBeneficiarioCreationRequest proyectoBeneficiarioCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var proyectoBeneficiario = this._mapper.Map<ProyectoBeneficiario>(proyectoBeneficiarioCreationRequest);
                var rowsAffected = await this._proyectosBeneficiariosHandler.CreateAsync(proyectoBeneficiario);

                return rowsAffected > 0
                                        ? Created($"{this._configuration["HostURL"]}/ProyectosBeneficiarios/{proyectoBeneficiario.ProyectoID}/{proyectoBeneficiario.BeneficiarioID}",
                                                  new { ProyectoID = proyectoBeneficiario.ProyectoID, BeneficiarioID = proyectoBeneficiario.BeneficiarioID })
                                        : BadRequest("Error cuando se creaba el registro");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}