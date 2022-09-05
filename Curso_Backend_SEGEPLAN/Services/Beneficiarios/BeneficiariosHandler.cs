using Curso_Backend_SEGEPLAN.DataContext;
using Curso_Backend_SEGEPLAN.Entities;
using Curso_Backend_SEGEPLAN.Services.BaseService;
using Microsoft.EntityFrameworkCore;

namespace Curso_Backend_SEGEPLAN.Services.Beneficiarios
{
    public class BeneficiariosHandler : BaseService<Beneficiario>, IBeneficiariosHandler
    {
        public BeneficiariosHandler(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
