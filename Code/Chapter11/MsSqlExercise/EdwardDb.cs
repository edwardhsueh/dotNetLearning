using Microsoft.EntityFrameworkCore;
using System.IO;
namespace Edward.Shared{
  public class EdwardDb : DbContext
  {
      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }
      public DbSet<NameMap> NameMaps {get;set;}
      /// <summary>
      /// Connect to MSSqlServer
      /// </summary>
      /// <param name="optionsBuilder"></param>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
             string connectionString = "Server=127.0.0.1,1433;Database=EdwardDb;User Id=sa;Password=@Domy5506987;";
             optionsBuilder.UseSqlServer(connectionString);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

        new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
        new PostEntityTypeConfiguration().Configure(modelBuilder.Entity<Post>());

      }
  }
}
