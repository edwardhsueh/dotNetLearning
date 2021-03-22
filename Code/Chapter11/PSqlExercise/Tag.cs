using System.Collections.Generic;

namespace Edward.Shared {
  public class Tag
  {
      public string TagId { get; set; }
      // for Many to Many ratlion to PostTag
      public ICollection<Post> Posts { get; set; }
      // for 1 to Many ratlion to PostTag
      public ICollection<PostTag> PostTags { get; set; }
  }
}
