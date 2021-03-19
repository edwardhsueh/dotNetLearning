namespace Edward.Shared{
  public class NameMap
  {
      public int NameMapId { get; set; }
      public string Name { get; set; }

      public Post Post {get;set;}
      public Blog Blog {get;set;}
  }
}