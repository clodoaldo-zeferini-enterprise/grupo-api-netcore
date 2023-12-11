using Service.Grupo.Application.Models.Response.Errors;
using System;
using System.Collections.Generic;

namespace Service.Grupo.Application.Models.Response
{
    public class ResponseBase : IDisposable
    {
        public bool Resultado { get; private set; }             
        public string Mensagem { get; private set; }
        public List<Mensagem> Mensagens { get; private set; }
        public List<MyException> Exceptions { get; private set; }
        public object Data { get; private set; }

        public string Request { get; private set; }
        
        public Errors.ErrorsResponse ErrorsResponse { get; private set; }

        public ResponseBase()
        {             
            Mensagens = new List<Mensagem>();
            Exceptions = new List<MyException>(); 
            ErrorsResponse = new Errors.ErrorsResponse();
            Resultado = false;
        } 

        public void AddMensagem(string mensagem)
        {
            Mensagens.Add(new Mensagem(mensagem));
        }

        public void AddExceptions(Exception exception)
        {
            Exceptions.Add(new MyException(exception));
        }

        public void SetResultado(bool resultado)
        {
            Resultado = resultado;
        }

        public void SetData(object data)
        {
            Data = data;
        }

        public void SetErrorsResponse(Errors.ErrorsResponse errorsResponse)
        {
            ErrorsResponse = errorsResponse;
        }

        public void SetRequest(string request)
        {
            Request = request;
        }

        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        ~ResponseBase()
        {
            Dispose(false);
        }
        #endregion
    }
}
