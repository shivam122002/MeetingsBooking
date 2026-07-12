using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingsBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Meetings");
        }
    }
}
