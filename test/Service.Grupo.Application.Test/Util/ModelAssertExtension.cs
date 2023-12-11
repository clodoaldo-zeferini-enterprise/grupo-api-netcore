using Service.Grupo.Application.Base;

namespace Service.Grupo.Application.Test.Util
{
    public static class ModelAssertExtension
    {
        public static void ComMensagem(this ExcecaoDeModelo exception, string mensagem)
        {
            if(exception.MensagensDeErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a mensagem '{mensagem}'");
        }
    }
}
