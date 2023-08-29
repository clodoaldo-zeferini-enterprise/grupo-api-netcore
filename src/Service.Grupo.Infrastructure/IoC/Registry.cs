using Microsoft.Extensions.DependencyInjection;
using Service.GetAuthorization.Application.UseCases.GetAuthorization;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Request.Grupo.Grupo;
using Service.Grupo.Application.Models.Request.Log;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.UseCases.Grupo;
using Service.Grupo.Infrastructure.Repositories.DB;
using Service.Grupo.Repository.Interfaces.Repositories.DB;
using Service.Grupo.Application.UseCases.Log;


namespace Service.Grupo.Infrastructure.IoC
{
    public static class Registry
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            #region[Registrar Injeção de Dependência - Authentication]
            services.AddTransient<IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse>, GetGetAuthorizationUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<LogRequest, LogOutResponse>, SendLogUseCaseAsync>();

            #endregion[Registrar Injeção de Dependência - Authentication]


            #region[Registrar Injeção de Dependência - Grupo]
            services.AddTransient<IUseCaseAsync<DeleteGrupoRequest, GrupoOutResponse>, DeleteGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<GetGrupoRequest, GrupoOutResponse>, GetGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<InsertGrupoRequest, GrupoOutResponse>, InsertGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<UpdateGrupoRequest, GrupoOutResponse>, UpdateGrupoUseCaseAsync>();
            #endregion[Registrar Injeção de Dependência - Grupo]

        }

        public static void RegisterDatabase(this IServiceCollection services)
        {
            #region[Registrar Injeção de Dependência - Repositorio - DBGrupo]
            services.AddTransient<IGrupoRepository, GrupoRepository>();
            #endregion[Registrar Injeção de Dependência - Repositorio - DBGrupo]

        }


    }
}
