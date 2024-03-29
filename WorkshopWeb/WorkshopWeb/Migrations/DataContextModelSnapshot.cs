﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkshopWeb.Data;

namespace WorkshopWeb.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AddressWorkshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("CityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("AddressWorkshops");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AppointmentAndService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppointmentServiceId");

                    b.Property<int?>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("AppointmentAndServices");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AppointmentService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressWorkshopId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<int>("ModelCarId");

                    b.Property<string>("RegistrationPlate");

                    b.Property<string>("State");

                    b.Property<string>("UserId");

                    b.Property<int?>("UserNoRegistryId");

                    b.HasKey("Id");

                    b.HasIndex("AddressWorkshopId");

                    b.HasIndex("ModelCarId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserNoRegistryId");

                    b.ToTable("AppointmentServices");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.BrandCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("BrandCars");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ModelCarId");

                    b.Property<string>("RegistrationPlate")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.Property<int?>("UserNoRegistryId");

                    b.HasKey("Id");

                    b.HasIndex("ModelCarId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserNoRegistryId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.ModelCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandCarId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BrandCarId");

                    b.ToTable("ModelCars");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Reparation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppointmentId");

                    b.Property<int?>("BrandCarId");

                    b.Property<int?>("CarId");

                    b.Property<DateTime>("DateCreate");

                    b.Property<string>("Observation");

                    b.Property<string>("State");

                    b.Property<string>("UserEmployeeId");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("BrandCarId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserEmployeeId");

                    b.ToTable("Reparations");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.ServiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ReparationId");

                    b.Property<int?>("ServiceId");

                    b.Property<bool>("State");

                    b.HasKey("Id");

                    b.HasIndex("ReparationId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceDetails");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<int>("CityId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nif");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.UserNoRegistry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("UserNoRegistrys");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkshopWeb.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AddressWorkshop", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AppointmentAndService", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.AppointmentService")
                        .WithMany("Services")
                        .HasForeignKey("AppointmentServiceId");

                    b.HasOne("WorkshopWeb.Data.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.AppointmentService", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.AddressWorkshop", "AddressWorkshop")
                        .WithMany()
                        .HasForeignKey("AddressWorkshopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkshopWeb.Data.Entities.ModelCar", "ModelCar")
                        .WithMany()
                        .HasForeignKey("ModelCarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkshopWeb.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("WorkshopWeb.Data.Entities.UserNoRegistry", "UserNoRegistry")
                        .WithMany()
                        .HasForeignKey("UserNoRegistryId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Car", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.ModelCar", "ModelCar")
                        .WithMany()
                        .HasForeignKey("ModelCarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorkshopWeb.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("WorkshopWeb.Data.Entities.UserNoRegistry", "UserNoRegistry")
                        .WithMany()
                        .HasForeignKey("UserNoRegistryId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.City", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.ModelCar", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.BrandCar")
                        .WithMany("Model")
                        .HasForeignKey("BrandCarId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.Reparation", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.AppointmentService", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId");

                    b.HasOne("WorkshopWeb.Data.Entities.BrandCar", "BrandCar")
                        .WithMany()
                        .HasForeignKey("BrandCarId");

                    b.HasOne("WorkshopWeb.Data.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("WorkshopWeb.Data.Entities.User", "UserEmployee")
                        .WithMany()
                        .HasForeignKey("UserEmployeeId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.ServiceDetail", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.Reparation")
                        .WithMany("ServiceDetails")
                        .HasForeignKey("ReparationId");

                    b.HasOne("WorkshopWeb.Data.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("WorkshopWeb.Data.Entities.User", b =>
                {
                    b.HasOne("WorkshopWeb.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
