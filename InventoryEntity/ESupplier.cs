using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("Supplier")]
    public class ESupplier : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }
        [StringLength(150)]
        public string SupplierName { get; set; }
        public string Address { get; set; }
        [StringLength(150)]
        public string ContactPerson { get; set; }
        [StringLength(150)]
        public string ContactNo { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(150)]
        public string TinNumber { get; set; }
        public bool Vatable { get; set; }
    }
}