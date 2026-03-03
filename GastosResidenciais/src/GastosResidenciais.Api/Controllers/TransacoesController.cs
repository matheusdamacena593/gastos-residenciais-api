using GastosResidenciais.Application.UseCases.Transacoes.Count;
using GastosResidenciais.Application.UseCases.Transacoes.GetAll;
using GastosResidenciais.Application.UseCases.Transacoes.GetById;
using GastosResidenciais.Application.UseCases.Transacoes.Register;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Communication.Responses.Transacoes;
using GastosResidenciais.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseTransacaoJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterTransacaoUseCase useCase,
            [FromBody] RequestTransacaoJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageResultDTO<ResponseTransacaoJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IGetAllTransacoesUseCase useCase)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Items == null || response.Items.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTransacaoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetTransacaoByIdUseCase useCase,
            [FromRoute] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(ResponseCountJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count([FromServices] IGetTransacoesCountUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
    }
}
