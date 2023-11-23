using Service.Grupo.Application.Base;
using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Application.Models.Request.Grupo.Grupo
{
    public class UpdateGrupoRequest : RequestBase
    {
        private void Validate()
        {
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando(!IsGrupoIdValido, Resource.IdInvalido)
                .Quando((Nome == null || Nome.Length < 5 || Nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }

        public Guid GrupoId { get; set; }
        public EStatus Status { get; set; }
        public string Nome { get; set; }

        public UpdateGrupoRequest(Guid grupoId, EStatus status, string nomeDoGrupo)
        {
            GrupoId = grupoId;
            Status = status;
            Nome = nomeDoGrupo;

            Validate();            
        }
    }
}
