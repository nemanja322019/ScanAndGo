using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoreToShoppingCartEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_users_UserId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_products_ProductId",
                table: "ShoppingCartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItem",
                table: "ShoppingCartItem");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                newName: "shoppingCart");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItem",
                newName: "shoppingCartItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_UserId",
                table: "shoppingCart",
                newName: "IX_shoppingCart_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItem_ShoppingCartId",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItem_ProductId",
                table: "shoppingCartItems",
                newName: "IX_shoppingCartItems_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "shoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_shoppingCart",
                table: "shoppingCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shoppingCartItems",
                table: "shoppingCartItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCart_StoreId",
                table: "shoppingCart",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCart_stores_StoreId",
                table: "shoppingCart",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCart_users_UserId",
                table: "shoppingCart",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_products_ProductId",
                table: "shoppingCartItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_shoppingCart_ShoppingCartId",
                table: "shoppingCartItems",
                column: "ShoppingCartId",
                principalTable: "shoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCart_stores_StoreId",
                table: "shoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCart_users_UserId",
                table: "shoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_products_ProductId",
                table: "shoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_shoppingCart_ShoppingCartId",
                table: "shoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shoppingCart",
                table: "shoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_shoppingCart_StoreId",
                table: "shoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shoppingCartItems",
                table: "shoppingCartItems");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "shoppingCart");

            migrationBuilder.RenameTable(
                name: "shoppingCart",
                newName: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "shoppingCartItems",
                newName: "ShoppingCartItem");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCart_UserId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItem",
                newName: "IX_ShoppingCartItem_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_shoppingCartItems_ProductId",
                table: "ShoppingCartItem",
                newName: "IX_ShoppingCartItem_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItem",
                table: "ShoppingCartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_users_UserId",
                table: "ShoppingCart",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartItem",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_products_ProductId",
                table: "ShoppingCartItem",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
