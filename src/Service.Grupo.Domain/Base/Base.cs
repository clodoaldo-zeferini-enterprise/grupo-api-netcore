using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Domain.Base
{
    public class Base
    {
        public EStatus Status { get; set; }
        public DateTime? DataInsert { get; set; }
        public DateTime? DataUpdate { get; set; }
        public Guid SysUsuSessionId { get; set; }
    }
}
