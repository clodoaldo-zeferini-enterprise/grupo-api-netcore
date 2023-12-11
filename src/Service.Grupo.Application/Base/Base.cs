using System;

namespace Service.Grupo.Application.Base
{
    public class Base
    {        
        public Application.Models.Enum.EStatus Status { get; set; }
        public DateTime? DataInsert { get; set; }
        public DateTime? DataUpdate { get; set; }
        public Guid SysUsuSessionId { get; set; }
        public Guid RequestId { get; set; }
    }
}
