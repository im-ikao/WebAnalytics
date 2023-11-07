﻿// <auto-generated />
using System;
using IKao.WebAnalytics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IKao.WebAnalytics.Infrastructure.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IKao.WebAnalytics.Domain.Model.Developer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("developers", (string)null);
                });

            modelBuilder.Entity("IKao.WebAnalytics.Domain.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("age_rating");

                    b.Property<Instant>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Instant?>("DeletionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deletion_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("description");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("instruction_description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("published_date");

                    b.Property<Instant?>("ModificationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modification_date");

                    b.Property<string>("Play")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("play_link");

                    b.Property<int>("Players")
                        .HasColumnType("int")
                        .HasColumnName("counter_players");

                    b.Property<Instant?>("Publish")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("publish_date");

                    b.Property<string>("Seo")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("seo_description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("title");

                    b.Property<int>("developer_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("developer_id");

                    b.ToTable("games", (string)null);
                });

            modelBuilder.Entity("IKao.WebAnalytics.Domain.Model.Game", b =>
                {
                    b.HasOne("IKao.WebAnalytics.Domain.Model.Developer", "Developer")
                        .WithMany("RelationGames")
                        .HasForeignKey("developer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("IKao.WebAnalytics.Domain.ValueObjects.Media", "Media", b1 =>
                        {
                            b1.Property<int>("GameId")
                                .HasColumnType("int");

                            b1.HasKey("GameId");

                            b1.ToTable("games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");

                            b1.OwnsOne("IKao.WebAnalytics.Domain.ValueObjects.Url", "Cover", b2 =>
                                {
                                    b2.Property<int>("MediaGameId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(255)")
                                        .HasColumnName("media_cover");

                                    b2.HasKey("MediaGameId");

                                    b2.ToTable("games");

                                    b2.WithOwner()
                                        .HasForeignKey("MediaGameId");
                                });

                            b1.OwnsOne("IKao.WebAnalytics.Domain.ValueObjects.Url", "Icon", b2 =>
                                {
                                    b2.Property<int>("MediaGameId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(255)")
                                        .HasColumnName("media_icon");

                                    b2.HasKey("MediaGameId");

                                    b2.ToTable("games");

                                    b2.WithOwner()
                                        .HasForeignKey("MediaGameId");
                                });

                            b1.Navigation("Cover")
                                .IsRequired();

                            b1.Navigation("Icon")
                                .IsRequired();
                        });

                    b.OwnsOne("IKao.WebAnalytics.Domain.ValueObjects.Rating", "Rating", b1 =>
                        {
                            b1.Property<int>("GameId")
                                .HasColumnType("int");

                            b1.Property<int>("Count")
                                .HasColumnType("int")
                                .HasColumnName("rating_count");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("rating_value");

                            b1.HasKey("GameId");

                            b1.ToTable("games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.Navigation("Developer");

                    b.Navigation("Media")
                        .IsRequired();

                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("IKao.WebAnalytics.Domain.Model.Developer", b =>
                {
                    b.Navigation("RelationGames");
                });
#pragma warning restore 612, 618
        }
    }
}