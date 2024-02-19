using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ExaminationId",
                table: "Prescriptions",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions",
                column: "MedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Examination_ExaminationId",
                table: "Prescriptions",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medication_MedicationId",
                table: "Prescriptions",
                column: "MedicationId",
                principalTable: "Medication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Examination_ExaminationId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medication_MedicationId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_ExaminationId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicationId",
                table: "Prescriptions");
        }
    }
}
