using System;

namespace Service.Grupo.Domain.Base
{
    public class DapperQuery
    {
        public Guid SysUsuSessionId { get; private set; }
        public string Query { get; private set; }

        public DapperQuery()
        {
        }

        public DapperQuery(Guid sysUsuSessionId, string query)
        {
            SysUsuSessionId = sysUsuSessionId;
            Query = query;
        }
    }
}
