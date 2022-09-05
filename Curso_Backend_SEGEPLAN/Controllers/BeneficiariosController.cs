using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Actividades.Request;
using Curso_Backend_SEGEPLAN.DTOs.Beneficiarios.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.Actividades;
using Curso_Backend_SEGEPLAN.Services.Beneficiarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BeneficiariosController : ControllerBase
    {
        private readonly IBeneficiariosHandler _beneficiariosHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BeneficiariosController(IBeneficiariosHandler beneficiariosHandler, IConfiguration configuration, IMapper mapper)
        {
            this._beneficiariosHandler = beneficiariosHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Beneficiario[]>> Get()
        {
            try
            {
                var actividades = await this._beneficiariosHandler.GetAsync(null, string.Empty);

                return Ok(actividades);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{beneficiarioId:int}")]
        public async Task<ActionResult<Actividad>> GetById(int beneficiarioId)
        {
            try
            {
                if (beneficiarioId <= 0)
                    throw new ArgumentException("Beneficiario Id es inválido");

                var actividad = await this._beneficiariosHandler.GetByIdAsync(beneficiarioId);

                if (actividad == null)
                    return NotFound();

                return Ok(actividad);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] BeneficiarioCreationRequest beneficiarioCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var beneficiario = this._mapper.Map<Beneficiario>(beneficiarioCreationRequest);

                var actividadCreada = await this._beneficiariosHandler.CreateAsync(beneficiario);

                return actividadCreada > 0
                                ? Created($"{this._configuration["HostURL"]}/Beneficiarios/{beneficiario.BeneficiarioID}", new { BeneficiarioID = beneficiario.BeneficiarioID })
                                : BadRequest("Error cuando se intentaba crear el beneficiario");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{beneficiarioId:int}")]
        public async Task<IActionResult> Update([FromRoute] int beneficiarioId, [FromBody] BeneficiarioUpdateRequest beneficiarioUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (beneficiarioId != beneficiarioUpdateRequest.BeneficiarioID)
                    throw new ArgumentException("Beneficiario Id no coincide con Beneficiario de la URL");

                var actividad = this._mapper.Map<Beneficiario>(beneficiarioUpdateRequest);
                await this._beneficiariosHandler.UpdateAsync(actividad);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{beneficiarioId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int beneficiarioId)
        {
            try
            {
                if (beneficiarioId <= 0)
                    throw new ArgumentException("beneficiarioId es Inválido");

                var existeBeneficiario = await this._beneficiariosHandler.ExistRecordAsync(x => x.BeneficiarioID == beneficiarioId);

                if (!existeBeneficiario)
                    return NotFound();

                await this._beneficiariosHandler.DeleteAsync(beneficiarioId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
