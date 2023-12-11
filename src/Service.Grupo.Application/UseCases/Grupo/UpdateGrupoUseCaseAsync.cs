
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Grupo;
using Service.Grupo.Application.Models.Request.Log;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.STS;
using Service.Grupo.Repository.Interfaces.Repositories.DB;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Service.Grupo.Application.UseCases.Empresa
{
    public class UpdateGrupoUseCaseAsync : IUseCaseAsync<UpdateGrupoRequest,  EmpresaOutResponse>, IDisposable
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

            empresaResponse = null;
            authorizationOutResponse = null;
            authorizationResponse = null;
            empresaToUpdate = null;
        }

        ~UpdateGrupoUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private IGrupoRepository _empresaRepository;
        private IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> _getGetAuthorizationUseCaseAsync;
        private IUseCaseAsync<GetGrupoRequest,  EmpresaOutResponse> _getGrupoUseCaseAsync;
        private IUseCaseAsync<LogRequest, LogOutResponse> _sendLogUseCaseAsync;

        private EmpresaOutResponse output;
        private GrupoResponse empresaResponse;
        private AuthorizationOutResponse authorizationOutResponse;
        private AuthorizationResponse authorizationResponse;
        private Domain.Entities.Grupo empresaToUpdate;

        public UpdateGrupoUseCaseAsync(
              IConfiguration configuration
            , IGrupoRepository empresaRepository
            , IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse> getGetAuthorizationUseCaseAsync
            , IUseCaseAsync<GetGrupoRequest,  EmpresaOutResponse> getGrupoUseCaseAsync
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

        public async Task<EmpresaOutResponse> ExecuteAsync(UpdateGrupoRequest request)
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

                var empresaFromDB = await _empresaRepository.GetById(request.GrupoId);

                empresaToUpdate = new Domain.Entities.Grupo(request.SysUsuSessionId, empresaFromDB.GrupoId, empresaFromDB.EmpresaId, request.NomeDoGrupo, (Service.Grupo.Domain.Enum.EStatus)request.Status, empresaFromDB.DataInsert.Value);

                if (await _empresaRepository.Update(empresaToUpdate))
                {
                    output.AddMensagem("Registro Alterado com Sucesso!");
                    output.SetData(empresaToUpdate);
                    output.SetResultado(true);
                }
            }
            catch (Exception ex)
            {
                Models.Response.Errors.ErrorResponse errorResponse =
                    new("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Models.Response.Errors.ErrorResponse> errorResponses = new()
                {
                    errorResponse
                };
                output.SetErrorsResponse(new Models.Response.Errors.ErrorsResponse(errorResponses));

                output.AddExceptions(ex);
                output.AddMensagem("Ocorreu uma falha ao Atualizar o Registro!");
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
