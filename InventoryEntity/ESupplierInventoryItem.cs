using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("SupplierInventoryItem")]
    public class ESupplierInventoryItem : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierItemId{ get; set; }

        public int SupplierId { get; set; }

        public int InventoryId { get; set; }

        public double UnitPrice { get; set; }


    }
}