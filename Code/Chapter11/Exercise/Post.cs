namespace Edward.Shared{
  public class Post
  {
      public int PostId { get; set; }
      public string Title { get; set; }
      public string Content { get; set; }
      // represent relation to Blog
      public int BlogId { get; set; }
      public Blog Blog { get; set; }
      // represent relation to Blog
      public int SubBlogId { get; set; }
      public Blog SubBlog { get; set; }
      // represent relation to NameMap
      public int NameMapId { get;set;}
      public NameMap NameMap {get;set;}
  }
}
