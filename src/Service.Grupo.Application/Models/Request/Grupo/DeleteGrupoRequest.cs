using System;

namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class DeleteGrupoRequest : RequestBase
    {
        public Guid Id { get; set; }
        public GetGrupoRequest GetGrupoRequest { get; set; }

        private DeleteGrupoRequest()
        {
        }

        private void Validate()
        {
            var IsIdValido = Guid.TryParse(Id.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            Base.ValidadorDeRegra.Novo()
                .Quando(!IsIdValido, Base.Resource.IdInvalido)
                .Quando(!IsSysUsuSessionIdValido, Base.Resource.SysUsuSessionIdInvalido)
                .DispararExcecaoSeExistir();
        }


        public DeleteGrupoRequest(Guid id, Guid sysUsuSessionId, GetGrupoRequest getGrupoRequest)
        {
            Id = id;
            SysUsuSessionId = sysUsuSessionId;

            Validate();
            GetGrupoRequest = getGrupoRequest;
        }
    }
}
