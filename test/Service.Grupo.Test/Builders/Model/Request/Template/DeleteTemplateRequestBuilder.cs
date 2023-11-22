using System.Runtime.CompilerServices;
using Bogus;
using Bogus.DataSets;
using Service.Grupo.Domain.Enum;

namespace Service.Grupo.Test.Builders.Model.Request.Grupo
{
    public class DeleteGrupoRequestBuilder
    {
        private Guid Id { get; set; }
        private Guid SysUsuSessionId { get; set; }

        public static DeleteGrupoRequestBuilder Novo()
        {
            return new DeleteGrupoRequestBuilder();
        }
        
        public DeleteGrupoRequestBuilder ComId(string id)
        {
            SysUsuSessionId = Guid.NewGuid();

            if (Guid.TryParse(id, out var newGuid))
            {
                Id = newGuid;
                return this;
            }

            return null;
        }

        public DeleteGrupoRequestBuilder ComSysUsuSessionId(string sysUsuSessionId)
        {
            Id = Guid.NewGuid();

            if (Guid.TryParse(sysUsuSessionId, out var newGuid))
            {
                SysUsuSessionId = newGuid;
                return this;
            }

            return null;
        }

        public Application.Models.Request.Grupo.DeleteGrupoRequest Build()
        {
            var Grupo = new Application.Models.Request.Grupo.DeleteGrupoRequest(Id, SysUsuSessionId);
            return Grupo;
        }
    }
}
