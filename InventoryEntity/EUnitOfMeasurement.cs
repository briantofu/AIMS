using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("UnitOfMeasurement")]
    public class EUnitOfMeasurement : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitOfMeasurementId{ get; set; }

        public string Description{ get; set; }


    }
}