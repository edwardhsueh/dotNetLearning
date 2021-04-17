using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
namespace Packt.Shared
{
  public class Product
  {
    public int ProductID { get; set; }
    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; }

    [Column("UnitPrice", TypeName = "money")]
    public decimal? Cost { get; set; } // property name != field name

    [Column("UnitsInStock")]
    public short? Stock { get; set; }

    public bool Discontinued { get; set; }
    // these two define the foreign key relationship
    // to the Categories table
    public int CategoryID { get; set; }
    [JsonIgnore]
    [XmlIgnore]
    public virtual Category Category { get; set; }
  }
}