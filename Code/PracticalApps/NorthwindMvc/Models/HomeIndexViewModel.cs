using System.Collections.Generic;
using Packt.Shared;
namespace NorthwindMvc.Models
{
  public class HomeIndexViewModel
  {
    public int VisitorCount;
    public List<Category> Categories { get; set; } 
    public List<Product> Products { get; set; }
  }
}