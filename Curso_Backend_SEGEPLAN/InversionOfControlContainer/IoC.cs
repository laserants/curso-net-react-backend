using Curso_Backend_SEGEPLAN.Services.Actividades;
using Curso_Backend_SEGEPLAN.Services.Beneficiarios;
using Curso_Backend_SEGEPLAN.Services.Ejecutores;
using Curso_Backend_SEGEPLAN.Services.Proyectos;
using Curso_Backend_SEGEPLAN.Services.ProyectosEjecutores;
using Curso_Backend_SEGEPLAN.Services.ProyetosBeneficiarios;

namespace Curso_Backend_SEGEPLAN.InversionOfControlContainer
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IProyectosHandler, ProyectosHandler>();
            services.AddTransient<IActividadesHandler, ActividadesHandler>();
            services.AddTransient<IBeneficiariosHandler, BeneficiariosHandler>();
            services.AddTransient<IEjecutoresHandler, EjecutoresHandler>();
            services.AddTransient<IProyectosBeneficiariosHandler, ProyectosBeneficiariosHandler>();
            services.AddTransient<IProyectosEjecutoresHandler, ProyectosEjecutoresHandler>();

            return services;
        }
    }
}
