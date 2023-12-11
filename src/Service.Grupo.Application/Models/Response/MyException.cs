using System;

namespace Service.Grupo.Application.Models.Response
{
    public class MyException
    {
        public DateTime DataHora { get; private set; }
        public Exception Exception { get; private set; }

        public MyException() { }

        public MyException(Exception exception)
        {
            Exception = exception;
            DataHora = DateTime.Now;
        }
    }
}
