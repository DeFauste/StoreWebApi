using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.DataAccess.Postgress.Migrations
{
    /// <inheritdoc />
    public partial class fixkeyproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Images_ImageId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ImageId",
                table: "Product");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Product",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                table: "Product",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Images_ImageId",
                table: "Product",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }
    }
}
