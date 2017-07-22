using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SendGrid_Log.Migrations
{
    public partial class Sendat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Send_at",
                table: "EmailEvent");

            migrationBuilder.AddColumn<DateTime>(
                name: "eventSend_at",
                table: "EmailEvent",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eventSend_at",
                table: "EmailEvent");

            migrationBuilder.AddColumn<string>(
                name: "Send_at",
                table: "EmailEvent",
                nullable: true);
        }
    }
}
