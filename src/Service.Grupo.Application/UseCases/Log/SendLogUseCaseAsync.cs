
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
            _logResponse = null;

        }

        ~SendLogUseCaseAsync()
        {
            Dispose(false);
        }
        #endregion

        private IConfiguration _configuration;
        private LogOutResponse output;
        private LogResponse _logResponse;

        public SendLogUseCaseAsync(
            IConfiguration configuration)
        {
            _configuration = configuration;

            output = new();
        }

        public async Task<LogOutResponse> ExecuteAsync(LogRequest request)
        {
            try
            {
                _logResponse = new LogResponse(true);
                output.SetResultado(true);
                output.AddMensagem("Dado recuperado com Sucesso!");
                output.SetData(_logResponse);

                return output;
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
            }

            return output;
        }
    }
}
