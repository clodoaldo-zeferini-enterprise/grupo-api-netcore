using System;

namespace Service.Grupo.Application.Models.Response
{
    public class Mensagem
    {
        public DateTime DataHora { get; private set; }
        public string Texto { get; private set; }

        public Mensagem() { }

        public Mensagem(string texto)
        {
            Texto = texto;
            DataHora = DateTime.Now;
        }
    }
}
