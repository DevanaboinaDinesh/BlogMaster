using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthBb
{
    /// <inheritdoc />
    public partial class onc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "11c078f0-e356-42e1-8117-db6f94955f29", "6e8d67dd-112c-484f-a471-9f7ceeba8bc7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e003a301-4e7e-4fc3-8cfd-42410d40670e",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "681c0e0a-21ab-4469-b104-bb58061ca776", "df3386ff-8766-4456-9bc1-d1ed83e82d6f" });
        }
    }
}
