using Service.Grupo.Application.Base;
using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Application.Models
{
    public class Grupo : Base.Base 
    {
        public Guid GrupoId { get; set; }
        public string NomeDoGrupo { get; set; }

        private void Validate()
        {
            var IsIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }
        private Grupo()
        {
        }

        public Grupo(Guid grupoId)
        {
            GrupoId = grupoId;

            var IsIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .DispararExcecaoSeExistir();

        }

        public Grupo(Guid grupoId, string nomeDoGrupo, EStatus status, Guid sysUsuSessionId,  DateTime? dataInsert, DateTime? dataUpdate)
        {
            GrupoId = grupoId;
            NomeDoGrupo = nomeDoGrupo;
            Status = status;
            DataInsert = dataInsert;
            DataUpdate = dataUpdate;
            SysUsuSessionId = sysUsuSessionId;

            var IsIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(sysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Resource.IdInvalido)
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

        }
    }
}
