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
                .Quando((NomeDoGrupo == null || NomeDoGrupo.Length < 5 || NomeDoGrupo.Length > 100), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
        }
       
        public string NomeDoGrupo { get; set; }

        private InsertGrupoRequest()
        {
        }

        public InsertGrupoRequest(string nomeDoGrupo)
        {
            NomeDoGrupo = nomeDoGrupo;

            Validate();
            
        }
    }
}
