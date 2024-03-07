using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_reset_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_code_send_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_code_used",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "forgot_password_code",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "forgot_password_code_send_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "appointment_price",
                table: "doctor_details",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    doctor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    epicrisis_report = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    is_it_finished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointments", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointments_users_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_appointments_users_patient_id",
                        column: x => x.patient_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_appointments_doctor_id",
                table: "appointments",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_patient_id",
                table: "appointments",
                column: "patient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropColumn(
                name: "forgot_password_code",
                table: "users");

            migrationBuilder.DropColumn(
                name: "forgot_password_code_send_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "appointment_price",
                table: "doctor_details");

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
        }
    }
}
