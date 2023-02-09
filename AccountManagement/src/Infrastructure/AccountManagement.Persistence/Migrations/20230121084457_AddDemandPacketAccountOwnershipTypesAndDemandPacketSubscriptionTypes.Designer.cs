﻿// <auto-generated />
using System;
using AccountManagement.Persistence.Implimentation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountManagement.Persistence.Migrations
{
    [DbContext(typeof(AccountManagementDbContext))]
    [Migration("20230121084457_AddDemandPacketAccountOwnershipTypesAndDemandPacketSubscriptionTypes")]
    partial class AddDemandPacketAccountOwnershipTypesAndDemandPacketSubscriptionTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountManagement.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("ActualAmount")
                        .HasColumnType("float");

                    b.Property<double>("AvailableAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 718, DateTimeKind.Local).AddTicks(9415));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DemandPacketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 718, DateTimeKind.Local).AddTicks(9474));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DemandPacketId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.AccountOwnershipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountOwnershipType");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.BlockAccountTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(2523));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(2573));

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TraceNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("BlockAccountTransactions");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.BlockUnblockReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BlockUnblockReasons");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(86));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(160));

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BlockUnblockPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BlockUnblockReasonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(462));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DemandStatusId")
                        .HasColumnType("int");

                    b.Property<Guid>("LetterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(524));

                    b.Property<int>("SwiftTypeId")
                        .HasColumnType("int");

                    b.Property<Guid>("TraceCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlockUnblockReasonId");

                    b.HasIndex("DemandStatusId");

                    b.HasIndex("LetterId");

                    b.HasIndex("SwiftTypeId");

                    b.ToTable("DemandPackets");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacketAccountOwnershipType", b =>
                {
                    b.Property<int>("AccountOwnershipTypeId")
                        .HasColumnType("int");

                    b.Property<Guid>("DemandPacketId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountOwnershipTypeId", "DemandPacketId");

                    b.HasIndex("DemandPacketId");

                    b.ToTable("DemandPacketAccountOwnershipTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacketSubscriptionType", b =>
                {
                    b.Property<int>("SubscriptionTypeId")
                        .HasColumnType("int");

                    b.Property<Guid>("DemandPacketId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SubscriptionTypeId", "DemandPacketId");

                    b.HasIndex("DemandPacketId");

                    b.ToTable("DemandPacketSubscriptionTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DemandStatus");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Letter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContextImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 718, DateTimeKind.Local).AddTicks(8473));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("1/21/2023");

                    b.Property<string>("Deadline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LetterTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 718, DateTimeKind.Local).AddTicks(8644));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ReceiptDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TrackingCode")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LetterTypeId");

                    b.ToTable("Letters");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.LetterType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LetterType");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.SubscriptionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.SwiftType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SwiftTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountBlockingDate")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("AccountBlockingStatusCode")
                        .HasColumnType("int");

                    b.Property<string>("AccountBlockingStatusDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("AccountBlockingTraceNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(1682));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(1765));

                    b.Property<string>("WithdrawBlockingDate")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("WithdrawBlockingStatusCode")
                        .HasColumnType("int");

                    b.Property<string>("WithdrawBlockingStatusDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("WithdrawBlockingTraceNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.WithdrawAccountTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(3360));

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 21, 0, 44, 57, 719, DateTimeKind.Local).AddTicks(3438));

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TraceNumber")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("WithdrawAccountTransactions");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Account", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.DemandPacket", "DemandPacket")
                        .WithMany("Accounts")
                        .HasForeignKey("DemandPacketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DemandPacket");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.BlockAccountTransaction", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.Account", "Account")
                        .WithMany("BlockAccountTransactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Customer", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.Account", "Account")
                        .WithMany("Customers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacket", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.BlockUnblockReason", "BlockUnblockReason")
                        .WithMany("DemandPackets")
                        .HasForeignKey("BlockUnblockReasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountManagement.Domain.Entities.DemandStatus", "DemandStatus")
                        .WithMany("DemandPackets")
                        .HasForeignKey("DemandStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountManagement.Domain.Entities.Letter", "Letter")
                        .WithMany("DemandPackets")
                        .HasForeignKey("LetterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountManagement.Domain.Entities.SwiftType", "SwiftType")
                        .WithMany("DemandPackets")
                        .HasForeignKey("SwiftTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlockUnblockReason");

                    b.Navigation("DemandStatus");

                    b.Navigation("Letter");

                    b.Navigation("SwiftType");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacketAccountOwnershipType", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.AccountOwnershipType", "AccountOwnershipType")
                        .WithMany("DemandPacketAccountOwnershipTypes")
                        .HasForeignKey("AccountOwnershipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountManagement.Domain.Entities.DemandPacket", "DemandPacket")
                        .WithMany("DemandPacketAccountOwnershipTypes")
                        .HasForeignKey("DemandPacketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountOwnershipType");

                    b.Navigation("DemandPacket");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacketSubscriptionType", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.DemandPacket", "DemandPacket")
                        .WithMany("DemandPacketSubscriptionTypes")
                        .HasForeignKey("DemandPacketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountManagement.Domain.Entities.SubscriptionType", "SubscriptionType")
                        .WithMany("DemandPacketSubscriptionTypes")
                        .HasForeignKey("SubscriptionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DemandPacket");

                    b.Navigation("SubscriptionType");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Letter", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.LetterType", "LetterType")
                        .WithMany("Letters")
                        .HasForeignKey("LetterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LetterType");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.WithdrawAccountTransaction", b =>
                {
                    b.HasOne("AccountManagement.Domain.Entities.Account", "Account")
                        .WithMany("WithdrawAccountTransactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Account", b =>
                {
                    b.Navigation("BlockAccountTransactions");

                    b.Navigation("Customers");

                    b.Navigation("WithdrawAccountTransactions");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.AccountOwnershipType", b =>
                {
                    b.Navigation("DemandPacketAccountOwnershipTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.BlockUnblockReason", b =>
                {
                    b.Navigation("DemandPackets");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandPacket", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("DemandPacketAccountOwnershipTypes");

                    b.Navigation("DemandPacketSubscriptionTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.DemandStatus", b =>
                {
                    b.Navigation("DemandPackets");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.Letter", b =>
                {
                    b.Navigation("DemandPackets");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.LetterType", b =>
                {
                    b.Navigation("Letters");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.SubscriptionType", b =>
                {
                    b.Navigation("DemandPacketSubscriptionTypes");
                });

            modelBuilder.Entity("AccountManagement.Domain.Entities.SwiftType", b =>
                {
                    b.Navigation("DemandPackets");
                });
#pragma warning restore 612, 618
        }
    }
}