using Service.Grupo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Service.Grupo.Application.Models.Request.Grupo;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace Service.Grupo.Application.Test.Builders
{
    public class DeleteEmpresaRequestBuilder : Service.Grupo.Application.Base.Base
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }


        private DeleteGrupoRequest deleteEmpresaRequest;

        public static DeleteEmpresaRequestBuilder Novo()
        {
            var deleteEmpresaBuilderFaker = new Faker<DeleteEmpresaRequestBuilder>("pt_BR")
                .RuleFor(u => u.SysUsuSessionId, f => Guid.NewGuid())
                .RuleFor(u => u.RequestId, f => Guid.NewGuid())
                .RuleFor(u => u.GrupoId, f => Guid.NewGuid())
                .RuleFor(u => u.EmpresaId, f => Guid.NewGuid())
                ;

            var deleteEmpresaBuilders = deleteEmpresaBuilderFaker.Generate(1);

            return deleteEmpresaBuilders[0];
        }

        public DeleteEmpresaRequestBuilder ComSysUsuSessionId(Guid sysUsuSessionId)
        {
            this.SysUsuSessionId = sysUsuSessionId;
            return this;
        }

        public DeleteEmpresaRequestBuilder ComRequestId(Guid requestId)
        {
            this.RequestId = requestId;
            return this;
        }

        public DeleteEmpresaRequestBuilder ComGrupoId(Guid grupoId)
        {
            this.GrupoId = grupoId;
            return this;
        }

        public DeleteEmpresaRequestBuilder ComEmpresaId(Guid empresaId)
        {
            this.EmpresaId = empresaId;
            return this;
        }

        public Service.Grupo.Application.Models.Request.Grupo.DeleteGrupoRequest Build()
        {
            deleteEmpresaRequest = new DeleteGrupoRequest(SysUsuSessionId, RequestId, GrupoId, EmpresaId);

            return deleteEmpresaRequest;
        }
    }
}
