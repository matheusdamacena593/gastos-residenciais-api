using AutoMapper;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.DTOs;
using GastosResidenciais.Domain.Repositories.Categorias;

namespace GastosResidenciais.Application.UseCases.Categorias.GetAll
{
    public class GetAllCategoriasUseCase : IGetAllCategoriasUseCase
    {
        private readonly ICategoriaReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriasUseCase(
            ICategoriaReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageResultDTO<ResponseCategoriaJson>> Execute(int page, int pageSize)
        {
            var result = await _repository.GetAll(page, pageSize);

            return new PageResultDTO<ResponseCategoriaJson>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Items = _mapper.Map<List<ResponseCategoriaJson>>(result.Items)
            };
        }
    }
}
