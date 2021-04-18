using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Index(nameof(CategoryId), Name = "CategoriesProducts")]
    [Index(nameof(CategoryId), Name = "CategoryID")]
    [Index(nameof(ProductName), Name = "ProductName")]
    [Index(nameof(SupplierId), Name = "SupplierID")]
    [Index(nameof(SupplierId), Name = "SuppliersProducts")]
    public partial class Product
    {
        // public Product()
        // {
        //     OrderDetails = new HashSet<OrderDetail>();
        // }

        [Key]
        [Column("ProductID")]
        public long ProductId { get; set; }
        [Required]
        [Column(TypeName = "varchar (40)")]
        public string ProductName { get; set; }
        [Column("SupplierID", TypeName = "integer")]
        public long? SupplierId { get; set; }
        [Column("CategoryID", TypeName = "integer")]
        public long? CategoryId { get; set; }
        [Column(TypeName = "varchar (20)")]
        public string QuantityPerUnit { get; set; }
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "smallint")]
        public long? UnitsInStock { get; set; }
        [Column(TypeName = "smallint")]
        public long? UnitsOnOrder { get; set; }
        [Column(TypeName = "smallint")]
        public long? ReorderLevel { get; set; }
        [Required]
        [Column(TypeName = "boolean")]
        public bool Discontinued { get; set; } = false;

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
