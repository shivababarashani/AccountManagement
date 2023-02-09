using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountManagement.Persistence.Migrations
{
    public partial class FillCreateDateAndModifyDateFilds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4085));

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "2/6/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "2/5/2023");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(3951));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(6025));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5663));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockWithdrawTransactions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8081));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockWithdrawTransactions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7643));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4871));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "2/5/2023",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "2/6/2023");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Letters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(3951),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(6025),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DemandPackets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5663),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(5598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockWithdrawTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockWithdrawTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(8011),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7643),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BlockDepositTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(7546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4928),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 5, 3, 46, 0, 912, DateTimeKind.Local).AddTicks(4871),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
