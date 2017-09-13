using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableClientItem : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientItemID { get; set; }
        public int ClientBaseID { get; set; }
        public int Quantity { get; set; }
        public int ItemNo { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
    }
}