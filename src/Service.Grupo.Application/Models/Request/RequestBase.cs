using Service.Grupo.Application.Base;
using System;

namespace Service.Grupo.Application.Models.Request
{
    public class RequestBase
    {
        public Application.Models.Enum.EStatus Status { get; protected set; } = Application.Models.Enum.EStatus.ATIVO;
        public Guid SysUsuSessionId { get; protected set; }
        public Guid RequestId { get; protected set; } = Guid.NewGuid();
        public bool RetornaLista { get; protected set; } = false;
    }
}
