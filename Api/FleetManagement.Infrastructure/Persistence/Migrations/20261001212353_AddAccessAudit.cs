using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "audit_entries",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    occurred_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    actor_user_id = table.Column<int>(type: "integer", nullable: true),
                    actor_username = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    entity_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    entity_id = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    changes_json = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
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
                name: "IX_users_username",
                schema: "security",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_entries",
                schema: "security");

            migrationBuilder.DropTable(
                name: "users",
                schema: "security");
        }
    }
}
