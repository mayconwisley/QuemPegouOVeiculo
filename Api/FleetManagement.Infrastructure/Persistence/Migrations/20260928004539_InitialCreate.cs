using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "operacoes");

            migrationBuilder.EnsureSchema(
                name: "cadastros");

            migrationBuilder.CreateTable(
                name: "motoristas",
                schema: "cadastros",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cnh = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    vencimento_cnh = table.Column<DateOnly>(type: "date", nullable: false),
                    categoria_cnh = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    rg = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motoristas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                schema: "cadastros",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    modelo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    chassi = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    renavam = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vencimentos_cnh",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    motorista_id = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    vencido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vencimentos_cnh", x => x.id);
                    table.ForeignKey(
                        name: "FK_vencimentos_cnh_motoristas_motorista_id",
                        column: x => x.motorista_id,
                        principalSchema: "cadastros",
                        principalTable: "motoristas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abastecimentos",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false),
                    motorista_id = table.Column<int>(type: "integer", nullable: false),
                    quilometragem = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    litros = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abastecimentos", x => x.id);
                    table.CheckConstraint("ck_abastecimentos_valores", "quilometragem >= 0 AND valor >= 0 AND litros > 0");
                    table.ForeignKey(
                        name: "FK_abastecimentos_motoristas_motorista_id",
                        column: x => x.motorista_id,
                        principalSchema: "cadastros",
                        principalTable: "motoristas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abastecimentos_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "cadastros",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "manutencoes",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manutencoes", x => x.id);
                    table.CheckConstraint("ck_manutencoes_valor", "valor >= 0");
                    table.ForeignKey(
                        name: "FK_manutencoes_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "cadastros",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "movimentacoes",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false),
                    motorista_id = table.Column<int>(type: "integer", nullable: false),
                    saida_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    chegada_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    km_inicial = table.Column<int>(type: "integer", nullable: false),
                    km_final = table.Column<int>(type: "integer", nullable: true),
                    descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacoes", x => x.id);
                    table.CheckConstraint("ck_movimentacoes_chegada", "(chegada_utc IS NULL) = (km_final IS NULL) AND (chegada_utc IS NULL OR chegada_utc >= saida_utc)");
                    table.CheckConstraint("ck_movimentacoes_km", "km_inicial >= 0 AND (km_final IS NULL OR km_final >= km_inicial)");
                    table.ForeignKey(
                        name: "FK_movimentacoes_motoristas_motorista_id",
                        column: x => x.motorista_id,
                        principalSchema: "cadastros",
                        principalTable: "motoristas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movimentacoes_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "cadastros",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "multas",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false),
                    motorista_id = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    valor = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    pontos = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_multas", x => x.id);
                    table.CheckConstraint("ck_multas_valores", "valor >= 0 AND pontos >= 0");
                    table.ForeignKey(
                        name: "FK_multas_motoristas_motorista_id",
                        column: x => x.motorista_id,
                        principalSchema: "cadastros",
                        principalTable: "motoristas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_multas_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "cadastros",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "status_veiculos",
                schema: "operacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    veiculo_id = table.Column<int>(type: "integer", nullable: false),
                    inicio_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fim_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    descricao = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_veiculos", x => x.id);
                    table.CheckConstraint("ck_status_veiculos_datas", "fim_utc IS NULL OR fim_utc >= inicio_utc");
                    table.ForeignKey(
                        name: "FK_status_veiculos_veiculos_veiculo_id",
                        column: x => x.veiculo_id,
                        principalSchema: "cadastros",
                        principalTable: "veiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abastecimentos_motorista_id",
                schema: "operacoes",
                table: "abastecimentos",
                column: "motorista_id");

            migrationBuilder.CreateIndex(
                name: "IX_abastecimentos_veiculo_id_data",
                schema: "operacoes",
                table: "abastecimentos",
                columns: new[] { "veiculo_id", "data" });

            migrationBuilder.CreateIndex(
                name: "IX_manutencoes_veiculo_id_data",
                schema: "operacoes",
                table: "manutencoes",
                columns: new[] { "veiculo_id", "data" });

            migrationBuilder.CreateIndex(
                name: "IX_motoristas_cpf",
                schema: "cadastros",
                table: "motoristas",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movimentacoes_motorista_id_saida_utc",
                schema: "operacoes",
                table: "movimentacoes",
                columns: new[] { "motorista_id", "saida_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_movimentacoes_veiculo_id",
                schema: "operacoes",
                table: "movimentacoes",
                column: "veiculo_id",
                unique: true,
                filter: "chegada_utc IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_multas_motorista_id",
                schema: "operacoes",
                table: "multas",
                column: "motorista_id");

            migrationBuilder.CreateIndex(
                name: "IX_multas_veiculo_id_data",
                schema: "operacoes",
                table: "multas",
                columns: new[] { "veiculo_id", "data" });

            migrationBuilder.CreateIndex(
                name: "IX_status_veiculos_veiculo_id_inicio_utc",
                schema: "operacoes",
                table: "status_veiculos",
                columns: new[] { "veiculo_id", "inicio_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_placa",
                schema: "cadastros",
                table: "veiculos",
                column: "placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vencimentos_cnh_motorista_id_data",
                schema: "operacoes",
                table: "vencimentos_cnh",
                columns: new[] { "motorista_id", "data" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abastecimentos",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "manutencoes",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "movimentacoes",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "multas",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "status_veiculos",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "vencimentos_cnh",
                schema: "operacoes");

            migrationBuilder.DropTable(
                name: "veiculos",
                schema: "cadastros");

            migrationBuilder.DropTable(
                name: "motoristas",
                schema: "cadastros");
        }
    }
}
