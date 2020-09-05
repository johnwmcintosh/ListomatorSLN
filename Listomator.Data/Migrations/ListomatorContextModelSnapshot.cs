﻿// <auto-generated />
using System;
using Listomator.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Listomator.Data.Migrations
{
    [DbContext(typeof(ListomatorContext))]
    partial class ListomatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("Listomator.Data.Model.ToDoGroup", b =>
                {
                    b.Property<string>("ToDoGroupName")
                        .HasColumnType("TEXT");

                    b.HasKey("ToDoGroupName");

                    b.ToTable("ToDoGroups");
                });

            modelBuilder.Entity("Listomator.Data.Model.ToDoItem", b =>
                {
                    b.Property<string>("ToDoItemName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ToDoGroupName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("UseDueDate")
                        .HasColumnType("INTEGER");

                    b.HasKey("ToDoItemName");

                    b.HasIndex("ToDoGroupName");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("Listomator.Data.Model.ToDoItem", b =>
                {
                    b.HasOne("Listomator.Data.Model.ToDoGroup", "ToDoGroup")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoGroupName");
                });
#pragma warning restore 612, 618
        }
    }
}