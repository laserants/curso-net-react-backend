using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.ProyectosEjecutores.Request;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Extensions;
using Curso_Backend_SEGEPLAN.Services.ProyectosEjecutores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso_Backend_SEGEPLAN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProyectosEjecutoresController : ControllerBase
    {
        private readonly IProyectosEjecutoresHandler _proyectosEjecutoresHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProyectosEjecutoresController(IProyectosEjecutoresHandler proyectosEjecutoresHandler, IConfiguration configuration, IMapper mapper)
        {
            this._proyectosEjecutoresHandler = proyectosEjecutoresHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProyectoEjecutor[]>> Get()
        {
            try
            {
                var proyectosEjecutores = await this._proyectosEjecutoresHandler.GetAsync();

                return Ok(proyectosEjecutores);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{proyectoId:int}/{ejecutorId:int}")]
        public async Task<ActionResult<ProyectoEjecutor>> GetByProjectIdAndExecutorIdAsync([FromRoute] int proyectoId, [FromRoute] int ejecutorId)
        {
            try
            {
                if (proyectoId <= 0 || ejecutorId <= 0)
                    throw new ArgumentException("ProyectoId o EjecutorId son inválidos");

                var proyectoEjecutor = await this._proyectosEjecutoresHandler.GetByProjectIdAndExecutorIdAsync(proyectoId, ejecutorId);

                if (proyectoEjecutor == null)
                    return NotFound();

                return Ok(proyectoEjecutor);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProyectoEjecutorCreationRequest proyectoEjecutorCreationRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var proyectoEjecutor = this._mapper.Map<ProyectoEjecutor>(proyectoEjecutorCreationRequest);
                var filasAfectadas = await this._proyectosEjecutoresHandler.CreateAsync(proyectoEjecutor);

                return filasAfectadas > 0
                                ? Created($"{this._configuration["HostURL"]}/ProyectosEjecutores/{proyectoEjecutor.ProyectoID}/{proyectoEjecutor.EjecutorID}", new { ProyectoId = proyectoEjecutor.ProyectoID, EjecutorId = proyectoEjecutor.EjecutorID })
                                : BadRequest("Error cuando se intentaba crear la relación entre Proyecto y Ejecutor");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
