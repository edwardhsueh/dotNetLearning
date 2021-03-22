using System;
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
      // represent relation to Blog
      public int MainBlogId { get; set; }
      public Blog MainBlog { get; set; }
      // represent relation to Blog
      public int SubBlogId { get; set; }
      public Blog SubBlog { get; set; }
      // represent relation to NameMap
      public int NameMapId { get;set;}
      public NameMap NameMap {get;set;}
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
          builder
            .HasOne(p => p.MainBlog)
            .WithMany(b => b.MainPosts)
            .HasForeignKey(p => p.MainBlogId);
          builder
            .HasOne(p => p.SubBlog)
            .WithMany(b => b.SubPosts)
            .HasForeignKey(p => p.SubBlogId)
            .OnDelete(DeleteBehavior.ClientCascade);
          builder
            .HasOne(p => p.NameMap)
            .WithOne(n => n.Post)
            .HasForeignKey<Post>("NameMapId");
          // Define Precision, usually for decimal and DataTime
          builder
            .Property(b => b.Pay)
            .HasPrecision(14, 2);
          // PostGreSQL doesn't support, but MSSQL is ok
          builder
              .Property(b => b.LastUpated)
              .HasPrecision(3);
      }
  }
}
