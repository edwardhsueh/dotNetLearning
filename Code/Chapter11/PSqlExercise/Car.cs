using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Edward.Shared{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string State { get; set; }
        public string DisplayName {get;set;}
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }
        [Required]
        [MaxLength(10)]
        public string Model { get; set; }
        // property to hold concurency
        public uint xmin {get;set;}
    }

  public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
  {
      // Define Foreign Key reference
      public void Configure(EntityTypeBuilder<Car> builder)
      {
            // Define Composite Key
            builder
                .HasKey(c => new { c.State, c.LicensePlate });
            builder
                .Property(c => c.DisplayName)
                .HasComputedColumnSql("\"State\" || ', ' ||  \"LicensePlate\"", stored: true);
            // concurrency token for PostGreSQL
            builder
                .UseXminAsConcurrencyToken();

      }
  }
}
