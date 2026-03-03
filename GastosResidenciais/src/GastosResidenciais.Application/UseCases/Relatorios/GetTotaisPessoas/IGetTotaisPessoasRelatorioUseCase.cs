using GastosResidenciais.Communication.Responses.Relatorios;

namespace GastosResidenciais.Application.UseCases.Relatorios.GetTotaisPessoas
{
    public interface IGetTotaisPessoasRelatorioUseCase
    {
        Task<ResponseTotaisPessoasJson> Execute(int page, int pageSize);
    }
}
