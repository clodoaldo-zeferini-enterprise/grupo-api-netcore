using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.Log;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.STS;
using Service.Grupo.Application.UseCases.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Service.GetAuthorization.Application.UseCases.GetAuthorization
{
    public class GetGetAuthorizationUseCaseAsync : IUseCaseAsync<AuthorizationRequest, AuthorizationOutResponse>, IDisposable
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
            _authorizationResponse = null;

        }

        ~GetGetAuthorizationUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private AuthorizationOutResponse output;
        private IUseCaseAsync<LogRequest, LogOutResponse> _sendLogUseCaseAsync;
        private AuthorizationResponse _authorizationResponse;

        public GetGetAuthorizationUseCaseAsync(
            IConfiguration configuration
            , IUseCaseAsync<LogRequest, LogOutResponse> sendLogUseCaseAsync)
        {
            _configuration = configuration;
            _sendLogUseCaseAsync = sendLogUseCaseAsync; 
            output = new();
        }

        public async Task<AuthorizationOutResponse> ExecuteAsync(AuthorizationRequest request)
        {
            try
            {
                _authorizationResponse = new AuthorizationResponse(true);
                output.SetResultado(true);
                output.AddMensagem("Dado recuperado com Sucesso!");
                output.SetData(_authorizationResponse);

                return output;
            }            
            catch (Exception ex)
            {
                output.AddMensagem("Ocorreram Exceções durante a execução");
                output.AddExceptions(ex);
                Grupo.Application.Models.Response.Errors.ErrorResponse errorResponse = new Grupo.Application.Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Grupo.Application.Models.Response.Errors.ErrorResponse> errorResponses = new List<Grupo.Application.Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                output.SetErrorsResponse(new Grupo.Application.Models.Response.Errors.ErrorsResponse(errorResponses));
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
