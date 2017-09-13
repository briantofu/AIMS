using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("RequestItem")]
    public class ERequestItem : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestItemId { get; set; }

        public int RequestId { get; set; }
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

    }
}