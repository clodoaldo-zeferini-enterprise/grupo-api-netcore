using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Request.STS;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.STS;
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
            _output = null;

        }

        ~GetGetAuthorizationUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private AuthorizationOutResponse _output;
        private AuthorizationResponse _authorizationResponse;

        public GetGetAuthorizationUseCaseAsync(
            IConfiguration configuration )
        {
            _configuration = configuration;           

            _output = new()
            {
                Resultado = false,
                Mensagem = "Dados Fornecidos são inválidos!"
            };
        }

        public async Task<AuthorizationOutResponse> ExecuteAsync(AuthorizationRequest request)
        {
            try
            {
                _authorizationResponse = new AuthorizationResponse(true);
                _output.Resultado = true;
                _output.Mensagem = "Dado recuperado com Sucesso!";
                _output.Data = _authorizationResponse;

                return _output;
            }            
            catch (Exception ex)
            {
                _output.Mensagem = "Ocorreram Exceções durante a execução";
                _output.AddExceptions(ex);
                Grupo.Application.Models.Response.Errors.ErrorResponse errorResponse = new Grupo.Application.Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                System.Collections.Generic.List<Grupo.Application.Models.Response.Errors.ErrorResponse> errorResponses = new List<Grupo.Application.Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                _output.ErrorsResponse = new Grupo.Application.Models.Response.Errors.ErrorsResponse(errorResponses);
            }
            finally
            {
                _output.Request = JsonConvert.SerializeObject(request, Formatting.Indented);
            }

            return _output;
        }
    }
}
