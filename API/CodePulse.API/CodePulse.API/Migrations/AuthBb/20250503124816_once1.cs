using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthBb
{
    /// <inheritdoc />
    public partial class once1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23141181-4ec8-4f19-b4cd-5adc2af23c7e", "AQAAAAIAAYagAAAAEDQEF37MhvffbbJ0bvlBkyJou/sYYODCnSIBsrrdohaS0OC6oKl90aA56KWhRL139g==", "58d6a802-c846-4ab2-b11c-b601d157e7cd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1704ecd2-9c8e-4ddb-9315-1c09b7382cdf", "AQAAAAIAAYagAAAAEO8V07KbjC+641Zr3SH4VMjTrMOOItIY4wgyAVheNfL2tRPg54BpcAboCUWlYwQOSQ==", "8b972ff8-55af-4861-b6c3-8350732ef1a1" });
        }
    }
}
