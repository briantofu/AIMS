using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("Location")]
    public class ELocation : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
    }
}