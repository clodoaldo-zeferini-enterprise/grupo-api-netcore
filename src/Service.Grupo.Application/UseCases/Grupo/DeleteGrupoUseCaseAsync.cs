
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
using System.Collections.Generic;
using Service.Grupo.Application.Base;
using Service.GetAuthorization.Application.UseCases.GetAuthorization;
using Service.Grupo.Domain.Enum;

namespace Service.Grupo.Application.UseCases.Empresa
{
    public class DeleteGrupoUseCaseAsync : IUseCaseAsync<DeleteGrupoRequest, GrupoOutResponse>, IDisposable
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
            grupoToDelete = null;
        }

        ~DeleteGrupoUseCaseAsync()
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
        private GrupoResponse grupoResponse;
        private AuthorizationOutResponse authorizationOutResponse;
        private AuthorizationResponse authorizationResponse;
        private Domain.Entities.Grupo grupoToDelete;

        public DeleteGrupoUseCaseAsync(
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
            output.SetResultado(false);
        }

        public async Task<GrupoOutResponse> ExecuteAsync(DeleteGrupoRequest request)
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

                var grupoDB = await _empresaRepository.GetById(request.RequestId);

                grupoToDelete = new Domain.Entities.Grupo(grupoDB.SysUsuSessionId, grupoDB.GrupoId, grupoDB.EmpresaId, grupoDB.NomeDoGrupo, EStatus.EXCLUIDO, grupoDB.DataInsert.Value);

                output.SetResultado(await _empresaRepository.Update(grupoToDelete));

                output.AddMensagem((output.Resultado ? "Registro Excluído com Sucesso!" : "Ocorreu uma falha ao Excluir o Registro!"));
            }
            catch (Exception ex)
            {
                output.AddMensagem("Ocorreram Exceções durante a execução");
                output.AddExceptions(ex);
                Service.Grupo.Application.Models.Response.Errors.ErrorResponse errorResponse = new Service.Grupo.Application.Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                List<Service.Grupo.Application.Models.Response.Errors.ErrorResponse> errorResponses = new List<Service.Grupo.Application.Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                output.SetErrorsResponse(new Service.Grupo.Application.Models.Response.Errors.ErrorsResponse(errorResponses));
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
