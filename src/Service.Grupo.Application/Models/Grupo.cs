using Service.Grupo.Application.Base;
using System;

namespace Service.Grupo.Application.Models
{
    public class Grupo : Base.Base 
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string NomeDoGrupo { get; private set; }

        private void Validate()
        {
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid isSysUsuSessionIdValido);
            var IsRequestIdValido = Guid.TryParse(RequestId.ToString(), out Guid isRequestIdValido);
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid isGrupoIdValido);

            Base.ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Base.Resource.SysUsuSessionIdInvalido)
                .Quando(!IsRequestIdValido, Base.Resource.RequestIdInvalido)
                .Quando(!IsGrupoIdValido, Base.Resource.GrupoIdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeDoGrupoInvalido)
                .DispararExcecaoSeExistir();
        }

        private Grupo()
        {
        }

        public Grupo(Guid sysUsuSessionId, Guid requestId, Guid grupoId, Guid empresaId, string nomeDoGrupo, Application.Models.Enum.EStatus status, DateTime? dataInsert, DateTime? dataUpdate)
        {
            SysUsuSessionId = sysUsuSessionId;
            RequestId = requestId;
            GrupoId = grupoId;
            EmpresaId = empresaId;
            NomeDoGrupo = nomeDoGrupo;
            Status = status;
            DataInsert = dataInsert;
            DataUpdate = dataUpdate;

            Validate();
        }
    }
}
