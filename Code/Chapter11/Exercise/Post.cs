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
/// client-cascade in fluent API(Database use Delete no action)
/// refer to https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete for
/// more detail
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
      public int ? SubBlogId { get; set; }
      public Blog SubBlog { get; set; }
      // represent relation to NameMap
      public int ? NameMapId { get;set;}
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
          builder
            .HasOne(p => p.MainBlog)
            .WithMany(b => b.MainPosts)
            .HasForeignKey(p => p.MainBlogId)
            .OnDelete(DeleteBehavior.ClientCascade);
          builder
            .HasOne(p => p.SubBlog)
            .WithMany(b => b.SubPosts)
            .HasForeignKey(p => p.SubBlogId)
            .OnDelete(DeleteBehavior.ClientCascade);
          builder
            .HasOne(p => p.NameMap)
            .WithOne(n => n.Post)
            .HasForeignKey<Post>("NameMapId")
            .OnDelete(DeleteBehavior.ClientCascade);
          // Define Precision, usually for decimal and DataTime
          builder
            .Property(b => b.Pay)
            .HasPrecision(14, 2);
          builder
              .Property(b => b.LastUpated)
              .HasPrecision(3);
      }
  }
}
