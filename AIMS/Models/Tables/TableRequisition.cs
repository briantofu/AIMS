using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableRequisition : TableBasedModel
    {
        public TableRequisition()
        {
            RequisitionDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitionID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequisitionDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequiredDate { get; set; }

        public string SpecialInstruction { get; set; }

        public string RequisitionType { get; set; }

        public string ReasonForDeclined { get; set; }
        
        public int LocationID { get; set; }

        public int SupplierID { get; set; }

        public int? UserID { get; set; }

        public string Status { get; set; }

    }
}