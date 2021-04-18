using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Index(nameof(CustomerId), Name = "CustomerID")]
    [Index(nameof(CustomerId), Name = "CustomersOrders")]
    [Index(nameof(EmployeeId), Name = "EmployeeID")]
    [Index(nameof(EmployeeId), Name = "EmployeesOrders")]
    [Index(nameof(OrderDate), Name = "OrderDate")]
    [Index(nameof(ShipPostalCode), Name = "ShipPostalCode")]
    [Index(nameof(ShippedDate), Name = "ShippedDate")]
    [Index(nameof(ShipVia), Name = "ShippersOrders")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("OrderID")]
        public long OrderId { get; set; }
        [Column("CustomerID", TypeName = "char (5)")]
        public string CustomerId { get; set; }
        [Column("EmployeeID", TypeName = "integer")]
        public long? EmployeeId { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime? RequiredDate { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime? ShippedDate { get; set; }
        [Column(TypeName = "integer")]
        public long? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [Column(TypeName = "varchar (40)")]
        public string ShipName { get; set; }
        [Column(TypeName = "varchar (60)")]
        public string ShipAddress { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string ShipCity { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string ShipRegion { get; set; }
        [Column(TypeName = "varchar (10)")]
        public string ShipPostalCode { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string ShipCountry { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Orders")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(ShipVia))]
        [InverseProperty(nameof(Shipper.Orders))]
        public virtual Shipper ShipViaNavigation { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
