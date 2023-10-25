using System;

namespace Service.Grupo.Application.Models.Request.Log
{
    public class LogRequest
    {
        public Guid SysUsuSessionId { get; set; }

        private LogRequest()
        {
        }
        public LogRequest(Guid sysUsuSessionId)
        {
            SysUsuSessionId = sysUsuSessionId;
        }
    }
}
