using Microsoft.Extensions.DependencyInjection;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.UseCases.Empresa;

namespace Service.Grupo.Console.Service
{
    internal class GetGrupoService : IDisposable
    {
        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~GetGrupoService()
        {
            Dispose(false);
        }
        #endregion

        private readonly IServiceProvider _serviceProvider;
        private readonly GetGrupoRequest _getEmpresaRequest;

        private GetGrupoService() { }

        public GetGrupoService(IServiceProvider serviceProvider, GetGrupoRequest getEmpresaRequest)
        {
            _serviceProvider = serviceProvider;
            _getEmpresaRequest = getEmpresaRequest;
        }

        public async Task<GrupoResponse?> ExecuteAsync()
        {
            var service = ActivatorUtilities.CreateInstance<GetGrupoUseCaseAsync>(_serviceProvider);

            var empresaOutResponse = await service.ExecuteAsync(_getEmpresaRequest);

            if (empresaOutResponse.Data != null)
            {
                var emrpresaResponse = (GrupoResponse)empresaOutResponse.Data;
                return emrpresaResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
