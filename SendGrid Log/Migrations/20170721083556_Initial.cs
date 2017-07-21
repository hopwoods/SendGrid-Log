using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SendGrid_Log.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailEvent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ASMGroupID = table.Column<string>(nullable: true),
                    Attempt = table.Column<string>(nullable: true),
                    Categories = table.Column<string>(nullable: true),
                    Cert_Err = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    EventID = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    MarketingCampaignID = table.Column<string>(nullable: true),
                    MarketingCampaignName = table.Column<string>(nullable: true),
                    MarketingCampaignSplitID = table.Column<string>(nullable: true),
                    MarketingCampaignVersion = table.Column<string>(nullable: true),
                    MessageID = table.Column<string>(nullable: true),
                    Newsletter = table.Column<string>(nullable: true),
                    PostType = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    SmtpID = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TLS = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    URLOffset = table.Column<string>(nullable: true),
                    UniqueArgKey = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true)
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
