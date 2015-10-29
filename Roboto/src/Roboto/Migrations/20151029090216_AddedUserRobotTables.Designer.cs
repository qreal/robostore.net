using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Roboto.Models;

namespace Roboto.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20151029090216_AddedUserRobotTables")]
    partial class AddedUserRobotTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Roboto.Models.Entities.Robot", b =>
                {
                    b.Property<int>("RobotID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ModelConfig")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Program")
                        .IsRequired();

                    b.Property<string>("SSID")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("SystemConfig")
                        .IsRequired();

                    b.Property<int>("UserID");

                    b.HasKey("RobotID");
                });

            modelBuilder.Entity("Roboto.Models.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserID");
                });
        }
    }
}
