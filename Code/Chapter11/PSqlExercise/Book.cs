using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edward.Shared{
  public class Book {
    public int BookId{get;set;}
    private string _url;
    public string Url
    {
        get { return _url; }
    }

    public void SetUrl(string url)
    {
        // put your validation code here

        _url = url;
    }

    private string _privateKey;
    public string GetKey()
    {
        return _privateKey;
    }

    public void SetKey(string key)
    {
        _privateKey = key;
    }
  }

  public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
  {

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // set Field-only properties
        builder
          .Property("_privateKey");
        // set Backing Field
        builder
          .Property(b => b.Url)
          .HasField("_url");
    }
  }
}