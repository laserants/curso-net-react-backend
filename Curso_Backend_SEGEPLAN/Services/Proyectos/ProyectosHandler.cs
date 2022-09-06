using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Services.BaseService;

namespace Curso_Backend_SEGEPLAN.Services.Proyectos
{
    public class ProyectosHandler : BaseService<Proyecto>, IProyectosHandler
    {
        public ProyectosHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
}
