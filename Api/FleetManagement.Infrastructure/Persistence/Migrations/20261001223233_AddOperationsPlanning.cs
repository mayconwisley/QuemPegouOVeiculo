using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationsPlanning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "expected_return_utc",
                schema: "operations",
                table: "vehicle_movements",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "plan_id",
                schema: "operations",
                table: "maintenance_records",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "maintenance_plans",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    interval_days = table.Column<int>(type: "integer", nullable: true),
                    interval_mileage = table.Column<int>(type: "integer", nullable: true),
                    next_due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    next_due_mileage = table.Column<int>(type: "integer", nullable: true),
                    last_completed_on = table.Column<DateOnly>(type: "date", nullable: true),
                    last_completed_mileage = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    revision = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance_plans", x => x.id);
                    table.CheckConstraint("ck_maintenance_plans_interval", "((interval_days IS NULL AND next_due_date IS NULL) OR (interval_days > 0 AND next_due_date IS NOT NULL)) AND ((interval_mileage IS NULL AND next_due_mileage IS NULL) OR (interval_mileage > 0 AND next_due_mileage >= 0)) AND (interval_days IS NOT NULL OR interval_mileage IS NOT NULL)");
                    table.CheckConstraint("ck_maintenance_plans_revision", "revision > 0");
                    table.ForeignKey(
                        name: "FK_maintenance_plans_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "movement_checklists",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movement_id = table.Column<int>(type: "integer", nullable: false),
                    phase = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    tires_ok = table.Column<bool>(type: "boolean", nullable: false),
                    lights_ok = table.Column<bool>(type: "boolean", nullable: false),
                    fluids_ok = table.Column<bool>(type: "boolean", nullable: false),
                    body_ok = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    checked_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movement_checklists", x => x.id);
                    table.CheckConstraint("ck_movement_checklists_phase", "phase IN ('departure', 'arrival')");
                    table.ForeignKey(
                        name: "FK_movement_checklists_vehicle_movements_movement_id",
                        column: x => x.movement_id,
                        principalSchema: "operations",
                        principalTable: "vehicle_movements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_expected_return_utc",
                schema: "operations",
                table: "vehicle_movements",
                column: "expected_return_utc",
                filter: "arrival_utc IS NULL AND expected_return_utc IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "ck_vehicle_movements_expected_return",
                schema: "operations",
                table: "vehicle_movements",
                sql: "expected_return_utc IS NULL OR expected_return_utc >= departure_utc");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_plan_id",
                schema: "operations",
                table: "maintenance_records",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_plans_next_due_date",
                schema: "operations",
                table: "maintenance_plans",
                column: "next_due_date",
                filter: "is_active = true AND next_due_date IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_plans_vehicle_id_is_active",
                schema: "operations",
                table: "maintenance_plans",
                columns: new[] { "vehicle_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "IX_movement_checklists_movement_id_phase",
                schema: "operations",
                table: "movement_checklists",
                columns: new[] { "movement_id", "phase" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_maintenance_records_maintenance_plans_plan_id",
                schema: "operations",
                table: "maintenance_records",
                column: "plan_id",
                principalSchema: "operations",
                principalTable: "maintenance_plans",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_maintenance_records_maintenance_plans_plan_id",
                schema: "operations",
                table: "maintenance_records");

            migrationBuilder.DropTable(
                name: "maintenance_plans",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "movement_checklists",
                schema: "operations");

            migrationBuilder.DropIndex(
                name: "IX_vehicle_movements_expected_return_utc",
                schema: "operations",
                table: "vehicle_movements");

            migrationBuilder.DropCheckConstraint(
                name: "ck_vehicle_movements_expected_return",
                schema: "operations",
                table: "vehicle_movements");

            migrationBuilder.DropIndex(
                name: "IX_maintenance_records_plan_id",
                schema: "operations",
                table: "maintenance_records");

            migrationBuilder.DropColumn(
                name: "expected_return_utc",
                schema: "operations",
                table: "vehicle_movements");

            migrationBuilder.DropColumn(
                name: "plan_id",
                schema: "operations",
                table: "maintenance_records");
        }
    }
}
