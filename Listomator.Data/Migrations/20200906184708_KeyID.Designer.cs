﻿// <auto-generated />
using System;
using Listomator.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Listomator.Data.Migrations
{
    [DbContext(typeof(ListomatorContext))]
    [Migration("20200906184708_KeyID")]
    partial class KeyID
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("Listomator.Data.Model.ToDoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ToDoGroupName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDoGroups");
                });

            modelBuilder.Entity("Listomator.Data.Model.ToDoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ToDoGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ToDoItemName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseDueDate")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ToDoGroupId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("Listomator.Data.Model.ToDoItem", b =>
                {
                    b.HasOne("Listomator.Data.Model.ToDoGroup", "ToDoGroup")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}