using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Edward.Shared{
  public class NameMap
  {
      public int NameMapId { get; set; }
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
           builder
            .HasData(
                new NameMap{NameMapId=1, Name="中視"},
                new NameMap{NameMapId=2, Name="台視"},
                new NameMap{NameMapId=3, Name="華視"},
                new NameMap{NameMapId=4, Name="華視4"},
                new NameMap{NameMapId=5, Name="華視5"},
                new NameMap{NameMapId=6, Name="華視6"}
            );
      }
  }
}