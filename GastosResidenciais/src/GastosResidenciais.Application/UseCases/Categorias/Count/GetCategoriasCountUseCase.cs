using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Domain.Repositories.Pessoas;

namespace GastosResidenciais.Application.UseCases.Categorias.Count
{
    public class GetCategoriasCountUseCase : IGetCategoriasCountUseCase
    {
        private readonly ICategoriaReadOnlyRepository _repository;

        public GetCategoriasCountUseCase(ICategoriaReadOnlyRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResponseCountJson> Execute()
        {
            var total = await _repository.CountAsync();

            return new ResponseCountJson { Total = total };
        }
    }
}
