using Service.Grupo.Application.Base;
using System;


namespace Service.Grupo.Application.Models.Request.Grupo
{
    public class InsertGrupoRequest : RequestBase
    {
        private void Validate()
        {
            var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid sysUsuSessionIdValido);

            ValidadorDeRegra.Novo()
                .Quando(!IsSysUsuSessionIdValido, Resource.SysUsuSessionIdInvalido)
                .Quando((Nome == null || Nome.Length < 5 || Nome.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }

        public GetGrupoRequest GetGrupoRequest { get; set; }   
        public string Nome { get; set; }

        private InsertGrupoRequest()
        {
        }

        public InsertGrupoRequest(string nome, GetGrupoRequest getGrupoRequest)
        {
            Nome = nome;
            GetGrupoRequest = getGrupoRequest;

            Validate();
            
        }
    }
}
