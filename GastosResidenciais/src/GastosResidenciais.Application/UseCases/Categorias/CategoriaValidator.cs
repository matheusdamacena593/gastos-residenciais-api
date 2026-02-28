using FluentValidation;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Exception;

namespace GastosResidenciais.Application.UseCases.Categorias
{
    public class CategoriaValidator : AbstractValidator<RequestCategoriaJson>
    {
        public CategoriaValidator()
        {
            RuleFor(categoria => categoria.Descricao).NotEmpty().WithMessage(ResourceErrorMessages.DESCRICAO_OBRIGATORIA);
            RuleFor(categoria => categoria.Descricao).MaximumLength(400).WithMessage(ResourceErrorMessages.DESCRICAO_INVALIDA);
            RuleFor(categoria => categoria.Finalidade).IsInEnum().WithMessage(ResourceErrorMessages.FINALIDADE_ENUM_INVALIDO);
        }
    }
}
