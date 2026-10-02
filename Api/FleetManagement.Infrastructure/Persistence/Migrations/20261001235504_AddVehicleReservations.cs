using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reservation_id",
                schema: "operations",
                table: "vehicle_movements",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vehicle_reservations",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    purpose = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    revision = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_reservations", x => x.id);
                    table.CheckConstraint("ck_vehicle_reservations_dates", "end_utc > start_utc");
                    table.CheckConstraint("ck_vehicle_reservations_revision", "revision > 0");
                    table.CheckConstraint("ck_vehicle_reservations_status", "status IN ('Confirmed', 'InUse', 'Completed', 'Cancelled')");
                    table.ForeignKey(
                        name: "FK_vehicle_reservations_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "registrations",
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_reservations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS btree_gist;");
            migrationBuilder.Sql("""
                ALTER TABLE operations.vehicle_reservations
                ADD CONSTRAINT ex_vehicle_reservations_overlap
                EXCLUDE USING gist (
                    vehicle_id WITH =,
                    tstzrange(start_utc, end_utc, '[)') WITH &&
                ) WHERE (status IN ('Confirmed', 'InUse'));
                """);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_reservation_id",
                schema: "operations",
                table: "vehicle_movements",
                column: "reservation_id",
                unique: true,
                filter: "reservation_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_reservations_driver_id_start_utc",
                schema: "operations",
                table: "vehicle_reservations",
                columns: new[] { "driver_id", "start_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_reservations_vehicle_id_start_utc",
                schema: "operations",
                table: "vehicle_reservations",
                columns: new[] { "vehicle_id", "start_utc" });

            migrationBuilder.AddForeignKey(
                name: "FK_vehicle_movements_vehicle_reservations_reservation_id",
                schema: "operations",
                table: "vehicle_movements",
                column: "reservation_id",
                principalSchema: "operations",
                principalTable: "vehicle_reservations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicle_movements_vehicle_reservations_reservation_id",
                schema: "operations",
                table: "vehicle_movements");

            migrationBuilder.DropTable(
                name: "vehicle_reservations",
                schema: "operations");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_movements_reservation_id",
                schema: "operations",
                table: "vehicle_movements");

            migrationBuilder.DropColumn(
                name: "reservation_id",
                schema: "operations",
                table: "vehicle_movements");
        }
    }
}
