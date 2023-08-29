using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Grupo.Application.Base
{
    public class ValidadorDeRegra
    {
        private readonly List<string> _mensagensDeErros;

        private ValidadorDeRegra()
        {
            _mensagensDeErros = new List<string>();
        }

        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _mensagensDeErros.Add(mensagemDeErro);

            return this;
        }

        public void DispararExcecaoSeExistir()
        {
            if (_mensagensDeErros.Any())
                throw new ExcecaoDeModelo(_mensagensDeErros);
        }
    }

    public class ExcecaoDeModelo : ArgumentException
    {
        public List<string> MensagensDeErro { get; set; }

        public ExcecaoDeModelo(List<string> mensagensDeErros)
        {
            MensagensDeErro = mensagensDeErros;
        }
    }
}
