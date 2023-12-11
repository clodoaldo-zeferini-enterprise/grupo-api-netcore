using System;

namespace Service.Grupo.Application.Models.Request.STS
{
    public class AuthorizationRequest
    {
        public Guid SysUsuSessionId { get; private set; }

        private AuthorizationRequest()
        {
        }
        public AuthorizationRequest(Guid sysUsuSessionId)
        {
            SysUsuSessionId = sysUsuSessionId;
        }
    }
}
