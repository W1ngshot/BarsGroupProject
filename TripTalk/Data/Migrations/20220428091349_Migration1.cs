using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_article_id",
                table: "tag");

            migrationBuilder.DropIndex(
                name: "ix_tag_article_id",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "password",
                table: "user");

            migrationBuilder.DropColumn(
                name: "article_id",
                table: "tag");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "hash");

            migrationBuilder.AddColumn<string>(
                name: "password_salt",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "salt");

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "article",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "views",
                table: "article",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "ak_name",
                table: "tag",
                column: "name");

            migrationBuilder.CreateTable(
                name: "article_tag",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    article_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_tag", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_tag_article_article_id",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_tag_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_tag_article_id",
                table: "article_tag",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_article_tag_tag_id",
                table: "article_tag",
                column: "tag_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_tag");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_name",
                table: "tag");

            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "user");

            migrationBuilder.DropColumn(
                name: "password_salt",
                table: "user");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "article");

            migrationBuilder.DropColumn(
                name: "views",
                table: "article");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "article_id",
                table: "tag",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_tag_article_id",
                table: "tag",
                column: "article_id");

            migrationBuilder.AddForeignKey(
                name: "fk_article_id",
                table: "tag",
                column: "article_id",
                principalTable: "article",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
