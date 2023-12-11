using Bogus;
using Service.Grupo.Application.Base;
using Service.Grupo.Application.Test.Builders;
using Service.Grupo.Application.Test.Util;

namespace Service.Grupo.Application.Test.Empresa.Models.Request
{
    public class DeleteGrupoRequestTest
    {
        public DeleteGrupoRequestTest()
        {
        }    

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_Deve_Empresa_Ser_Criada_Com_Nome_Invalido(string nomeEmpresaInvalido)
        {
            Assert.Throws<ExcecaoDeModelo>(() =>
                InsertGrupoRequestBuilder.Novo().ComNomeDoGrupo(nomeEmpresaInvalido).Build())
                .ComMensagem(Resource.NomeDoGrupoInvalido);
        }
    }
}
