
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Service.Grupo.Application.Interfaces;
using Service.Grupo.Application.Models.Response;
using Service.Grupo.Application.Models.Request.Log;
using Service.Grupo.Application.Models.Response.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Service.Grupo.Application.UseCases.Log
{
    public class SendLogUseCaseAsync : IUseCaseAsync<LogRequest, LogOutResponse>, IDisposable
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

        ~SendLogUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private LogOutResponse _output;
        private LogResponse _logResponse;

        public SendLogUseCaseAsync(
            IConfiguration configuration)
        {
            _configuration = configuration;

            _output = new()
            {
                Resultado = false,
                Mensagem = "Dados Fornecidos são inválidos!"
            };
        }

        public async Task<LogOutResponse> ExecuteAsync(LogRequest request)
        {
            try
            {
                _logResponse = new LogResponse(true);
                _output.Resultado = true;
                _output.Mensagem = "Dado recuperado com Sucesso!";
                _output.Data = _logResponse;

                return _output;
            }
            catch (Exception ex)
            {
                _output.Mensagem = "Ocorreram Exceções durante a execução";
                _output.AddExceptions(ex);
                Service.Grupo.Application.Models.Response.Errors.ErrorResponse errorResponse = new Service.Grupo.Application.Models.Response.Errors.ErrorResponse("id", "parameter", JsonConvert.SerializeObject(ex, Formatting.Indented));
                List<Service.Grupo.Application.Models.Response.Errors.ErrorResponse> errorResponses = new List<Service.Grupo.Application.Models.Response.Errors.ErrorResponse>();
                errorResponses.Add(errorResponse);
                _output.ErrorsResponse = new Service.Grupo.Application.Models.Response.Errors.ErrorsResponse(errorResponses);
            }
            finally
            {
                _output.Request = JsonConvert.SerializeObject(request, Formatting.Indented);
            }

            return _output;
        }
    }
}
