using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Grupo.Application.Models.Response.Errors
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public string Parameter { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(string id, string parameter)
        {
            Id = id;
            Parameter = parameter;
        }

        public ErrorResponse(string id, string parameter, string message) : this(id, parameter)
        {
            Message = message;
        }
    }
}
