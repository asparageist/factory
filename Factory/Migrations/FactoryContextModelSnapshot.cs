﻿// <auto-generated />
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Factory.Migrations
{
    [DbContext(typeof(FactoryContext))]
    partial class FactoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Property<int>("EngineerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EngineerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EngineerID");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("Factory.Models.EngineerMachine", b =>
                {
                    b.Property<int>("EngineerMachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EngineerID")
                        .HasColumnType("int");

                    b.Property<int>("MachineID")
                        .HasColumnType("int");

                    b.HasKey("EngineerMachineId");

                    b.HasIndex("EngineerID");

                    b.HasIndex("MachineID");

                    b.ToTable("EngineerMachines");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Property<int>("MachineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MachineID");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Factory.Models.EngineerMachine", b =>
                {
                    b.HasOne("Factory.Models.Engineer", "Engineer")
                        .WithMany("JoinEntities")
                        .HasForeignKey("EngineerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Factory.Models.Machine", "Machine")
                        .WithMany("JoinEntities")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineer");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Factory.Models.Engineer", b =>
                {
                    b.Navigation("JoinEntities");
                });

            modelBuilder.Entity("Factory.Models.Machine", b =>
                {
                    b.Navigation("JoinEntities");
                });
#pragma warning restore 612, 618
        }
    }
}