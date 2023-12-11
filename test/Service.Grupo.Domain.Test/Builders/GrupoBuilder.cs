using Bogus;
using Service.Grupo.Domain.Enum;

namespace Service.Grupo.Domain.Test.Builders
{
    public class GrupoBuilder : Service.Grupo.Domain.Base.Base
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string NomeDoGrupo { get; private set; }

        private Domain.Entities.Grupo empresa;

        public static GrupoBuilder Novo()
        {
            var eStatus = new Domain.Enum.EStatus[] { Domain.Enum.EStatus.ATIVO, Domain.Enum.EStatus.INATIVO, Domain.Enum.EStatus.EXCLUIDO };
            var empresaBuilderFaker = new Faker<GrupoBuilder>("pt_BR")                
                .RuleFor(u => u.SysUsuSessionId, f => Guid.NewGuid())
                .RuleFor(u => u.GrupoId, f => Guid.NewGuid())
                .RuleFor(u => u.EmpresaId, f => Guid.NewGuid())
                .RuleFor(u => u.Status, f => f.PickRandom(eStatus))
                .RuleFor(u => u.DataInsert, f => f.Date.Soon())
                .RuleFor(u => u.DataUpdate, f => f.Date.Soon())
                .RuleFor(u => u.NomeDoGrupo, f => f.Company.CompanyName())
                ;

            var empresaBuilders = empresaBuilderFaker.Generate(1);

            return empresaBuilders[0];
        }        

        public GrupoBuilder ComSysUsuSessionId(Guid sysUsuSessionId)
        {
            this.SysUsuSessionId = sysUsuSessionId;
            return this;
        }

        public GrupoBuilder ComGrupoId(Guid grupoId)
        {
            this.GrupoId = grupoId;
            return this;
        }

        public GrupoBuilder ComEmpresaId(Guid empresaId)
        {
            this.EmpresaId = empresaId;
            return this;
        }

        public GrupoBuilder ComStatus(EStatus status)
        {
            Status = status;
            return this;
        }


        public GrupoBuilder ComNomeDoGrupo(string nomeDoGrupo)
        {
            NomeDoGrupo = nomeDoGrupo;
            return this;
        }


        public Service.Grupo.Domain.Entities.Grupo Build()
        {
            empresa = new Service.Grupo.Domain.Entities.Grupo(SysUsuSessionId, GrupoId, EmpresaId, NomeDoGrupo, Status, DataUpdate.Value);

            return empresa;
        }
    }
}
