using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SendGrid_Log.Models;

namespace SendGrid_Log.Migrations
{
    [DbContext(typeof(SendGrid_LogContext))]
    [Migration("20170724114501_events")]
    partial class events
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SendGrid_Log.Models.EmailEvent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("asm_group_id");

                    b.Property<string>("attempt");

                    b.Property<string>("cert_err");

                    b.Property<string>("email");

                    b.Property<string>("event");

                    b.Property<DateTime>("eventSend_at");

                    b.Property<DateTime>("eventTimestamp");

                    b.Property<string>("ip");

                    b.Property<string>("reason");

                    b.Property<string>("response");

                    b.Property<string>("sg_event_id");

                    b.Property<string>("sg_message_id");

                    b.Property<string>("tls");

                    b.Property<string>("type");

                    b.Property<string>("url");

                    b.Property<string>("useragent");

                    b.Property<string>("userid");

                    b.HasKey("ID");

                    b.ToTable("EmailEvent");
                });
        }
    }
}
