using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableInventoryItem : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryItemID { get; set; }
        
        [StringLength(150)]
        public string ItemName { get; set; }

        public int? UnitOfMeasurementID { get; set; }

        public int? Location { get; set; }
    }
}

