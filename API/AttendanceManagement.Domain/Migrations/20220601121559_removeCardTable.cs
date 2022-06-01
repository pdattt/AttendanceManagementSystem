using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Domain.Migrations
{
    public partial class removeCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "CardId",
                table: "Attendees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Attendees");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendeeID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Attendees_AttendeeID",
                        column: x => x.AttendeeID,
                        principalTable: "Attendees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AttendeeID",
                table: "Cards",
                column: "AttendeeID");
        }
    }
}
