using System;

namespace Service.Grupo.Application.Models.Response
{
    public class Mensagem
    {
        public DateTime DataHora { get; set; }
        public string Texto { get; set; }

        public Mensagem() { }

        public Mensagem(string texto)
        {
            Texto = texto;
            DataHora = DateTime.Now;
        }
    }
}
