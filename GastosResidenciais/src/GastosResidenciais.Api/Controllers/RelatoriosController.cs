using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisCategorias;
using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisPessoas;
using GastosResidenciais.Communication.Responses.Relatorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {

        [HttpGet("totais-pessoas")]
        [ProducesResponseType(typeof(ResponseTotaisPessoasJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTotaisPessoas(
            [FromServices] IGetTotaisPessoasRelatorioUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Pessoas.Any())
                return Ok(response);

            return NoContent();
        }

        [HttpGet("totais-categorias")]
        [ProducesResponseType(typeof(ResponseTotaisCategoriasJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTotaisCategorias(
            [FromServices] IGetTotaisCategoriasRelatorioUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Categorias.Any())
                return Ok(response);

            return NoContent();
        }
    }
}
