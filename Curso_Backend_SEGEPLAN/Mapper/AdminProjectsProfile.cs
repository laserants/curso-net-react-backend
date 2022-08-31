using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Actividades.Request;
using Curso_Backend_SEGEPLAN.DTOs.Proyectos.Request;
using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Mapper
{
    public class AdminProjectsProfile : Profile
    {
        public AdminProjectsProfile()
        {
            CreateMap<ProyectoCreationRequest, Proyecto>();
            CreateMap<ActividadCreationRequest, Actividad>();
            CreateMap<ActividadUpdateRequest, Actividad>();
        }
    }
}
