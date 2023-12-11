using Bogus;
using Service.Grupo.Application.Models.Enum;
using Service.Grupo.Application.Models.Request.Grupo;

namespace Service.Grupo.Application.Test.Builders
{
    public class UpdateGrupoRequestBuilder : Service.Grupo.Application.Base.Base
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string NomeDoGrupo { get; private set; }

        private UpdateGrupoRequest updateEmpresaRequest;

        public static UpdateGrupoRequestBuilder Novo()
        {
            var eStatus = new Application.Models.Enum.EStatus[] { Application.Models.Enum.EStatus.ATIVO, Application.Models.Enum.EStatus.INATIVO, Application.Models.Enum.EStatus.EXCLUIDO };
            var insertEmpresaBuilderFaker = new Faker<UpdateGrupoRequestBuilder>("pt_BR")                
                .RuleFor(u => u.SysUsuSessionId, f => Guid.NewGuid())
                .RuleFor(u => u.RequestId, f => Guid.NewGuid())
                .RuleFor(u => u.GrupoId, f => Guid.NewGuid())
                .RuleFor(u => u.EmpresaId, f => Guid.NewGuid())
                .RuleFor(u => u.Status, f => f.PickRandom(eStatus))
                .RuleFor(u => u.NomeDoGrupo, f => f.Company.CompanyName())
                ;

            var insertEmpresaBuilders = insertEmpresaBuilderFaker.Generate(1);

            return insertEmpresaBuilders[0];
        }        

        public UpdateGrupoRequestBuilder ComSysUsuSessionId(Guid sysUsuSessionId)
        {
            this.SysUsuSessionId = sysUsuSessionId;
            return this;
        }

        public UpdateGrupoRequestBuilder ComRequestId(Guid requestId)
        {
            this.RequestId = requestId;
            return this;
        }

        public UpdateGrupoRequestBuilder ComGrupoId(Guid grupoId)
        {
            this.GrupoId = grupoId;
            return this;
        }

        public UpdateGrupoRequestBuilder ComStatus(EStatus status)
        {
            Status = status;
            return this;
        }


        public UpdateGrupoRequestBuilder ComNomeDoGrupo(string nomeDoGrupo)
        {
            NomeDoGrupo = nomeDoGrupo;
            return this;
        }


        public Service.Grupo.Application.Models.Request.Grupo.UpdateGrupoRequest Build()
        {
            updateEmpresaRequest = new UpdateGrupoRequest(SysUsuSessionId, RequestId, GrupoId, EmpresaId, Status, NomeDoGrupo);

            return updateEmpresaRequest;
        }
    }
}
