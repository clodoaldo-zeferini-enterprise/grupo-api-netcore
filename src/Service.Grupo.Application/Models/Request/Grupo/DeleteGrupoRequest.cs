using System;

namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class DeleteGrupoRequest : RequestBase
    {
        public Guid GrupoId { get; set; }

        private DeleteGrupoRequest() {}

        private void Validate()
        {
            var IsGrupoIdValido = Guid.TryParse(GrupoId.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            Base.ValidadorDeRegra.Novo()
                .Quando(!IsGrupoIdValido, Base.Resource.IdInvalido)
                .Quando(!IsSysUsuSessionIdValido, Base.Resource.SysUsuSessionIdInvalido)
                .DispararExcecaoSeExistir();
        }


        public DeleteGrupoRequest(Guid grupoId, Guid sysUsuSessionId, bool retornaLista = false)
        {
            GrupoId = grupoId;
            SysUsuSessionId = sysUsuSessionId;
            RetornaLista = retornaLista;

            Validate();
        }
    }
}
