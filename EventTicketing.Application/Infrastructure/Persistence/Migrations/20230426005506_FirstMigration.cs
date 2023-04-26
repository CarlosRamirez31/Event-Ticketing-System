using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Application.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EventCreatedBy = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneId);
                });

            migrationBuilder.CreateTable(
                name: "EventPrices",
                columns: table => new
                {
                    EventPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPrices", x => x.EventPriceId);
                    table.ForeignKey(
                        name: "FK_EventPriceEvent",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventPriceZone",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_PlaceZone",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EventPriceId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    ParticipantName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TicketEventPrice",
                        column: x => x.EventPriceId,
                        principalTable: "EventPrices",
                        principalColumn: "EventPriceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPlace",
                        column: x => x.TicketId,
                        principalTable: "Places",
                        principalColumn: "PlaceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPrices_EventId",
                table: "EventPrices",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPrices_ZoneId",
                table: "EventPrices",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_ZoneId",
                table: "Places",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventPriceId",
                table: "Tickets",
                column: "EventPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_Name",
                table: "Zones",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "EventPrices");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
