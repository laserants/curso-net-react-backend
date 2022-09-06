using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Actividades.Request;
using Curso_Backend_SEGEPLAN.DTOs.Beneficiarios.Request;
using Curso_Backend_SEGEPLAN.DTOs.Ejecutores.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.Actividades;
using Curso_Backend_SEGEPLAN.Services.Beneficiarios;
using Curso_Backend_SEGEPLAN.Services.Ejecutores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EjecutoresController : ControllerBase
    {
        private readonly IEjecutoresHandler _ejecutoresHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EjecutoresController(IEjecutoresHandler ejecutoresHandler, IConfiguration configuration, IMapper mapper)
        {
            this._ejecutoresHandler = ejecutoresHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Ejecutor[]>> Get()
        {
            try
            {
                var actividades = await this._ejecutoresHandler.GetAsync(null, string.Empty);

                return Ok(actividades);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{ejecutorId:int}")]
        public async Task<ActionResult<Ejecutor>> GetById(int ejecutorId)
        {
            try
            {
                if (ejecutorId <= 0)
                    throw new ArgumentException("Ejecutor Id es inválido");

                var actividad = await this._ejecutoresHandler.GetByIdAsync(ejecutorId);

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
        public async Task<ActionResult<int>> Create([FromBody] EjecutorCreationRequest ejecutorCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ejecutor = this._mapper.Map<Ejecutor>(ejecutorCreationRequest);

                var actividadCreada = await this._ejecutoresHandler.CreateAsync(ejecutor);

                return actividadCreada > 0
                                ? Created($"{this._configuration["HostURL"]}/Ejecutores/{ejecutor.EjecutorID}", new { EjecutorID = ejecutor.EjecutorID })
                                : BadRequest("Error cuando se intentaba crear el ejecutor");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{ejecutorId:int}")]
        public async Task<IActionResult> Update([FromRoute] int ejecutorId, [FromBody] EjecutorUpdateRequest ejecutorUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (ejecutorId != ejecutorUpdateRequest.EjecutorID)
                    throw new ArgumentException("Ejecutor Id no coincide con Ejecutor de la URL");

                var ejecutor = this._mapper.Map<Ejecutor>(ejecutorUpdateRequest);
                await this._ejecutoresHandler.UpdateAsync(ejecutor);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{ejecutorId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int ejecutorId)
        {
            try
            {
                if (ejecutorId <= 0)
                    throw new ArgumentException("ejecutorId es Inválido");

                var existeEjecutor = await this._ejecutoresHandler.ExistRecordAsync(x => x.EjecutorID == ejecutorId);

                if (!existeEjecutor)
                    return NotFound();

                await this._ejecutoresHandler.DeleteAsync(ejecutorId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
