﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Talkish.Dal;

namespace Talkish.Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogTopic", b =>
                {
                    b.Property<int>("BlogsBlogId")
                        .HasColumnType("int");

                    b.Property<int>("TopicsTopicId")
                        .HasColumnType("int");

                    b.HasKey("BlogsBlogId", "TopicsTopicId");

                    b.HasIndex("TopicsTopicId");

                    b.ToTable("BlogTopic");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PublicationId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId");

                    b.HasIndex("PublicationId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Talkish.Domain.Models.BasicInfo", b =>
                {
                    b.Property<int>("BasicInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BasicInfoId");

                    b.ToTable("BasicInfo");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PublicationId")
                        .HasColumnType("int");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("BlogId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PublicationId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Publication", b =>
                {
                    b.Property<int>("PublicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("PublicationId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TopicId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Talkish.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("BasicInfoId")
                        .HasColumnType("int");

                    b.Property<string>("IdentityId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("BasicInfoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<int>("FollowersUserId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingUserId")
                        .HasColumnType("int");

                    b.HasKey("FollowersUserId", "FollowingUserId");

                    b.HasIndex("FollowingUserId");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("BlogTopic", b =>
                {
                    b.HasOne("Talkish.Domain.Models.Blog", null)
                        .WithMany()
                        .HasForeignKey("BlogsBlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talkish.Domain.Models.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Talkish.Domain.Models.Author", b =>
                {
                    b.HasOne("Talkish.Domain.Models.Publication", null)
                        .WithMany("Authors")
                        .HasForeignKey("PublicationId");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Blog", b =>
                {
                    b.HasOne("Talkish.Domain.Models.Author", "Author")
                        .WithMany("Blogs")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talkish.Domain.Models.Publication", null)
                        .WithMany("Blogs")
                        .HasForeignKey("PublicationId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Publication", b =>
                {
                    b.HasOne("Talkish.Domain.Models.Author", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Talkish.Domain.Models.User", b =>
                {
                    b.HasOne("Talkish.Domain.Models.BasicInfo", "BasicInfo")
                        .WithMany()
                        .HasForeignKey("BasicInfoId");

                    b.HasOne("Talkish.Domain.Models.Author", "AuthorProfile")
                        .WithOne("UserProfile")
                        .HasForeignKey("Talkish.Domain.Models.User", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorProfile");

                    b.Navigation("BasicInfo");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("Talkish.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talkish.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FollowingUserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Talkish.Domain.Models.Author", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Talkish.Domain.Models.Publication", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Blogs");
                });
#pragma warning restore 612, 618
        }
    }
}
