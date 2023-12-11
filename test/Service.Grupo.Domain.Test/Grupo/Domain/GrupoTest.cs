using Bogus;
using Service.Grupo.Domain.Base;
using Service.Grupo.Domain.Test.Builders;
using Service.Grupo.Domain.Test.Util;

namespace Service.Grupo.Domain.Test.Empresa.Domain
{
    public class GrupoTest
    {
        private readonly string _nomeEmpresa;

        private readonly Faker _faker;

        public GrupoTest()
        {
            _faker = new Faker();

            _nomeEmpresa = _faker.Company.CompanyName();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_Deve_Empresa_Ser_Inserida_Com_Nome_Invalido(string nomeGrupoInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                GrupoBuilder.Novo().ComNomeDoGrupo(nomeGrupoInvalido).Build())
                .ComMensagem(Resource.NomeDoGrupoInvalido);
        }        
    }
}
