
using Service.Grupo.Domain.Base;
using Service.Grupo.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Grupo.Domain.Entities
{
    [Table("Empresa")]
    public class Grupo : Base.Base
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }   
        public string NomeDoGrupo { get; private set; }

        private void Validate()
        {
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid grupoIdValido);    
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando(!IsGrupoIdValido, Resource.GrupoIdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeDoGrupoInvalido)
                .DispararExcecaoSeExistir();
        }

        private Grupo() { }

        public Grupo(Guid sysUsuSessionId, Guid grupoId, string nomeDoGrupo)
        {
            SysUsuSessionId = sysUsuSessionId;
            GrupoId = grupoId;
            EmpresaId = Guid.NewGuid();
            Status = EStatus.ATIVO;
            DataInsert = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            NomeDoGrupo = nomeDoGrupo;

            Validate();
        }
        public Grupo(Guid sysUsuSessionId, Guid grupoId, Guid empresaId, string nomeDoGrupo, EStatus status, DateTime dataInsert)
        {
            SysUsuSessionId = sysUsuSessionId;
            GrupoId = grupoId;
            EmpresaId = empresaId;
            Status = EStatus.ATIVO;
            DataInsert = DateTime.Parse(dataInsert.ToString("yyyy-MM-dd HH:mm:ss"));
            DataUpdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            NomeDoGrupo = nomeDoGrupo;

            Validate();
        }

    }
}
