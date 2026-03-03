using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisCategorias;
using GastosResidenciais.Application.UseCases.Relatorios.GetTotaisPessoas;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisCategorias;
using GastosResidenciais.Application.UseCases.Relatorios.Pdfs.PdfTotaisPessoas;
using GastosResidenciais.Communication.Responses.Relatorios;
using GastosResidenciais.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IGetTotaisPessoasRelatorioUseCase useCase)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Pessoas.Items == null || response.Pessoas.Items.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet("totais-categorias")]
        [ProducesResponseType(typeof(ResponseTotaisCategoriasJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTotaisCategorias(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromServices] IGetTotaisCategoriasRelatorioUseCase useCase)
        {
            var response = await useCase.Execute(page, pageSize);

            if (response.Categorias.Items == null || response.Categorias.Items.Count == 0)
                return NoContent();

            return Ok(response);
        }

        [HttpGet("pdf-totais-pessoas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdfTotaisPessoa(
            [FromServices] IPdfTotaisPessoasUseCase useCase)
        {
            byte[] file = await useCase.Execute();

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Pdf, "relatorio-pessoas.pdf");

            return NoContent();
        }
        
        [HttpGet("pdf-totais-categorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPdfTotaisPessoa(
            [FromServices] IPdfTotaisCategoriasUseCase useCase)
        {
            byte[] file = await useCase.Execute();

            if (file.Length > 0)
                return File(file, MediaTypeNames.Application.Pdf, "relatorio-categorias.pdf");

            return NoContent();
        }
    }
}
