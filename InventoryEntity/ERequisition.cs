using BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("Requisition")]
    public class ERequisition : EBase
    {
        public ERequisition()
        {
            RequisitionDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitionId { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequisitionDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime RequiredDate { get; set; }

        public string SpecialInstruction { get; set; }

        public string RequisitionType { get; set; }

        public string ReasonForDeclined { get; set; }

        public int LocationId { get; set; }

        public int SupplierId { get; set; }

        public int? UserId { get; set; }

        public double DeliveryCharges { get; set; }

        public string Status { get; set; }

    }
}