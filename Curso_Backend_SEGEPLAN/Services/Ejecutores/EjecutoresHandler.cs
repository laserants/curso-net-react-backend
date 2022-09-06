using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Services.BaseService;

namespace Curso_Backend_SEGEPLAN.Services.Ejecutores
{
    public class EjecutoresHandler : BaseService<Ejecutor>, IEjecutoresHandler
    {
        public EjecutoresHandler(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        { }
    }
}
