using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("PartialDeliveryItem")]
    public class EPartialDeliveryItem : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartialDeliveryItemId { get; set; }

        public int PartialDeliveryId { get; set; }

        public int DeliveredQuantity { get; set; }

        public int RequisitionItemId { get; set; }
        
    }
}

