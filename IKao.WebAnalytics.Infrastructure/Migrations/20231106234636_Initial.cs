using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IKao.WebAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "developers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_developers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    seo_description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    instruction_description = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    developer_id = table.Column<int>(type: "int", nullable: false),
                    age_rating = table.Column<int>(type: "int", nullable: false),
                    rating_value = table.Column<int>(type: "int", nullable: false),
                    rating_count = table.Column<int>(type: "int", nullable: false),
                    counter_players = table.Column<int>(type: "int", nullable: false),
                    media_cover = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    media_icon = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    play_link = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    publish_date = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    creation_date = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    modification_date = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    deletion_date = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    published_date = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.id);
                    table.ForeignKey(
                        name: "FK_games_developers_developer_id",
                        column: x => x.developer_id,
                        principalTable: "developers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_developer_id",
                table: "games",
                column: "developer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "developers");
        }
    }
}
