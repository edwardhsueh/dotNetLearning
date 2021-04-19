using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Packt.Shared
{
    [Keyless]
    public partial class Territory
    {
        [Required]
        [Column("TerritoryID", TypeName = "varchar (20)")]
        public string TerritoryId { get; set; }
        [Required]
        [Column(TypeName = "varchar (50)")]
        public string TerritoryDescription { get; set; }
        [Column("RegionID", TypeName = "integer")]
        public long RegionId { get; set; }
    }
}
