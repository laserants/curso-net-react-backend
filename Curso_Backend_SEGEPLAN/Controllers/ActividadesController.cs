using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Actividades.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.Actividades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActividadesController : ControllerBase
    {
        private readonly IActividadesHandler _actividadesHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ActividadesController(IActividadesHandler actividadesHandler, IConfiguration configuration, IMapper mapper)
        {
            this._actividadesHandler = actividadesHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Actividad[]>> Get()
        {
            try
            {
                var actividades = await this._actividadesHandler.GetAsync(null, string.Empty);

                return Ok(actividades);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{actividadId:int}")]
        public async Task<ActionResult<Actividad>> GetById(int actividadId)
        {
            try
            {
                if (actividadId <= 0)
                    throw new ArgumentException("Actividad Id es inválido");

                var actividad = await this._actividadesHandler.GetByIdAsync(actividadId);

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
        public async Task<ActionResult<int>> Create([FromBody] ActividadCreationRequest actividadCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var actividad = this._mapper.Map<Actividad>(actividadCreationRequest);

                var actividadCreada = await this._actividadesHandler.CreateAsync(actividad);

                return actividadCreada > 0
                                ? Created($"{this._configuration["HostURL"]}/Actividades/{actividad.ActividadID}", new { ActividadID = actividad.ActividadID })
                                : BadRequest("Error cuando se intentaba crear la actividad");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{actividadId:int}")]
        public async Task<IActionResult> Update([FromRoute] int actividadId, [FromBody] ActividadUpdateRequest actividadUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (actividadId != actividadUpdateRequest.ActividadID)
                    throw new ArgumentException("Actividad Id no coincide con ActividadId de la URL");

                var actividad = this._mapper.Map<Actividad>(actividadUpdateRequest);
                await this._actividadesHandler.UpdateAsync(actividad);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{actividadId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int actividadId)
        {
            try
            {
                if (actividadId <= 0)
                    throw new ArgumentException("ActividadId es Inválido");

                var existeActividad = await this._actividadesHandler.ExistRecordAsync(x => x.ActividadID == actividadId);

                if (!existeActividad)
                    return NotFound();

                await this._actividadesHandler.DeleteAsync(actividadId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
