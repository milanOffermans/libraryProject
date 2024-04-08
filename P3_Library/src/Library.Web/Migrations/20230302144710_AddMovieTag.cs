using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                _ = migrationBuilder.CreateTable(
                    name: "MovieTag",
                    columns: table => new
                    {
                        MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                    },
                    constraints: table =>
                    {
                        _ = table.PrimaryKey("PK_MovieTag", x => new { x.MovieId, x.TagId });
                        _ = table.ForeignKey(
                            name: "FK_MovieTag_Movie_MovieId",
                            column: x => x.MovieId,
                            principalTable: "Movie",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        _ = table.ForeignKey(
                            name: "FK_MovieTag_Tag_TagId",
                            column: x => x.TagId,
                            principalTable: "Tag",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                _ = migrationBuilder.CreateIndex(
                    name: "IX_MovieTag_TagId",
                    table: "MovieTag",
                    column: "TagId");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                _ = migrationBuilder.DropTable(
                    name: "MovieTag");
            }
        }
    }
}
