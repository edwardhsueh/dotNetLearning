using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Index(nameof(City), Name = "City")]
    [Index(nameof(CompanyName), Name = "CompanyNameCustomers")]
    [Index(nameof(PostalCode), Name = "PostalCodeCustomers")]
    [Index(nameof(Region), Name = "Region")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("CustomerID", TypeName = "char (5)")]
        [RegularExpression("[A-Z]{5}")]
        public string CustomerId { get; set; }
        [Required]
        [Column(TypeName = "varchar (40)")]
        public string CompanyName { get; set; }
        [Column(TypeName = "varchar (30)")]
        public string ContactName { get; set; }
        [Column(TypeName = "varchar (30)")]
        public string ContactTitle { get; set; }
        [Column(TypeName = "varchar (60)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string City { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string Region { get; set; }
        [Column(TypeName = "varchar (10)")]
        public string PostalCode { get; set; }
        [Column(TypeName = "varchar (15)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar (24)")]
        public string Phone { get; set; }
        [Column(TypeName = "varchar (24)")]
        public string Fax { get; set; }

        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
