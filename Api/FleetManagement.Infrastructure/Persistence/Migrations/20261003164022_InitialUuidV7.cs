using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialUuidV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "registrations");

            migrationBuilder.EnsureSchema(
                name: "operations");

            migrationBuilder.CreateTable(
                name: "audit_entries",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actor_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    actor_username = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    entity_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    changes_json = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                schema: "registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    license_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    license_expiration = table.Column<DateOnly>(type: "date", nullable: false),
                    license_category = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    rg = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    security_version = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                schema: "registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plate = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    model = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    chassis = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    renavam = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "license_expirations",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    expired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_license_expirations", x => x.id);
                    table.ForeignKey(
                        name: "FK_license_expirations_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "registrations",
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fines",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    points = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fines", x => x.id);
                    table.CheckConstraint("ck_fines_values", "amount >= 0 AND points >= 0");
                    table.ForeignKey(
                        name: "FK_fines_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "registrations",
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_fines_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "maintenance_plans",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "refuelings",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mileage = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    liters = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refuelings", x => x.id);
                    table.CheckConstraint("ck_refuelings_values", "mileage >= 0 AND amount >= 0 AND liters > 0");
                    table.ForeignKey(
                        name: "FK_refuelings_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "registrations",
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_refuelings_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_reservations",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "vehicle_statuses",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_statuses", x => x.id);
                    table.CheckConstraint("ck_vehicle_statuses_dates", "end_utc IS NULL OR end_utc >= start_utc");
                    table.ForeignKey(
                        name: "FK_vehicle_statuses_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "maintenance_records",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plan_id = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maintenance_records", x => x.id);
                    table.CheckConstraint("ck_maintenance_records_amount", "amount >= 0");
                    table.ForeignKey(
                        name: "FK_maintenance_records_maintenance_plans_plan_id",
                        column: x => x.plan_id,
                        principalSchema: "operations",
                        principalTable: "maintenance_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_maintenance_records_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "registrations",
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_movements",
                schema: "operations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reservation_id = table.Column<Guid>(type: "uuid", nullable: true),
                    departure_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    arrival_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expected_return_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    initial_mileage = table.Column<int>(type: "integer", nullable: false),
                    final_mileage = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_movements", x => x.id);
                    table.CheckConstraint("ck_vehicle_movements_arrival", "(arrival_utc IS NULL) = (final_mileage IS NULL) AND (arrival_utc IS NULL OR arrival_utc >= departure_utc)");
                    table.CheckConstraint("ck_vehicle_movements_expected_return", "expected_return_utc IS NULL OR expected_return_utc >= departure_utc");
                    table.CheckConstraint("ck_vehicle_movements_mileage", "initial_mileage >= 0 AND (final_mileage IS NULL OR final_mileage >= initial_mileage)");
                    table.ForeignKey(
                        name: "FK_vehicle_movements_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "registrations",
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_movements_vehicle_reservations_reservation_id",
                        column: x => x.reservation_id,
                        principalSchema: "operations",
                        principalTable: "vehicle_reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_movements_vehicles_vehicle_id",
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    movement_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "IX_audit_entries_entity_name_entity_id",
                schema: "security",
                table: "audit_entries",
                columns: new[] { "entity_name", "entity_id" });

            migrationBuilder.CreateIndex(
                name: "IX_audit_entries_occurred_at_utc",
                schema: "security",
                table: "audit_entries",
                column: "occurred_at_utc");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_cpf",
                schema: "registrations",
                table: "drivers",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fines_driver_id",
                schema: "operations",
                table: "fines",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_fines_vehicle_id_date",
                schema: "operations",
                table: "fines",
                columns: new[] { "vehicle_id", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_license_expirations_driver_id_date",
                schema: "operations",
                table: "license_expirations",
                columns: new[] { "driver_id", "date" });

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
                name: "IX_maintenance_records_plan_id",
                schema: "operations",
                table: "maintenance_records",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_maintenance_records_vehicle_id_date",
                schema: "operations",
                table: "maintenance_records",
                columns: new[] { "vehicle_id", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_movement_checklists_movement_id_phase",
                schema: "operations",
                table: "movement_checklists",
                columns: new[] { "movement_id", "phase" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refuelings_driver_id",
                schema: "operations",
                table: "refuelings",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_refuelings_vehicle_id_date",
                schema: "operations",
                table: "refuelings",
                columns: new[] { "vehicle_id", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                schema: "security",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_driver_id_departure_utc",
                schema: "operations",
                table: "vehicle_movements",
                columns: new[] { "driver_id", "departure_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_expected_return_utc",
                schema: "operations",
                table: "vehicle_movements",
                column: "expected_return_utc",
                filter: "arrival_utc IS NULL AND expected_return_utc IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_reservation_id",
                schema: "operations",
                table: "vehicle_movements",
                column: "reservation_id",
                unique: true,
                filter: "reservation_id IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_vehicle_id",
                schema: "operations",
                table: "vehicle_movements",
                column: "vehicle_id",
                unique: true,
                filter: "arrival_utc IS NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_statuses_vehicle_id_start_utc",
                schema: "operations",
                table: "vehicle_statuses",
                columns: new[] { "vehicle_id", "start_utc" });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_plate",
                schema: "registrations",
                table: "vehicles",
                column: "plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_entries",
                schema: "security");

            migrationBuilder.DropTable(
                name: "fines",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "license_expirations",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "maintenance_records",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "movement_checklists",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "refuelings",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "users",
                schema: "security");

            migrationBuilder.DropTable(
                name: "vehicle_statuses",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "maintenance_plans",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "vehicle_movements",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "vehicle_reservations",
                schema: "operations");

            migrationBuilder.DropTable(
                name: "drivers",
                schema: "registrations");

            migrationBuilder.DropTable(
                name: "vehicles",
                schema: "registrations");
        }
    }
}
