using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Edward.Shared{
  public class NameMap
  {
      public int ? NameMapId { get; set; }
      public string Name { get; set; }

      public Post Post {get;set;}
      public Blog Blog {get;set;}
  }
  public class NameMapEntityTypeConfiguration : IEntityTypeConfiguration<NameMap>
  {
      // Define Foreign Key reference
      public void Configure(EntityTypeBuilder<NameMap> builder)
      {
          // Define Table Description
           builder
            .HasComment("NameMap by Edward");
      }
  }
}