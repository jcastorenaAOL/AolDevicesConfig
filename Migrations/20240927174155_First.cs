using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AolDevicesConfig.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
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
                    ParameterName = table.Column<string>(type: "text", nullable: false)
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
                    PID = table.Column<string>(type: "text", nullable: false),
                    PIDConfig = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConfigurations", x => x.ConfigID);
                });

            migrationBuilder.CreateTable(
                name: "GamepadLayouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LayoutName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamepadLayouts", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "LayoutComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    GamepadLayoutId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LayoutComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LayoutComponents_GamepadLayouts_GamepadLayoutId",
                        column: x => x.GamepadLayoutId,
                        principalTable: "GamepadLayouts",
                        principalColumn: "Id",
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

            migrationBuilder.CreateIndex(
                name: "IX_LayoutComponents_GamepadLayoutId",
                table: "LayoutComponents",
                column: "GamepadLayoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConfigParameters");

            migrationBuilder.DropTable(
                name: "LayoutComponents");

            migrationBuilder.DropTable(
                name: "ConfigParameters");

            migrationBuilder.DropTable(
                name: "DeviceConfigurations");

            migrationBuilder.DropTable(
                name: "GamepadLayouts");
        }
    }
}
