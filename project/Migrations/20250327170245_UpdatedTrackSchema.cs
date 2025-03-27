using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTrackSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.RenameTable(
                name: "Tracks",
                newName: "tracks");

            migrationBuilder.RenameIndex(
                name: "IX_Tracks_AlbumId",
                table: "tracks",
                newName: "IX_tracks_AlbumId");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeId",
                table: "tracks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bytes",
                table: "tracks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Milliseconds",
                table: "tracks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "tracks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tracks",
                table: "tracks",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_tracks_albums_AlbumId",
                table: "tracks",
                column: "AlbumId",
                principalTable: "albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tracks_albums_AlbumId",
                table: "tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tracks",
                table: "tracks");

            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "tracks");

            migrationBuilder.DropColumn(
                name: "Milliseconds",
                table: "tracks");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "tracks");

            migrationBuilder.RenameTable(
                name: "tracks",
                newName: "Tracks");

            migrationBuilder.RenameIndex(
                name: "IX_tracks_AlbumId",
                table: "Tracks",
                newName: "IX_Tracks_AlbumId");

            migrationBuilder.AlterColumn<int>(
                name: "MediaTypeId",
                table: "Tracks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
