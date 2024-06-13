using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoaHoc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Three : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentBlogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(123)",
                oldMaxLength: 123);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "CommentBlogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CommentBlogs_ParentId",
                table: "CommentBlogs",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentBlogs_CommentBlogs_ParentId",
                table: "CommentBlogs",
                column: "ParentId",
                principalTable: "CommentBlogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentBlogs_CommentBlogs_ParentId",
                table: "CommentBlogs");

            migrationBuilder.DropIndex(
                name: "IX_CommentBlogs_ParentId",
                table: "CommentBlogs");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "CommentBlogs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CommentBlogs",
                type: "nvarchar(123)",
                maxLength: 123,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
