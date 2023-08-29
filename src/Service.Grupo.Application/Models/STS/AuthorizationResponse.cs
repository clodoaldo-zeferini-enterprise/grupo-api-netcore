using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Grupo.Application.Models.STS
{
    public class AuthorizationResponse
    {
        public bool IsValid { get; set; }

        private AuthorizationResponse() { }

        public AuthorizationResponse(bool isValid) {
            IsValid = IsValid;

        }
    }
}
