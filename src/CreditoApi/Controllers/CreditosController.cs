using CreditoApi.Application.Extensions;
using CreditoApi.Application.Modules.Creditos;
using CreditoApi.Domain.Creditos;
using CreditoApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditoApi.Controllers;

[Route("api/[controller]")]
public class CreditosController(ICreditoService creditoService) : ApiBaseController
{
    [SwaggerOperation(
        Summary = "Integra créditos constituídos",
        Description = "Valida o payload, publica cada crédito no tópico Kafka e retorna 202 Accepted.")]
    [SwaggerResponse(StatusCodes.Status202Accepted, "Mensagens publicadas com sucesso", typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação ou conflito", typeof(object))]
    [HttpPost("integrar-credito-constituido")]
    public async Task<IActionResult> IntegrarCreditosAsync(
        [FromBody] IntegrarCreditoRequest request,
        CancellationToken cancellationToken = default)
    {
        var validator = new IntegrarCreditoRequestValidator();
        var requestValidation = await validator.ValidateAsync(request, cancellationToken);
        if (!requestValidation.IsValid)
        {
            return BadRequest(requestValidation.ToErrors());
        }

        IEnumerable<Credito> creditosIntegrados = request.Creditos.Select(c => c.MapToEntity());
        var result = await creditoService.IntegrarCreditosAsync(creditosIntegrados, cancellationToken);

        return result.IsFailure ? BadRequest(result.Error) : Accepted(new { success = true });
    }

    [SwaggerOperation(
        Summary = "Obtém créditos por NFS-e",
        Description = "Retorna a lista de créditos constituídos associados ao número da NFS-e.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Lista retornada", typeof(IEnumerable<CreditoDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação", typeof(object))]
    [HttpGet("{numeroNfse}")]
    public async Task<IActionResult> ObterPorNumeroNfseAsync(
        string numeroNfse,
        CancellationToken cancellationToken = default)
    {
        var result = await creditoService.ObterPorNumeroNfseAsync(numeroNfse, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Select(v => (CreditoDto)v));
    }

    [SwaggerOperation(
        Summary = "Obtém crédito por número do crédito",
        Description = "Retorna os detalhes de um crédito constituído pelo número do crédito.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Crédito encontrado", typeof(CreditoDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Crédito não encontrado", typeof(object))]
    [HttpGet("credito/{numeroCredito}")]
    public async Task<IActionResult> ObterPorNumeroCreditoAsync(
        string numeroCredito,
        CancellationToken cancellationToken = default)
    {
        var result = await creditoService.ObterPorNumeroCreditoAsync(numeroCredito, cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok((CreditoDto)result.Value);
    }
}
