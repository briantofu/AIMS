using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableSupplierInventoryItem : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierItemID{ get; set; }

        public int SupplierID { get; set; }

        public int InventoryID { get; set; }


    }
}