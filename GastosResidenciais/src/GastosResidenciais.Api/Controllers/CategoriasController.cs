using GastosResidenciais.Application.UseCases.Categorias.Count;
using GastosResidenciais.Application.UseCases.Categorias.GetAll;
using GastosResidenciais.Application.UseCases.Categorias.GetById;
using GastosResidenciais.Application.UseCases.Categorias.Register;
using GastosResidenciais.Communication.Requests;
using GastosResidenciais.Communication.Responses;
using GastosResidenciais.Communication.Responses.Categorias;
using GastosResidenciais.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GastosResidenciais.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCategoriaJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterCategoriaUseCase useCase,
            [FromBody] RequestCategoriaJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageResultDTO<ResponseCategoriaJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page,
            [FromQuery] int pageSize, 
            [FromServices] IGetAllCategoriasUseCase useCase)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Items == null || response.Items.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCategoriaJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetCategoriaByIdUseCase useCase,
            [FromRoute] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(ResponseCountJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count([FromServices] IGetCategoriasCountUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
    }
}
