using Bogus;
using Service.Grupo.Application.Base;
using Service.Grupo.Application.Test.Builders;
using Service.Grupo.Application.Test.Util;

namespace Service.Grupo.Application.Test.Empresa.Models.Request
{
    public class InsertGrupoRequestTest
    {
        public InsertGrupoRequestTest()
        {
        }  
       
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_Deve_InsertGrupo_Ser_Criada_Com_NomeDoGrupo_Invalido(string nomeEmpresaInvalido)
        {
            Assert.Throws<ExcecaoDeModelo>(() =>
                InsertGrupoRequestBuilder.Novo().ComNomeDoGrupo(nomeEmpresaInvalido).Build())
                .ComMensagem(Resource.NomeDoGrupoInvalido);
        }
    }
}
