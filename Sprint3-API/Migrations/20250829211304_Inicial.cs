﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sprint3_API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARGO",
                columns: table => new
                {
                    ID_CARGO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME_CARGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DESCRICAO_CARGO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARGO", x => x.ID_CARGO);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TELEFONE_CLIENTE = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    NOME_CLIENTE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SEXO_CLIENTE = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false),
                    EMAIL_CLIENTE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF_CLIENTE = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LOCALIZACAO_PATIO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NOME_PATIO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DESCRICAO_PATIO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO", x => x.ID_PATIO);
                });

            migrationBuilder.CreateTable(
                name: "MOTO",
                columns: table => new
                {
                    ID_MOTO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PLACA_MOTO = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    MODELO_MOTO = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    SITUACAO_MOTO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CHASSI_MOTO = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    CLIENTE_ID_CLIENTE = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTO", x => x.ID_MOTO);
                    table.ForeignKey(
                        name: "CLIENTE_FK",
                        column: x => x.CLIENTE_ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.SetNull);
                });
            
            migrationBuilder.Sql(
                "ALTER TABLE MOTO ADD CONSTRAINT CHK_MODELO_MOTO CHECK (MODELO_MOTO IN ('Mottu Pop', 'Mottu Sport', 'Mottu-E'))"
            );

            migrationBuilder.Sql(
                "ALTER TABLE MOTO ADD CONSTRAINT CHK_SITUACAO_MOTO CHECK (SITUACAO_MOTO IN ('Inativa', 'Ativa', 'Manutenção', 'Em Trânsito'))"
            );


            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    ID_FUNCIONARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME_FUNCIONARIO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TELEFONE_FUNCIONARIO = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CARGO_ID_CARGO = table.Column<int>(type: "integer", nullable: false),
                    PATIO_ID_PATIO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.ID_FUNCIONARIO);
                    table.ForeignKey(
                        name: "CARGO_FK_FUNCIONARIO",
                        column: x => x.CARGO_ID_CARGO,
                        principalTable: "CARGO",
                        principalColumn: "ID_CARGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_PATIO_PATIO_ID_PATIO",
                        column: x => x.PATIO_ID_PATIO,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GERENTE",
                columns: table => new
                {
                    ID_GERENTE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME_GERENTE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TELEFONE_GERENTE = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CPF_GERENTE = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    PATIO_ID_PATIO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GERENTE", x => x.ID_GERENTE);
                    table.ForeignKey(
                        name: "PATIO_FK_GERENTE",
                        column: x => x.PATIO_ID_PATIO,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    ID_SETOR = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TIPO_SETOR = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    STATUS_SETOR = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PATIO_ID_PATIO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.ID_SETOR);
                    table.ForeignKey(
                        name: "PATIO_FK",
                        column: x => x.PATIO_ID_PATIO,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.Sql(
                "ALTER TABLE SETOR ADD CONSTRAINT CHK_STATUS_SETOR CHECK (STATUS_SETOR IN ('Cheia', 'Parcial', 'Livre'))"
            );

            migrationBuilder.Sql(
                "ALTER TABLE SETOR ADD CONSTRAINT CHK_TIPO_SETOR CHECK (TIPO_SETOR IN ('Pendência', 'Reparos Simples', 'Danos Estruturais Graves', 'Motor Defeituoso', 'Agendada Para Manutenção', 'Pronta para Aluguel', 'Sem Placa', 'Minha Mottu'))"
            );

            migrationBuilder.CreateTable(
                name: "VAGA",
                columns: table => new
                {
                    ID_VAGA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NUMERO_VAGA = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    STATUS_OCUPADA = table.Column<int>(type: "integer", nullable: false),
                    SETOR_ID_SETOR = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAGA", x => x.ID_VAGA);
                    table.ForeignKey(
                        name: "SETOR_FK_VAGA",
                        column: x => x.SETOR_ID_SETOR,
                        principalTable: "SETOR",
                        principalColumn: "ID_SETOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOVIMENTACAO",
                columns: table => new
                {
                    ID_MOVIMENTACAO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_SAIDA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DESCRICAO_MOVIMENTACAO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MOTO_ID_MOTO = table.Column<int>(type: "integer", nullable: false),
                    VAGA_ID_VAGA = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIMENTACAO", x => x.ID_MOVIMENTACAO);
                    table.ForeignKey(
                        name: "MOTO_FK",
                        column: x => x.MOTO_ID_MOTO,
                        principalTable: "MOTO",
                        principalColumn: "ID_MOTO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "VAGA_FK",
                        column: x => x.VAGA_ID_VAGA,
                        principalTable: "VAGA",
                        principalColumn: "ID_VAGA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_CPF_CLIENTE",
                table: "CLIENTE",
                column: "CPF_CLIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_CARGO_ID_CARGO",
                table: "FUNCIONARIO",
                column: "CARGO_ID_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_PATIO_ID_PATIO",
                table: "FUNCIONARIO",
                column: "PATIO_ID_PATIO");

            migrationBuilder.CreateIndex(
                name: "IX_GERENTE_CPF_GERENTE",
                table: "GERENTE",
                column: "CPF_GERENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GERENTE_PATIO_ID_PATIO",
                table: "GERENTE",
                column: "PATIO_ID_PATIO");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_CHASSI_MOTO",
                table: "MOTO",
                column: "CHASSI_MOTO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_CLIENTE_ID_CLIENTE",
                table: "MOTO",
                column: "CLIENTE_ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_PLACA_MOTO",
                table: "MOTO",
                column: "PLACA_MOTO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTACAO_MOTO_ID_MOTO",
                table: "MOVIMENTACAO",
                column: "MOTO_ID_MOTO");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTACAO_VAGA_ID_VAGA",
                table: "MOVIMENTACAO",
                column: "VAGA_ID_VAGA");

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_PATIO_ID_PATIO",
                table: "SETOR",
                column: "PATIO_ID_PATIO");

            migrationBuilder.CreateIndex(
                name: "IX_VAGA_SETOR_ID_SETOR",
                table: "VAGA",
                column: "SETOR_ID_SETOR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO");

            migrationBuilder.DropTable(
                name: "GERENTE");

            migrationBuilder.DropTable(
                name: "MOVIMENTACAO");

            migrationBuilder.DropTable(
                name: "CARGO");

            migrationBuilder.DropTable(
                name: "MOTO");

            migrationBuilder.DropTable(
                name: "VAGA");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "SETOR");

            migrationBuilder.DropTable(
                name: "PATIO");
        }
    }
}
