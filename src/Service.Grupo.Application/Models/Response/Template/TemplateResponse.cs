using System.Collections.Generic;

namespace Service.Grupo.Application.Models.Response
{
    public class GrupoResponse
    {
        public List<Navigator> Navigators { get; set; }
        public List<Grupo> Grupos { get; set; }

        public GrupoResponse()
        {
            Navigators = new List<Navigator>();
            Grupos = new List<Grupo>();
        }

        public GrupoResponse(List<Navigator> navigators, List<Grupo> Grupos)
        {
            Navigators = navigators;
            Grupos = Grupos;
        }
    }
}
