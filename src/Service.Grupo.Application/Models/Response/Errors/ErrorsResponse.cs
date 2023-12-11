using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Grupo.Application.Models.Response.Errors
{
    public class ErrorsResponse
    {
        public List<ErrorResponse> Errors { get; private set; }

        public ErrorsResponse()
        {
            Errors = new List<ErrorResponse>();
        }

        public ErrorsResponse(List<ErrorResponse> errors)
        {
            Errors = errors;
        }
    }
}
