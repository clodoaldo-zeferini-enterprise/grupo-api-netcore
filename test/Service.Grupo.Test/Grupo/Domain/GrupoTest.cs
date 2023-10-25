using Bogus;
using Service.Grupo.Domain.Base;
using Service.Grupo.Test.Builders.Domain;
using Service.Grupo.Test.Util;

namespace Service.Grupo.Test.Grupo.Domain
{
    public class GrupoTest
    {
        private readonly string _nome;

        private readonly Faker _faker;

        public GrupoTest()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_Deve_Grupo_Ter_Um_Nome_Invalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                GrupoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }
    }
}
