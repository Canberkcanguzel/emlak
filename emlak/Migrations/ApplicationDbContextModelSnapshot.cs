﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using emlak.Repository;

#nullable disable

namespace emlak.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("emlak.Models.ContactRequests", b =>
                {
                    b.Property<int>("request_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("request_id"));

                    b.Property<string>("contactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contactPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdAt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pro_ID")
                        .HasColumnType("int");

                    b.Property<int>("user_ID")
                        .HasColumnType("int");

                    b.HasKey("request_id");

                    b.HasIndex("pro_ID");

                    b.HasIndex("user_ID");

                    b.ToTable("ContactRequest");
                });

            modelBuilder.Entity("emlak.Models.Properties", b =>
                {
                    b.Property<int>("pro_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pro_id"));

                    b.Property<decimal>("area")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("bedrooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("propertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("user_ID")
                        .HasColumnType("int");

                    b.HasKey("pro_id");

                    b.HasIndex("user_ID");

                    b.ToTable("Propertie");
                });

            modelBuilder.Entity("emlak.Models.PropertyImages", b =>
                {
                    b.Property<int>("image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("image_id"));

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<int?>("Usersuser_id")
                        .HasColumnType("int");

                    b.Property<byte[]>("imageURL")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("pro_ID")
                        .HasColumnType("int");

                    b.HasKey("image_id");

                    b.HasIndex("Usersuser_id");

                    b.HasIndex("pro_ID");

                    b.ToTable("PropertyImage");
                });

            modelBuilder.Entity("emlak.Models.user.Users", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("emlak.Models.ContactRequests", b =>
                {
                    b.HasOne("emlak.Models.Properties", "Properties")
                        .WithMany()
                        .HasForeignKey("pro_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("emlak.Models.user.Users", "Users")
                        .WithMany("ContactRequests")
                        .HasForeignKey("user_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Properties");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("emlak.Models.Properties", b =>
                {
                    b.HasOne("emlak.Models.user.Users", "Users")
                        .WithMany("Properties")
                        .HasForeignKey("user_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("emlak.Models.PropertyImages", b =>
                {
                    b.HasOne("emlak.Models.user.Users", null)
                        .WithMany("PropertyImages")
                        .HasForeignKey("Usersuser_id");

                    b.HasOne("emlak.Models.Properties", "Properties")
                        .WithMany()
                        .HasForeignKey("pro_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("emlak.Models.user.Users", b =>
                {
                    b.Navigation("ContactRequests");

                    b.Navigation("Properties");

                    b.Navigation("PropertyImages");
                });
#pragma warning restore 612, 618
        }
    }
}
