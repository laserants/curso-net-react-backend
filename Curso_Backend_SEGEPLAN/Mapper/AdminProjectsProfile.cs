using AutoMapper;
using Curso_Backend_SEGEPLAN.DTOs.Actividades.Request;
using Curso_Backend_SEGEPLAN.DTOs.Beneficiarios.Request;
using Curso_Backend_SEGEPLAN.DTOs.Ejecutores.Request;
using Curso_Backend_SEGEPLAN.DTOs.Proyectos.Request;
using Curso_Backend_SEGEPLAN.DTOs.ProyectosBeneficiarios.Request;
using Curso_Backend_SEGEPLAN.DTOs.ProyectosEjecutores.Request;
using Curso_Backend_SEGEPLAN.Entities;

namespace Curso_Backend_SEGEPLAN.Mapper
{
    public class AdminProjectsProfile : Profile
    {
        public AdminProjectsProfile()
        {
            CreateMap<ProyectoCreationRequest, Proyecto>();
            CreateMap<ProyectoUpdateRequest, Proyecto>();
            CreateMap<ActividadCreationRequest, Actividad>();
            CreateMap<ActividadUpdateRequest, Actividad>();
            CreateMap<BeneficiarioCreationRequest, Beneficiario>();
            CreateMap<BeneficiarioUpdateRequest, Beneficiario>();
            CreateMap<EjecutorCreationRequest, Ejecutor>();
            CreateMap<EjecutorUpdateRequest, Ejecutor>();
            CreateMap<ProyectoBeneficiarioCreationRequest, ProyectoBeneficiario>();
            CreateMap<ProyectoEjecutorCreationRequest, ProyectoEjecutor>();
        }
    }
}
