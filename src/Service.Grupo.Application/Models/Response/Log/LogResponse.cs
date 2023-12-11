using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Grupo.Application.Models.Response.Log
{
    public class LogResponse
    {
        public bool IsValid { get; private set; }

        private LogResponse() { }

        public LogResponse(bool isValid)
        {
            IsValid = IsValid;

        }
    }
}
