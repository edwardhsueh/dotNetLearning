using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edward.Shared{
  // comment supported on EFCore5.0
  [Comment("NameMap managed on the website")]
  public class Blog
  {
      // disable auto generation Key
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public int BlogId { get; set; }
      public string Url { get; set; }
      public uint xmin { get; set; }

      // represent 1 to Many relation to Post
      public ICollection<Post> MainPosts {get;set;}
      // represent 1 to Many  relation to Post
      public ICollection<Post> SubPosts {get;set;}
      // represent 1 to 1 relation to NameMap
      public int NameMapId { get;set;}
      public NameMap NameMap {get;set;}
  }
  public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
  {
      public void Configure(EntityTypeBuilder<Blog> builder)
      {
          // Define Table Description
          builder
            .HasComment("Blogs by Edward");
          // Define Foreign Key reference
          builder
            .HasOne(b => b.NameMap)
            .WithOne(n => n.Blog)
            .HasForeignKey<Blog>("NameMapId")
            .OnDelete(DeleteBehavior.ClientCascade);
          // concurrency token for PostGreSQL
          builder
            .UseXminAsConcurrencyToken();


      }
  }
}
