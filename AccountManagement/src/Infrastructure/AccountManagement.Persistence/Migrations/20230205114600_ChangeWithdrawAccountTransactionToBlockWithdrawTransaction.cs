using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Persistence.Migrations
{
    public partial class ChangeWithdrawAccountTransactionToBlockWithdrawTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WithdrawAccountTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(595));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(3951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(6025),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5663),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2240));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4928),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4871),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1493));

            migrationBuilder.CreateTable(
                name: "BlockWithdrawTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8011)),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8081)),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockWithdrawTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockWithdrawTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockWithdrawTransactions_AccountId",
                table: "BlockWithdrawTransactions",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockWithdrawTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(595),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4085));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(3951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(6025));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5663));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4339),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4871));

            migrationBuilder.CreateTable(
                name: "WithdrawAccountTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4817)),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4890)),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawAccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawAccountTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawAccountTransactions_AccountId",
                table: "WithdrawAccountTransactions",
                column: "AccountId",
                unique: true);
        }
    }
}
