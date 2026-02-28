using FluentValidation;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Domain.Enums;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Exception;

namespace GastosResidenciais.Application.UseCases.Transacoes
{
    public class TransacaoValidator : AbstractValidator<RequestTransacaoJson>
    {
        private readonly ICategoriaReadOnlyRepository _categoriaRepo;
        private readonly IPessoaReadOnlyRepository _pessoaRepo;

        public TransacaoValidator(
            ICategoriaReadOnlyRepository categoriaRepo,
            IPessoaReadOnlyRepository pessoaRepo)
        {
            _categoriaRepo = categoriaRepo;
            _pessoaRepo = pessoaRepo;

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage(ResourceErrorMessages.DESCRICAO_OBRIGATORIA)
                .MaximumLength(400).WithMessage(ResourceErrorMessages.DESCRICAO_INVALIDA);

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.VALOR_MAIOR_QUE_ZERO);

            RuleFor(x => x.TipoTransacao)
                .IsInEnum().WithMessage(ResourceErrorMessages.TIPO_TRANSACAO_ENUM_INVALIDO);

            RuleFor(x => x.PessoaId)
                .NotEmpty().WithMessage(ResourceErrorMessages.PESSOA_OBRIGATORIA)
                .MustAsync(PessoaExiste).WithMessage(ResourceErrorMessages.PESSOA_NAO_ENCONTRADA);

            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage(ResourceErrorMessages.CATEGORIA_OBRIGATORIA)
                .MustAsync(CategoriaExiste).WithMessage(ResourceErrorMessages.CATEGORIA_NAO_ENCONTRADA)
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .MustAsync(CategoriaCompativelComTipo)
                        .WithMessage(ResourceErrorMessages.CATEGORIA_INCOMPATIVEL_COM_TIPO);
                });
        }

        private async Task<bool> PessoaExiste(long pessoaId, CancellationToken ct)
            => await _pessoaRepo.ExistsById(pessoaId, ct);

        private async Task<bool> CategoriaExiste(long categoriaId, CancellationToken ct)
            => await _categoriaRepo.ExistsById(categoriaId, ct);

        private async Task<bool> CategoriaCompativelComTipo(RequestTransacaoJson req, CancellationToken ct)
        {
            var finalidade = await _categoriaRepo.GetFinalidadeById(req.CategoriaId, ct);
            if (finalidade is null) return false;

            if (finalidade == FinalidadeCategoriaEnum.Ambas)
                return true;

            return req.TipoTransacao switch
            {
                Communication.Enums.TipoTransacaoEnum.Despesa => finalidade == FinalidadeCategoriaEnum.Despesa,
                Communication.Enums.TipoTransacaoEnum.Receita => finalidade == FinalidadeCategoriaEnum.Receita,
                _ => false
            };
        }
    }
}
