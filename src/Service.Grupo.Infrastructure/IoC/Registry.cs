using Microsoft.Extensions.DependencyInjection;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Request.Log;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.UseCases.Empresa;
using Service.Grupo.Application.UseCases.Log;
using Service.Grupo.Infrastructure.Repositories.DB;
using Service.Grupo.Repository.Interfaces.Repositories.DB;
using Service.GetAuthorization.Application.UseCases.GetAuthorization;


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
            services.AddTransient<IUseCaseAsync<DeleteGrupoRequest, EmpresaOutResponse>, DeleteGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<GetGrupoRequest, EmpresaOutResponse>, GetGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<InsertGrupoRequest, EmpresaOutResponse>, InsertGrupoUseCaseAsync>();
            services.AddTransient<IUseCaseAsync<UpdateGrupoRequest, EmpresaOutResponse>, UpdateGrupoUseCaseAsync>();
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
