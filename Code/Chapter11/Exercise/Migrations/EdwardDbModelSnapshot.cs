﻿// <auto-generated />
using Edward.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exercise.Migrations
{
    [DbContext(typeof(EdwardDb))]
    partial class EdwardDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Edward.Shared.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NameMapId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("BlogId");

                    b.HasIndex("NameMapId")
                        .IsUnique();

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Edward.Shared.NameMap", b =>
                {
                    b.Property<int>("NameMapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("NameMapId");

                    b.ToTable("NameMap");
                });

            modelBuilder.Entity("Edward.Shared.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BlogId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<int>("NameMapId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubBlogId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.HasIndex("NameMapId")
                        .IsUnique();

                    b.HasIndex("SubBlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Edward.Shared.Blog", b =>
                {
                    b.HasOne("Edward.Shared.NameMap", "NameMap")
                        .WithOne("Blog")
                        .HasForeignKey("Edward.Shared.Blog", "NameMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NameMap");
                });

            modelBuilder.Entity("Edward.Shared.Post", b =>
                {
                    b.HasOne("Edward.Shared.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Edward.Shared.NameMap", "NameMap")
                        .WithOne("Post")
                        .HasForeignKey("Edward.Shared.Post", "NameMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Edward.Shared.Blog", "SubBlog")
                        .WithMany("SubPosts")
                        .HasForeignKey("SubBlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");

                    b.Navigation("NameMap");

                    b.Navigation("SubBlog");
                });

            modelBuilder.Entity("Edward.Shared.Blog", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("SubPosts");
                });

            modelBuilder.Entity("Edward.Shared.NameMap", b =>
                {
                    b.Navigation("Blog");

                    b.Navigation("Post");
                });
#pragma warning restore 612, 618
        }
    }
}
