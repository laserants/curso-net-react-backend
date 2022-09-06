using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Services.BaseService;

namespace Curso_Backend_SEGEPLAN.Services.Actividades
{
    public class ActividadesHandler : BaseService<Actividad>, IActividadesHandler
    {
        public ActividadesHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {}
    }
}
