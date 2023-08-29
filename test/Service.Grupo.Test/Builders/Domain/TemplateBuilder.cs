using Service.Grupo.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;

namespace Service.Grupo.Test.Builders.Domain
{
    public class GrupoBuilder
    {
        private Guid Id { get; set; }
        private EStatus Status { get; set; }
        private DateTime? DataInsert { get; set; }
        private DateTime? DataUpdate { get; set; }
        private Guid SysUsuSessionId { get; set; }
        private string Nome { get; set; }

        public static GrupoBuilder Novo()
        {
            var eStatus = new EStatus[] { EStatus.ATIVO, EStatus.INATIVO, EStatus.EXCLUIDO };
            var GrupoBuilderFaker = new Faker<GrupoBuilder>("pt_BR")
                .RuleFor(u => u.Status, f => f.PickRandom(eStatus));

            var GruposBuilders = GrupoBuilderFaker.Generate(1);

            return GruposBuilders[0];
        }

        public GrupoBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public GrupoBuilder ComStatus(EStatus eStatus)
        {
            Status = eStatus;

            return this;
        }

        public GrupoBuilder ComId(Guid id)
        {
            Id = id;
            return this;
        }

        public GrupoBuilder ComSysUsuSessionId(Guid sysUsuSessionId)
        {
            SysUsuSessionId = sysUsuSessionId;
            return this;
        }

        public Service.Grupo.Domain.Entities.Grupo Build()
        {
            var Grupo = new Service.Grupo.Domain.Entities.Grupo(Id, Nome, Status, SysUsuSessionId);

            return Grupo;
        }
    }
}
