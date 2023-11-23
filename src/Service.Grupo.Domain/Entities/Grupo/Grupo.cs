
using Service.Grupo.Domain.Base;
using Service.Grupo.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Grupo.Domain.Entities
{
    [Table("Grupo")]
    public class Grupo : Base.Base
    {
        public Guid GrupoId { get; set; }   
        public string NomeDoGrupo { get; set; }

        private void Validate()
        {
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsGrupoIdValido, Resource.IdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeInvalido)
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .DispararExcecaoSeExistir();
        }

        public Grupo() { }

        public Grupo(Guid grupoId, string nomeDoGrupo, EStatus status, Guid sysUsuSessionId)
        {
            GrupoId = grupoId;
            NomeDoGrupo = nomeDoGrupo;
            Status = status;
            SysUsuSessionId = sysUsuSessionId;

            Validate();
        }

        public Grupo(Guid grupoId)
        {
            var IsIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            
            ValidadorDeRegra.Novo().Quando(!IsIdValido, Resource.IdInvalido).DispararExcecaoSeExistir();

            GrupoId = grupoId;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando((string.IsNullOrEmpty(nome) || nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

            NomeDoGrupo = nome;
        }
    }
}
