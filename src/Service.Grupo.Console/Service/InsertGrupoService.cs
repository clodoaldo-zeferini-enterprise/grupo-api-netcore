using Microsoft.Extensions.DependencyInjection;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.UseCases.Empresa;

namespace Service.Grupo.Console.Service
{
    internal class InsertGrupoService : IDisposable
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

        ~InsertGrupoService()
        {
            Dispose(false);
        }
        #endregion

        private readonly IServiceProvider _serviceProvider;
        private readonly InsertGrupoRequest _insertEmpresaRequest;

        private InsertGrupoService() { }

        public InsertGrupoService(IServiceProvider serviceProvider, InsertGrupoRequest insertEmpresaRequest)
        {
            _serviceProvider = serviceProvider;
            _insertEmpresaRequest = insertEmpresaRequest;
        }

        public async Task<GrupoResponse?> ExecuteAsync()
        {

            var service = ActivatorUtilities.CreateInstance<InsertGrupoUseCaseAsync>(_serviceProvider);

            var empresaOutResponse = await service.ExecuteAsync(_insertEmpresaRequest);

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
