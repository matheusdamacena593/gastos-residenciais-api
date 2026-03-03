using CommonTesteUtilities.Requests;
using GastosResidenciais.Application.UseCases.Pessoas;
using FluentAssertions;
using GastosResidenciais.Exception;

namespace Validators.Tests.Pessoas
{
    public class RegisterPessoaValidatorTests
    {
        [Fact]
        public void Success()
        {
            // Arrange
            var validator = new PessoaValidator();
            var request = RequestRegisterPessoaJsonBuilder.Build();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Error_Nome_Vazio(string nome)
        {
            // Arrange
            var validator = new PessoaValidator();
            var request = RequestRegisterPessoaJsonBuilder.Build();
            request.Nome = nome;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_OBRIGATORIO));
        }

        [Theory]
        [InlineData("João Victor Henrique Antônio de Oliveira e Silva Pereira dos Santos Almeida Rodrigues Costa Ferreira Carvalho " +
            "Gomes Martins Araújo Barbosa Cardoso Ribeiro Monteiro Mendes Nascimento Lima Teixeira Correia Dias Moreira Cunha Vieira " +
            "Ramos Freitas Pinto Moura Campos Castro Rocha Neves Braga")]
        public void Error_Nome_Maior_200_Caracteres(string nome)
        {
            // Arrange
            var validator = new PessoaValidator();
            var request = RequestRegisterPessoaJsonBuilder.Build();
            request.Nome = nome;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.NOME_INVALIDO));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-65)]
        public void Error_Idade_Invalida(int idade)
        {
            // Arrange
            var validator = new PessoaValidator();
            var request = RequestRegisterPessoaJsonBuilder.Build();
            request.Idade = idade;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.IDADE_NAO_PERMITIDA));
        }
    }
}
