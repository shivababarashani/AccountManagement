using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Persistence.Migrations
{
    public partial class RemoveTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(1092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7199),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(1040));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1608),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6030));

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "2/5/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "1/27/2023");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(5856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4692),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(8063));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7999));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4244),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7679));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6499),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(599));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6961));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2420),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6902));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(1092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "WithdrawAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(1040),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(7199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6030),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1608));

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "1/27/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "2/5/2023");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(5856),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(1463));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(8063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7999),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(7600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(657),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6499));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockAccountTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 342, DateTimeKind.Local).AddTicks(599),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(6902),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 20, 17, 888, DateTimeKind.Local).AddTicks(2420));

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountBlockingDate = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    AccountBlockingStatusCode = table.Column<int>(type: "int", nullable: true),
                    AccountBlockingStatusDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountBlockingTraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(9557)),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 27, 20, 39, 0, 341, DateTimeKind.Local).AddTicks(9703)),
                    WithdrawBlockingDate = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    WithdrawBlockingStatusCode = table.Column<int>(type: "int", nullable: true),
                    WithdrawBlockingStatusDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WithdrawBlockingTraceNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }
    }
}
