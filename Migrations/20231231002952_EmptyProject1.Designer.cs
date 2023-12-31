﻿// <auto-generated />
using EmptyProject.Areas.BasicCore.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmptyProject.Migrations
{
    [DbContext(typeof(EFCoreContext))]
    [Migration("20231231002952_EmptyProject1")]
    partial class EmptyProject1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmptyProject.Areas.CMSCore.Entities.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<byte>("Active")
                        .HasColumnType("tinyint");

                    b.Property<string>("IconURLPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuFatherId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("URLPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MenuId");

                    b.ToTable("Menu");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 0,
                            Name = "BasicCore",
                            Order = 100,
                            URLPath = ""
                        },
                        new
                        {
                            MenuId = 2,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 1,
                            Name = "Failure",
                            Order = 0,
                            URLPath = "/BasicCore/Failure"
                        },
                        new
                        {
                            MenuId = 3,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 1,
                            Name = "Parameter",
                            Order = 0,
                            URLPath = "/BasicCore/Parameter"
                        },
                        new
                        {
                            MenuId = 4,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 0,
                            Name = "BasicCulture",
                            Order = 200,
                            URLPath = ""
                        },
                        new
                        {
                            MenuId = 5,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 4,
                            Name = "City",
                            Order = 0,
                            URLPath = "/BasicCulture/City"
                        },
                        new
                        {
                            MenuId = 6,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 4,
                            Name = "State",
                            Order = 0,
                            URLPath = "/BasicCulture/State"
                        },
                        new
                        {
                            MenuId = 7,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 4,
                            Name = "Country",
                            Order = 0,
                            URLPath = "/BasicCulture/Country"
                        },
                        new
                        {
                            MenuId = 8,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 4,
                            Name = "Planet",
                            Order = 0,
                            URLPath = "/BasicCulture/Planet"
                        },
                        new
                        {
                            MenuId = 9,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 4,
                            Name = "Sex",
                            Order = 0,
                            URLPath = "/BasicCulture/Sex"
                        },
                        new
                        {
                            MenuId = 10,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 0,
                            Name = "CMSCore",
                            Order = 300,
                            URLPath = ""
                        },
                        new
                        {
                            MenuId = 11,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 10,
                            Name = "User",
                            Order = 0,
                            URLPath = "/CMSCore/User"
                        },
                        new
                        {
                            MenuId = 12,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 10,
                            Name = "Role",
                            Order = 0,
                            URLPath = "/CMSCore/Role"
                        },
                        new
                        {
                            MenuId = 13,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 10,
                            Name = "Menu",
                            Order = 0,
                            URLPath = "/CMSCore/Menu"
                        },
                        new
                        {
                            MenuId = 14,
                            Active = (byte)1,
                            IconURLPath = "",
                            MenuFatherId = 10,
                            Name = "Permission",
                            Order = 0,
                            URLPath = "/CMSCore/Permission"
                        });
                });

            modelBuilder.Entity("EmptyProject.Areas.CMSCore.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("EmptyProject.Areas.CMSCore.Entities.RoleMenu", b =>
                {
                    b.Property<int>("RoleMenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleMenuId"));

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RoleMenuId");

                    b.ToTable("RoleMenu");

                    b.HasData(
                        new
                        {
                            RoleMenuId = 1,
                            MenuId = 10,
                            RoleId = 1
                        },
                        new
                        {
                            RoleMenuId = 2,
                            MenuId = 14,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("EmptyProject.Areas.CMSCore.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(380)
                        .HasColumnType("nvarchar(380)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "novillo.matias1@gmail.com",
                            Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw=="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}