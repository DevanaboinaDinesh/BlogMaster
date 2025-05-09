using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthBb
{
    /// <inheritdoc />
    public partial class InitialMigrationforAuth1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "681c0e0a-21ab-4469-b104-bb58061ca776", null, "df3386ff-8766-4456-9bc1-d1ed83e82d6f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80c839f1-69a5-415f-aa60-aeedaba19c54", "AQAAAAIAAYagAAAAEJhhcf+/UTFvN6LrpWO5bHDqRkSRImsvzpXeb3Nxk6kSgorHF5Qk7OdgMOY2fssl/w==", "8a8ba714-3962-4788-9a2e-efbae7becf30" });
        }
    }
}
