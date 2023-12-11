using Service.Grupo.Application.Base;
using System;


namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class InsertGrupoRequest : RequestBase
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public string NomeDoGrupo { get; private set; }        

        private void Validate()
        {
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid isSysUsuSessionIdValido);
            var IsRequestIdValido = Guid.TryParse(RequestId.ToString(), out Guid isRequestIdValido);
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid isGrupoIdValido);
            var IsEmpresaIdValido = Guid.TryParse(EmpresaId.ToString(), out Guid isEmpresaIdValido);

            Base.ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Base.Resource.SysUsuSessionIdInvalido)
                .Quando(!IsRequestIdValido, Base.Resource.RequestIdInvalido)
                .Quando(!IsGrupoIdValido, Base.Resource.GrupoIdInvalido)
                .Quando((string.IsNullOrEmpty(NomeDoGrupo) || NomeDoGrupo.Length > 100), Resource.NomeDoGrupoInvalido)
                .DispararExcecaoSeExistir();
        }

        private InsertGrupoRequest()
        {
        }

        public InsertGrupoRequest(Guid sysUsuSessionId, Guid requestId, Guid grupoId, string nomeDoGrupo)
        {
            SysUsuSessionId = sysUsuSessionId;
            RequestId = requestId;
            GrupoId = grupoId;

            NomeDoGrupo = nomeDoGrupo;
         
            Validate();
        }
    }
}
