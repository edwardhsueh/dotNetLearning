using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Keyless]
    public partial class EmployeeTerritory
    {
        [Column("EmployeeID", TypeName = "integer")]
        public long EmployeeId { get; set; }
        [Required]
        [Column("TerritoryID", TypeName = "varchar (20)")]
        public string TerritoryId { get; set; }
    }
}
