using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Movie",
                type: "varbinary(max)",
                maxLength: 1048576,
                nullable: false,
                defaultValue: new byte[0]);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movie");
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}