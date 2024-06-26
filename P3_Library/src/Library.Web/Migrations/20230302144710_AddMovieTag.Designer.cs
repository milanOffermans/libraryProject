﻿// <auto-generated />
using System;
using Library.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Web.Migrations
{
    [DbContext(typeof(LibContext))]
    [Migration("20230302144710_AddMovieTag")]
    partial class AddMovieTag
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.Web.Domain.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasMaxLength(1048576)
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Movie", (string)null);
                });

            modelBuilder.Entity("Library.Web.Domain.MovieTag", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MovieId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MovieTag", (string)null);
                });

            modelBuilder.Entity("Library.Web.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Library.Web.Domain.MovieTag", b =>
                {
                    b.HasOne("Library.Web.Domain.Movie", "Movie")
                        .WithMany("MovieTags")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Web.Domain.Tag", "Tag")
                        .WithMany("MovieTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Library.Web.Domain.Movie", b =>
                {
                    b.Navigation("MovieTags");
                });

            modelBuilder.Entity("Library.Web.Domain.Tag", b =>
                {
                    b.Navigation("MovieTags");
                });
#pragma warning restore 612, 618
        }
    }
}
