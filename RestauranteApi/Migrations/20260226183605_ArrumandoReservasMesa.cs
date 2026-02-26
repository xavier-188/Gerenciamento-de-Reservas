using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauranteApi.Migrations
{
    /// <inheritdoc />
    public partial class ArrumandoReservasMesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "Mesas");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "Reservas",
                newName: "DataHoraInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFim",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraFim",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "DataHoraInicio",
                table: "Reservas",
                newName: "DataHora");

            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "Mesas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
