using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "password_reset_code",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "password_reset_code_send_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "password_reset_code_used",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ix_users_email_confirm_code",
                table: "users",
                column: "email_confirm_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_email_confirm_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_code_send_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_code_used",
                table: "users");
        }
    }
}
