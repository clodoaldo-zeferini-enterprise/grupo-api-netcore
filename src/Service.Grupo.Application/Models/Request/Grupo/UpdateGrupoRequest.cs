using Service.Grupo.Application.Base;
using Service.Grupo.Domain.Enum;
using System;

namespace Service.Grupo.Application.Models.Request.Grupo.Grupo
{
    public class UpdateGrupoRequest : RequestBase
    {
        private void Validate()
        {
            var IsIdValido = Guid.TryParse(Id.ToString(), out Guid idValido);
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando(!IsIdValido, Resource.IdInvalido)
                .Quando((Nome == null || Nome.Length < 5 || Nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }

        public Guid Id { get; set; }
        public EStatus Status { get; set; }
        public string Nome { get; set; }

        public GetGrupoRequest GetGrupoRequest { get; set; }    

        public UpdateGrupoRequest(Guid id, EStatus status, string nome, GetGrupoRequest getGrupoRequest)
        {
            Id = id;
            Status = status;
            Nome = nome;

            GetGrupoRequest = getGrupoRequest;

            Validate();            
        }
    }
}
