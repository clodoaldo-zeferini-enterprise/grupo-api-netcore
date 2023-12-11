using Bogus;
using Service.Grupo.Application.Base;
using Service.Grupo.Application.Test.Builders;
using Service.Grupo.Application.Test.Util;

namespace Service.Grupo.Application.Test.Empresa.Models.Request
{
    public class UpdateGrupoRequestTest
    {
        public UpdateGrupoRequestTest()
        {
        }   

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_Deve_Empresa_Ser_Atualizada_Com_Nome_Invalido(string nomeEmpresaInvalido)
        {
            Assert.Throws<ExcecaoDeModelo>(() =>
                UpdateGrupoRequestBuilder.Novo().ComNomeDoGrupo(nomeEmpresaInvalido).Build())
                .ComMensagem(Resource.NomeDoGrupoInvalido);
        }
    }
}
