using Service.Grupo.Application.Base;
using System;

namespace Service.Grupo.Application.Models.Request
{
    public class RequestBase
    {
        public Guid SysUsuSessionId { get; set; }
        public Guid RequestId { get; set; } = Guid.NewGuid();
        public bool RetornaLista { get; set; } = false;
    }
}
