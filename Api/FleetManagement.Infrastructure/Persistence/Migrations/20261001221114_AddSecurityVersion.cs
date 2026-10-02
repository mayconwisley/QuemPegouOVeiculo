using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "security_version",
                schema: "security",
                table: "users",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE security.users SET security_version = md5(random()::text || id::text || clock_timestamp()::text) WHERE security_version = '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "security_version",
                schema: "security",
                table: "users");
        }
    }
}
