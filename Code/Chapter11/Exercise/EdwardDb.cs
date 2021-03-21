using Microsoft.EntityFrameworkCore;
using System.IO;
namespace Edward.Shared{
  public class EdwardDb : DbContext
  {
      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }
      public DbSet<NameMap> NameMaps {get;set;}
      /// <summary>
      /// Connect to Sqlite
      /// </summary>
      /// <param name="optionsBuilder"></param>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
           string path = Path.Combine(System.Environment.CurrentDirectory, "EdwardDb.db");
             optionsBuilder.UseSqlite($"Filename={path}");
      }



      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

        new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
        new PostEntityTypeConfiguration().Configure(modelBuilder.Entity<Post>());

      }
  }
}
