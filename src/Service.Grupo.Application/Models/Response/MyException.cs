using System;

namespace Service.Grupo.Application.Models.Response
{
    public class MyException
    {
        public DateTime DataHora { get; set; }
        public Exception Exception { get; set; }

        public MyException() { }

        public MyException(Exception exception)
        {
            Exception = exception;
            DataHora = DateTime.Now;
        }
    }
}
