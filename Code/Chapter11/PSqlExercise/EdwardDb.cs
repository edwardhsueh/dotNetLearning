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
      // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      // {
      //      string path = Path.Combine(System.Environment.CurrentDirectory, "EdwardDb.db");
      //        optionsBuilder.UseSqlite($"Filename={path}");
      // }

      /// <summary>
      /// Connect to MSSqlServer
      /// </summary>
      /// <param name="optionsBuilder"></param>
      // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      // {
      //        string connectionString = "Server=127.0.0.1,1433;Database=EdwardDb;User Id=sa;Password=@Domy5506987;";
      //        optionsBuilder.UseSqlServer(connectionString);
      // }

      /// <summary>
      /// Connect to PostgreSQL
      /// </summary>
      /// <param name="optionsBuilder"></param>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
             string connectionString = "Server=127.0.0.1;Port=5432;Database=EdwardDb;User Id=postgres;Password=@Domy5506987;";
             optionsBuilder.UseNpgsql(connectionString);
      }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

        new BlogEntityTypeConfiguration().Configure(modelBuilder.Entity<Blog>());
        new PostEntityTypeConfiguration().Configure(modelBuilder.Entity<Post>());

      }
  }
}
