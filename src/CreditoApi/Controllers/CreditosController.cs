using CreditoApi.Application.Extensions;
using CreditoApi.Application.Modules.Creditos;
using CreditoApi.Domain.Creditos;
using CreditoApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CreditoApi.Controllers;

[Route("api/[controller]")]
public class CreditosController(ICreditoService creditoService) : ApiBaseController
{
    [HttpPost("integrar-credito-constituido")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        return result.IsFailure ? FromError(result.Error) : Accepted(new { success = true });
    }

    [HttpGet("{numeroNfse}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpGet("credito/{numeroCredito}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
