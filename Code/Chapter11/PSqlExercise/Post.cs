using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/// <summary>
/// For MS SQL Server, to Prevent multiple cascade paths,
/// (1) we should keep only one non-nullable foregin key, which map to cacade
///     delete(by database)
/// (2) other foregin Key should use nullable foreign key, which map to
/// client-cascade in fluent API(Database use Delete no action)[OnDelete(DeleteBehavior.ClientCascade);]
/// refer to https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete for
/// more detail
/// For PostGre SQL, no limit
/// </summary>
namespace Edward.Shared{
  public class Post
  {
      public int PostId { get; set; }
      [Column(TypeName = "varchar(200)")]
      public string Title { get; set; }
      [MaxLength(500)]
      [Required]
      public string Content { get; set; }
      public decimal Pay {get;set;}
      public DateTime LastUpated {get;set;}
      // rowversion to handle concurrentcy for postGreSql
      public uint xmin { get; set; }

      // Many to 1 relation to Blog
      public int MainBlogId { get; set; }
      public Blog MainBlog { get; set; }
      // Many to 1 relation to Blog
      public int SubBlogId { get; set; }
      public Blog SubBlog { get; set; }
      // 1 to 1 relation to NameMap
      public int NameMapId { get;set;}
      public NameMap NameMap {get;set;}
      // Many-to-Many relation to Tag
      public ICollection<Tag> Tags {get;set;}
      // for 1 to Many ratlion to PostTag
      public ICollection<PostTag> PostTags { get; set; }


  }
  public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
  {
      public void Configure(EntityTypeBuilder<Post> builder)
      {
          // Define Table Description
          builder
            .HasComment("Posts by Edward");
          // Define Column Descritpion
          builder
            .Property(b => b.Title)
            .HasComment("The Title of the Post");
          // Define Foreign Key reference
          // if two ore more Foreign Key refernce the same priciple table,
          // MS SQL Server allow only one of them to be cascade delete
          // Other should use ClientCascade, PostGreSQL no limit
          // Many to one relation
          builder
            .HasOne(p => p.MainBlog)
            .WithMany(b => b.MainPosts)
            .HasForeignKey(p => p.MainBlogId);

          // Many to one ralation
          builder
            .HasOne(p => p.SubBlog)
            .WithMany(b => b.SubPosts)
            .HasForeignKey(p => p.SubBlogId);

          // one to one ralation
          builder
            .HasOne(p => p.NameMap)
            .WithOne(n => n.Post)
            .HasForeignKey<Post>("NameMapId");

          // Define Many to Many Relatoin with beSpoke Joint　Table
          builder
            .HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity<PostTag>(
                j => j
                    .HasOne(pt => pt.Tag)
                    .WithMany(t => t.PostTags)
                    .HasForeignKey(pt => pt.TagId),
                j => j
                    .HasOne(pt => pt.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(pt => pt.PostId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.PostId, t.TagId });
                });
          // Define Precision, usually for decimal and DataTime
          builder
            .Property(b => b.Pay)
            .HasPrecision(14, 2);
          // PostGreSQL doesn't support, but MSSQL is ok
          // builder
          //     .Property(b => b.LastUpated)
          //     .HasPrecision(3);

          // set Concurrency xmin column
          builder
            .UseXminAsConcurrencyToken();

          builder
            .HasData(
              new Post{PostId=1, Title="第一篇", Content="第一篇內容", LastUpated=DateTime.Now, MainBlogId=1, SubBlogId =2, NameMapId=1},
              new Post{PostId=2, Title="第二篇", Content="第二篇內容", LastUpated=DateTime.Now, MainBlogId=1, SubBlogId =3, NameMapId=2}
            );
      }
  }
}
