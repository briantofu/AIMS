using BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("Request")]
    public class ERequest : EBase
    {
        public ERequest()
        {
            RequestDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

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