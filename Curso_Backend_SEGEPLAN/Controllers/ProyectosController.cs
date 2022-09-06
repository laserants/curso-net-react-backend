using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Proyectos.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.Proyectos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProyectosController : ControllerBase
    {
        private readonly IProyectosHandler _proyectosHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProyectosController(IProyectosHandler proyectosHandler, IConfiguration configuration, IMapper mapper)
        {
            this._proyectosHandler = proyectosHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Proyecto[]>> Get()
        {
            try
            {
                var proyectos = await this._proyectosHandler.GetAsync(null, "Actividades");

                return Ok(proyectos);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{proyectoId:int}")]
        public async Task<ActionResult<Proyecto>> GetById(int proyectoId)
        {
            try
            {
                if (proyectoId <= 0)
                    return BadRequest("Id del proyecto inválido");

                var proyecto = await this._proyectosHandler.GetByIdAsync(proyectoId);

                if (proyecto == null)
                    return NotFound();

                return Ok(proyecto);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProyectoCreationRequest proyectoCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var proyecto = this._mapper.Map<Proyecto>(proyectoCreationRequest);

                var proyectoCreado = await this._proyectosHandler.CreateAsync(proyecto);

                return proyectoCreado > 0
                                            ? Created($"{this._configuration["HostURL"]}/Proyectos/{proyecto.ProyectoID}", new { proyecto.ProyectoID})
                                            : BadRequest("Ocurrió un error cuando intentaba crear el proyecto");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{proyectoId:int}")]
        public async Task<IActionResult> Update([FromRoute] int proyectoId, [FromBody] Proyecto proyecto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (proyectoId != proyecto.ProyectoID)
                    return BadRequest("Proyecto Id de la url no coincide con el proyecto id del body");

                await this._proyectosHandler.UpdateAsync(proyecto);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{proyectoId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int proyectoId)
        {
            try
            {
                if(proyectoId <= 0)
                    throw new ArgumentException("El proyecto id es invalido");

                var existeProyecto = await this._proyectosHandler.ExistRecordAsync(x => x.ProyectoID == proyectoId);

                if(!existeProyecto)
                    return NotFound();

                await this._proyectosHandler.DeleteAsync(proyectoId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
