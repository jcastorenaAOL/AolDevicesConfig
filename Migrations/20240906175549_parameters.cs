using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AolDevicesConfig.Migrations
{
    /// <inheritdoc />
    public partial class parameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParameterName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigurations",
                columns: table => new
                {
                    ConfigID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PID = table.Column<string>(type: "text", nullable: true),
                    PIDConfig = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurations", x => x.ConfigID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConfigParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceConfiguration = table.Column<int>(type: "integer", nullable: false),
                    ConfigParameter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceConfigParameters_ConfigParameters_ConfigParameter",
                        column: x => x.ConfigParameter,
                        principalTable: "ConfigParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceConfigParameters_DeviceConfigurations_DeviceConfigura~",
                        column: x => x.DeviceConfiguration,
                        principalTable: "DeviceConfigurations",
                        principalColumn: "ConfigID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigParameters_ConfigParameter",
                table: "DeviceConfigParameters",
                column: "ConfigParameter");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConfigParameters_DeviceConfiguration",
                table: "DeviceConfigParameters",
                column: "DeviceConfiguration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConfigParameters");

            migrationBuilder.DropTable(
                name: "ConfigParameters");

            migrationBuilder.DropTable(
                name: "DeviceConfigurations");
        }
    }
}
