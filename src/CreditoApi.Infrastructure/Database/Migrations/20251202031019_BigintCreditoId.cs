using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CreditoApi.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class BigintCreditoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credito",
                schema: "credito_api");

            migrationBuilder.CreateTable(
                name: "credito",
                schema: "credito_api",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero_credito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_nfse = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    data_constituicao = table.Column<DateOnly>(type: "date", nullable: false),
                    valor_issqn = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    tipo_credito = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    simples_nacional = table.Column<bool>(type: "boolean", nullable: false),
                    aliquota = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    valor_faturado = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    valor_deducao = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    base_calculo = table.Column<decimal>(type: "numeric(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credito", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_credito_numero_credito",
                schema: "credito_api",
                table: "credito",
                column: "numero_credito",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credito",
                schema: "credito_api");

            migrationBuilder.CreateTable(
                name: "credito",
                schema: "credito_api",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    aliquota = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    base_calculo = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    data_constituicao = table.Column<DateOnly>(type: "date", nullable: false),
                    numero_credito = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    numero_nfse = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    simples_nacional = table.Column<bool>(type: "boolean", nullable: false),
                    tipo_credito = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    valor_deducao = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    valor_faturado = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    valor_issqn = table.Column<decimal>(type: "numeric(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credito", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_credito_numero_credito",
                schema: "credito_api",
                table: "credito",
                column: "numero_credito",
                unique: true);
        }
    }
}
