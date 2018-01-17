using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("RequisitionItem")]
    public class ERequisitionItem : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitionItemId { get; set; }

        public int SupplierId { get; set; }
        public int InventoryItemId { get; set; }
        public int RequisitionId { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOrderId { get; set; }
        //public string WholdingTax { get; set; }
       
    }
}