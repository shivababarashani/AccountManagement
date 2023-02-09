using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Persistence.Migrations
{
    public partial class ChangeBlockAccountTransactionToBlockDepositTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockAccountTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(595),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1463));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2636),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2240),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2420));

            migrationBuilder.CreateTable(
                name: "BlockDepositTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Date = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4339)),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4439)),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockDepositTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockDepositTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockDepositTransactions_AccountId",
                table: "BlockDepositTransactions",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockDepositTransactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7199),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(4817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1608),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(595));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4692),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4244),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(2240));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2420),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 35, 40, 244, DateTimeKind.Local).AddTicks(1493));

            migrationBuilder.CreateTable(
                name: "BlockAccountTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6419)),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6499)),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockAccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockAccountTransactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockAccountTransactions_AccountId",
                table: "BlockAccountTransactions",
                column: "AccountId",
                unique: true);
        }
    }
}
