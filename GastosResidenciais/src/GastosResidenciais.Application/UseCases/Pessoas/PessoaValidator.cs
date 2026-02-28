using FluentValidation;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Exception;

namespace GastosResidenciais.Application.UseCases.Pessoas
{
    public class PessoaValidator : AbstractValidator<RequestPessoaJson>
    {
        public PessoaValidator()
        {
            RuleFor(pessoa => pessoa.Nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_OBRIGATORIO);
            RuleFor(pessoa => pessoa.Nome).MaximumLength(200).WithMessage(ResourceErrorMessages.NOME_INVALIDO);
            RuleFor(pessoa => pessoa.Idade).GreaterThan(0).WithMessage(ResourceErrorMessages.IDADE_NAO_PERMITIDA);
        }
    }
}
