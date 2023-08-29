using Microsoft.Extensions.DependencyInjection;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.UseCases.Grupo;

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
        private readonly GetGrupoRequest _getGrupoRequest;

        private GetGrupoService() { }

        public GetGrupoService(IServiceProvider serviceProvider, GetGrupoRequest getGrupoRequest)
        {
            _serviceProvider = serviceProvider;
            _getGrupoRequest = getGrupoRequest;
        }

        public async Task<GrupoResponse?> ExecuteAsync()
        {
            var service = ActivatorUtilities.CreateInstance<GetGrupoUseCaseAsync>(_serviceProvider);

            var GrupoOutResponse = await service.ExecuteAsync(_getGrupoRequest);

            if (GrupoOutResponse.Data != null)
            {
                var GrupoResponse = (GrupoResponse)GrupoOutResponse.Data;
                return GrupoResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
