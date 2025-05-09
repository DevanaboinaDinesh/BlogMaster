using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthBb
{
    /// <inheritdoc />
    public partial class once : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1704ecd2-9c8e-4ddb-9315-1c09b7382cdf", "AQAAAAIAAYagAAAAEO8V07KbjC+641Zr3SH4VMjTrMOOItIY4wgyAVheNfL2tRPg54BpcAboCUWlYwQOSQ==", "8b972ff8-55af-4861-b6c3-8350732ef1a1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11c078f0-e356-42e1-8117-db6f94955f29", null, "6e8d67dd-112c-484f-a471-9f7ceeba8bc7" });
        }
    }
}
