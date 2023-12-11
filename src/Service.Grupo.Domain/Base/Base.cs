using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Domain.Base
{
    public class Base
    {
        public EStatus Status { get; protected set; }
        public DateTime? DataInsert { get; protected set; }
        public DateTime? DataUpdate { get; protected set; }
        public Guid SysUsuSessionId { get; protected set; }
    }
}
