using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RswebProject.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPHouse_Book_BookId",
                table: "BookPHouse");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPHouse_PHouse_PHouseId",
                table: "BookPHouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPHouse",
                table: "BookPHouse");

            migrationBuilder.RenameTable(
                name: "BookPHouse",
                newName: "BookPHouses");

            migrationBuilder.RenameIndex(
                name: "IX_BookPHouse_PHouseId",
                table: "BookPHouses",
                newName: "IX_BookPHouses_PHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPHouse_BookId",
                table: "BookPHouses",
                newName: "IX_BookPHouses_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPHouses",
                table: "BookPHouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPHouses_Book_BookId",
                table: "BookPHouses",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPHouses_PHouse_PHouseId",
                table: "BookPHouses",
                column: "PHouseId",
                principalTable: "PHouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPHouses_Book_BookId",
                table: "BookPHouses");

            migrationBuilder.DropForeignKey(
                name: "FK_BookPHouses_PHouse_PHouseId",
                table: "BookPHouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPHouses",
                table: "BookPHouses");

            migrationBuilder.RenameTable(
                name: "BookPHouses",
                newName: "BookPHouse");

            migrationBuilder.RenameIndex(
                name: "IX_BookPHouses_PHouseId",
                table: "BookPHouse",
                newName: "IX_BookPHouse_PHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPHouses_BookId",
                table: "BookPHouse",
                newName: "IX_BookPHouse_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPHouse",
                table: "BookPHouse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPHouse_Book_BookId",
                table: "BookPHouse",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPHouse_PHouse_PHouseId",
                table: "BookPHouse",
                column: "PHouseId",
                principalTable: "PHouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
