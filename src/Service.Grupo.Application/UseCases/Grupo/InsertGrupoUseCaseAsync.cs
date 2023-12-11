
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.STS;
using Service.Grupo.Repository.Interfaces.Repositories.DB;
using Service.Grupo.Application.Models.Request.Log;
using System;
using System.Threading.Tasks;
using Service.Grupo.Application.Base;

namespace Service.Grupo.Application.UseCases.Empresa
{
    public class InsertGrupoUseCaseAsync : IUseCaseAsync<InsertGrupoRequest,  GrupoOutResponse>, IDisposable
    {
        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            _configuration = null;
            _empresaRepository = null;
            _getGetAuthorizationUseCaseAsync = null;
            _getGrupoUseCaseAsync = null;
            _sendLogUseCaseAsync = null;


            grupoResponse = null;
            authorizationOutResponse = null;
            authorizationResponse = null;
            grupoToInsert = null;
        }

        ~InsertGrupoUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private IGrupoRepository _empresaRepository;
        private IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> _getGetAuthorizationUseCaseAsync;
        private IUseCaseAsync<GetGrupoRequest, GrupoOutResponse> _getGrupoUseCaseAsync;
        private IUseCaseAsync<LogRequest, LogOutResponse> _sendLogUseCaseAsync;

        private GrupoOutResponse output;
        private GrupoResponse         grupoResponse;
        private AuthorizationOutResponse authorizationOutResponse;
        private AuthorizationResponse    authorizationResponse;
        private Domain.Entities.Grupo grupoToInsert;

        public InsertGrupoUseCaseAsync(
              IConfiguration configuration
            , IGrupoRepository empresaRepository
            , IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> getGetAuthorizationUseCaseAsync
            , IUseCaseAsync<GetGrupoRequest, GrupoOutResponse> getGrupoUseCaseAsync
            , IUseCaseAsync<LogRequest, LogOutResponse> sendLogUseCaseAsync
)
        {
            _configuration = configuration;
            _empresaRepository = empresaRepository;
            _getGetAuthorizationUseCaseAsync = getGetAuthorizationUseCaseAsync;
            _getGrupoUseCaseAsync = getGrupoUseCaseAsync;
            _sendLogUseCaseAsync = sendLogUseCaseAsync;

            output = new();
        }

        public async Task<GrupoOutResponse> ExecuteAsync(InsertGrupoRequest request)
        {
            try
            {
                authorizationOutResponse = await _getGetAuthorizationUseCaseAsync.ExecuteAsync(new AuthorizationRequest(request.SysUsuSessionId));

                if (!authorizationOutResponse.Resultado)
                {
                    output.SetResultado(false);
                    output.AddMensagem("Ocorreu uma falha na Autorização!");
                    output.SetData(authorizationOutResponse.Data);

                    return output;
                }

                grupoToInsert = new Domain.Entities.Grupo(request.SysUsuSessionId, request.GrupoId, request.NomeDoGrupo);

                if (await _empresaRepository.Insert(grupoToInsert))
                {
                    output.AddMensagem("Registro inserido com Sucesso!");
                    output.SetData(grupoToInsert);
                    output.SetResultado(true);
                }
            }
            catch (Exception ex)
            {
                Models.Response.Errors.ErrorResponse errorResponse = new("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Models.Response.Errors.ErrorResponse> errorResponses = new()
                {
                    errorResponse
                };
                output.SetErrorsResponse(new Models.Response.Errors.ErrorsResponse(errorResponses));

                output.AddExceptions(ex);
                output.AddMensagem("Ocorreu uma falha ao Inserir o Registro!");
            }
            finally
            {
                output.SetRequest(JsonConvert.SerializeObject(request, Formatting.Indented));
                _sendLogUseCaseAsync.ExecuteAsync(new LogRequest(request.SysUsuSessionId));
            }

            return output;
        }
    }
}
