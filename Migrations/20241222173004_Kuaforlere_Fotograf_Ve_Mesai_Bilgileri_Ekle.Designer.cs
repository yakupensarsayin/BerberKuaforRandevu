﻿// <auto-generated />
using System;
using BerberKuaforRandevu.Veritabani;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BerberKuaforRandevu.Migrations
{
    [DbContext(typeof(KuaforVeritabani))]
    [Migration("20241222173004_Kuaforlere_Fotograf_Ve_Mesai_Bilgileri_Ekle")]
    partial class Kuaforlere_Fotograf_Ve_Mesai_Bilgileri_Ekle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BerberKuaforRandevu.Models.Kuafor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Fotograf")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("KullaniciId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("MesaiBaslangic")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("MesaiBitis")
                        .HasColumnType("time");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("SalonId");

                    b.ToTable("Kuaforler");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.KuaforUzmanlik", b =>
                {
                    b.Property<int>("KuaforId")
                        .HasColumnType("int");

                    b.Property<int>("YetenekId")
                        .HasColumnType("int");

                    b.HasKey("KuaforId", "YetenekId");

                    b.HasIndex("YetenekId");

                    b.ToTable("KuaforlerUzmanliklar");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.KuaforYetenek", b =>
                {
                    b.Property<int>("KuaforId")
                        .HasColumnType("int");

                    b.Property<int>("YetenekId")
                        .HasColumnType("int");

                    b.HasKey("KuaforId", "YetenekId");

                    b.HasIndex("YetenekId");

                    b.ToTable("KuaforlerYetenekler");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Kullanici", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Salon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SalonTuruId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("SalonTuruId");

                    b.ToTable("Salonlar");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.SalonTuru", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SalonTurleri");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Yetenek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Fiyat")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<int>("Sure")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalonId");

                    b.ToTable("Yetenekler");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Kuafor", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerberKuaforRandevu.Models.Salon", "Salon")
                        .WithMany("Kuaforler")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.KuaforUzmanlik", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kuafor", "Kuafor")
                        .WithMany("KuaforUzmanliklar")
                        .HasForeignKey("KuaforId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerberKuaforRandevu.Models.Yetenek", "Yetenek")
                        .WithMany("KuaforUzmanliklar")
                        .HasForeignKey("YetenekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kuafor");

                    b.Navigation("Yetenek");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.KuaforYetenek", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kuafor", "Kuafor")
                        .WithMany("KuaforYetenekler")
                        .HasForeignKey("KuaforId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerberKuaforRandevu.Models.Yetenek", "Yetenek")
                        .WithMany("KuaforYetenekler")
                        .HasForeignKey("YetenekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kuafor");

                    b.Navigation("Yetenek");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Salon", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerberKuaforRandevu.Models.SalonTuru", "SalonTuru")
                        .WithMany("Salonlar")
                        .HasForeignKey("SalonTuruId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("SalonTuru");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Yetenek", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Salon", "Salon")
                        .WithMany()
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BerberKuaforRandevu.Models.Kullanici", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Kuafor", b =>
                {
                    b.Navigation("KuaforUzmanliklar");

                    b.Navigation("KuaforYetenekler");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Salon", b =>
                {
                    b.Navigation("Kuaforler");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.SalonTuru", b =>
                {
                    b.Navigation("Salonlar");
                });

            modelBuilder.Entity("BerberKuaforRandevu.Models.Yetenek", b =>
                {
                    b.Navigation("KuaforUzmanliklar");

                    b.Navigation("KuaforYetenekler");
                });
#pragma warning restore 612, 618
        }
    }
}
