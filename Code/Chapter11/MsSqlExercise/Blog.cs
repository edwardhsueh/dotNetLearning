using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edward.Shared{
  // comment supported on EFCore5.0
  [Comment("NameMap managed on the website")]
  public class Blog
  {
      public int BlogId { get; set; }
      public string Url { get; set; }

      // represent relation to Post
      public ICollection<Post> MainPosts {get;set;}
      // represent relation to Post
      public ICollection<Post> SubPosts {get;set;}
      // represent relation to NameMap
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


      }
  }
}
