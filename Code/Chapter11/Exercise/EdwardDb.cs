using Microsoft.EntityFrameworkCore;
using System.IO;
namespace Edward.Shared{
  public class EdwardDb : DbContext
  {
      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
           string path = Path.Combine(System.Environment.CurrentDirectory, "EdwardDb.db");
             optionsBuilder.UseSqlite($"Filename={path}");
      }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        // Use the shadow property as a foreign key
          modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey("BlogId");
          modelBuilder.Entity<Post>()
            .HasOne(p => p.SubBlog)
            .WithMany(b => b.SubPosts)
            .HasForeignKey("SubBlogId");
          modelBuilder.Entity<Post>()
            .HasOne(p => p.NameMap)
            .WithOne(n => n.Post)
            .HasForeignKey<Post>("NameMapId");
          modelBuilder.Entity<Blog>()
            .HasOne(b => b.NameMap)
            .WithOne(n => n.Blog)
            .HasForeignKey<Blog>("NameMapId");
      }
  }
}
