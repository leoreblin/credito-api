using CreditoApi.Application.Modules.Creditos;

namespace CreditoApi.UnitTests;

public class CreditoDtoTests
{
    [Theory]
    [InlineData("Sim", true)]
    [InlineData("Nao", false)]
    [InlineData("Não", false)]
    public void MapToEntity_ShouldTranslateSimplesNacional(string flag, bool expected)
    {
        CreditoDto dto = CreateValidDto(simplesNacional: flag);

        var entity = dto.MapToEntity();

        Assert.Equal(expected, entity.SimplesNacional);
        Assert.Equal(dto.NumeroCredito, entity.NumeroCredito);
        Assert.Equal(dto.NumeroNfse, entity.NumeroNfse);
    }

    [Fact]
    public void MapToEntity_ShouldThrow_WhenSimplesNacionalIsInvalid()
    {
        CreditoDto dto = CreateValidDto(simplesNacional: "talvez");

        Assert.Throws<ArgumentException>(() => dto.MapToEntity());
    }

    [Fact]
    public void IntegrarCreditoRequestValidator_ShouldDetectDuplicatedNumeroCredito()
    {
        IntegrarCreditoRequestValidator validator = new();
        CreditoDto credito1 = CreateValidDto(numeroCredito: "123");
        CreditoDto credito2 = CreateValidDto(numeroCredito: "123");
        IntegrarCreditoRequest request = new([credito1, credito2]);

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorCode == CreditoError.NumeroCreditoDeveSerUnico.Code);
    }

    private static CreditoDto CreateValidDto(string numeroCredito = "123456", string simplesNacional = "Sim")
    {
        return new CreditoDto
        {
            NumeroCredito = numeroCredito,
            NumeroNfse = "7891011",
            DataConstituicao = new DateOnly(2024, 2, 25),
            ValorIssqn = 1500.75m,
            TipoCredito = "ISSQN",
            SimplesNacional = simplesNacional,
            Aliquota = 5.0m,
            ValorFaturado = 30000.00m,
            ValorDeducao = 5000.00m,
            BaseCalculo = 25000.00m
        };
    }
}
