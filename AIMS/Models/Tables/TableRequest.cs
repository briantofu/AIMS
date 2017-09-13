using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableRequest : TableBasedModel
    {
        public TableRequest()
        {
            RequestDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestID { get; set; }

        public int UserID { get; set; }

        public int LocationID { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequestDate{ get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequiredDate { get; set; }

        public string SpecialInstruction { get; set; }

        public string RequisitionType { get; set; }
        
        public string ReasonForDeclined { get; set; }


        public string Status { get; set; }

    }
}