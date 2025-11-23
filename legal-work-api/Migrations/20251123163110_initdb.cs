using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace legal_work_api.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lw_empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RazaoSocial = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lw_empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lw_funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lw_funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lw_jornada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    HorasTrabalhadas = table.Column<string>(type: "NVARCHAR2(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lw_jornada", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lw_empresa");

            migrationBuilder.DropTable(
                name: "lw_funcionario");

            migrationBuilder.DropTable(
                name: "lw_jornada");
        }
    }
}
