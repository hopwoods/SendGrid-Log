using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SendGrid_Log.Migrations
{
    public partial class events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailEvent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    asm_group_id = table.Column<int>(nullable: false),
                    attempt = table.Column<string>(nullable: true),
                    cert_err = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    @event = table.Column<string>(name: "event", nullable: true),
                    eventSend_at = table.Column<DateTime>(nullable: false),
                    eventTimestamp = table.Column<DateTime>(nullable: false),
                    ip = table.Column<string>(nullable: true),
                    reason = table.Column<string>(nullable: true),
                    response = table.Column<string>(nullable: true),
                    sg_event_id = table.Column<string>(nullable: true),
                    sg_message_id = table.Column<string>(nullable: true),
                    tls = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    useragent = table.Column<string>(nullable: true),
                    userid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailEvent", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailEvent");
        }
    }
}
