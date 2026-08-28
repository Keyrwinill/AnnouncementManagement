using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnnouncementManagement.Migrations
{
    /// <inheritdoc />
    public partial class RenameIsVisibleToIsPublish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisible",
                table: "Announcements",
                newName: "IsPublish");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublish",
                table: "Announcements",
                newName: "IsVisible");
        }
    }
}
