using System.Collections.Generic;

namespace Edward.Shared{
  public class Blog
  {
      public int ? BlogId { get; set; }
      public string Url { get; set; }

      // represent relation to Post
      public ICollection<Post> MainPosts {get;set;}
      // represent relation to Post
      public ICollection<Post> SubPosts {get;set;}
      // represent relation to NameMap
      public int NameMapId { get;set;}
      public NameMap NameMap {get;set;}
  }
}
