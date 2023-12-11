using System.Collections.Generic;

namespace Service.Grupo.Application.Models.Response
{
    public class GrupoResponse
    {
        public List<Navigator> Navigators { get; private set; }
        public List<Grupo> Empresas { get; private set; }

        private GrupoResponse()
        {
        }

        public GrupoResponse(Grupo empresa)
        {
            Navigators = new List<Navigator>();
            Empresas = new List<Grupo> { empresa };

            Navigator navigator= new Navigator(1,1,1,1);
            Navigators.Add(navigator);
        }

        public GrupoResponse(List<Navigator> navigators, List<Grupo> empresas)
        {
            Navigators = navigators;
            Empresas = empresas;
        }
    }
}
