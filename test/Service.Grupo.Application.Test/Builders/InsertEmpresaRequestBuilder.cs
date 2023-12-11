using Bogus;
using Service.Grupo.Application.Models.Request.Grupo;

namespace Service.Grupo.Application.Test.Builders
{
    public class InsertGrupoRequestBuilder : Service.Grupo.Application.Base.Base
    {        
        public Guid GrupoId { get; private set; }        
        public Guid EmpresaId { get; private set; }
        public string NomeDoGrupo { get; private set; }


        private InsertGrupoRequest insertEmpresaRequest;

        public static InsertGrupoRequestBuilder Novo()
        {
            var insertEmpresaBuilderFaker = new Faker<InsertGrupoRequestBuilder>("pt_BR")                
                .RuleFor(u => u.SysUsuSessionId, f => Guid.NewGuid())
                .RuleFor(u => u.RequestId, f => Guid.NewGuid())
                .RuleFor(u => u.GrupoId, f => Guid.NewGuid())
                .RuleFor(u => u.NomeDoGrupo, f => f.Company.CompanyName())
                ;

            var insertEmpresaBuilders = insertEmpresaBuilderFaker.Generate(1);

            return insertEmpresaBuilders[0];
        }

        public InsertGrupoRequestBuilder ComSysUsuSessionId(Guid sysUsuSessionId)
        {
            this.SysUsuSessionId = sysUsuSessionId;
            return this;
        }

        public InsertGrupoRequestBuilder ComRequestId(Guid requestId)
        {
            this.RequestId = requestId;
            return this;
        }

        public InsertGrupoRequestBuilder ComGrupoId(Guid grupoId)
        {
            this.GrupoId = grupoId;
            return this;
        }

        public InsertGrupoRequestBuilder ComNomeDoGrupo(string nomeDoGrupo)
        {
            NomeDoGrupo = nomeDoGrupo;
            return this;
        }


        public Service.Grupo.Application.Models.Request.Grupo.InsertGrupoRequest Build()
        {
            insertEmpresaRequest = new InsertGrupoRequest(SysUsuSessionId, RequestId, GrupoId, NomeDoGrupo);

            return insertEmpresaRequest;
        }
    }
}
