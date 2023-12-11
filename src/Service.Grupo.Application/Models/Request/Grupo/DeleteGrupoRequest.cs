using System;

namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class DeleteGrupoRequest : RequestBase
    {
        public Guid GrupoId { get; private set; }
        public Guid EmpresaId { get; private set; }
        

        private DeleteGrupoRequest() {}

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
                .DispararExcecaoSeExistir();
        }

        public DeleteGrupoRequest(Guid sysUsuSessionId, Guid requestId, Guid grupoId, Guid empresaId)
        {
            SysUsuSessionId = sysUsuSessionId;
            RequestId = requestId;
            GrupoId = grupoId;
            EmpresaId = empresaId;

            Validate();
        }
    }
}
