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
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

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
                name: "languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "game_categories",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_categories", x => new { x.category_id, x.game_id });
                    table.ForeignKey(
                        name: "FK_game_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_categories_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_languages",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_languages", x => new { x.game_id, x.language_id });
                    table.ForeignKey(
                        name: "FK_game_languages_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_languages_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_tags",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "int", nullable: false),
                    tag_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_tags", x => new { x.game_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_game_tags_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_categories_game_id",
                table: "game_categories",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_languages_language_id",
                table: "game_languages",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_tags_tag_id",
                table: "game_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_games_developer_id",
                table: "games",
                column: "developer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_categories");

            migrationBuilder.DropTable(
                name: "game_languages");

            migrationBuilder.DropTable(
                name: "game_tags");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "developers");
        }
    }
}
