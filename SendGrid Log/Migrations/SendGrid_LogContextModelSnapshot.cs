using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SendGrid_Log.Models;

namespace SendGrid_Log.Migrations
{
    [DbContext(typeof(SendGrid_LogContext))]
    partial class SendGrid_LogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SendGrid_Log.Models.EmailEvent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ASMGroupID");

                    b.Property<string>("Attempt");

                    b.Property<string>("Categories");

                    b.Property<string>("Cert_Err");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("EventID");

                    b.Property<string>("EventType");

                    b.Property<string>("IP");

                    b.Property<string>("MarketingCampaignID");

                    b.Property<string>("MarketingCampaignName");

                    b.Property<string>("MarketingCampaignSplitID");

                    b.Property<string>("MarketingCampaignVersion");

                    b.Property<string>("MessageID");

                    b.Property<string>("Newsletter");

                    b.Property<string>("PostType");

                    b.Property<string>("Reason");

                    b.Property<string>("Response");

                    b.Property<string>("SmtpID");

                    b.Property<string>("Status");

                    b.Property<string>("TLS");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Type");

                    b.Property<string>("URL");

                    b.Property<string>("URLOffset");

                    b.Property<string>("UniqueArgKey");

                    b.Property<string>("UserAgent");

                    b.HasKey("ID");

                    b.ToTable("EmailEvent");
                });
        }
    }
}
