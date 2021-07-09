using Microsoft.EntityFrameworkCore.Migrations;

namespace Hello.BookStore.Migrations
{
    public partial class AddedReviewBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookReviewDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewBook", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewBook");
        }
    }
}
