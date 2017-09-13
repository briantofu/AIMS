using BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryEntity
{
 
    [Table("PurchasingOrder")]
    public class EPurchasingOrder : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseOrderId { get; set; }
        public int RequisitionId { get; set; }
        public int SupplierId { get; set; }
        public double DeliveryCharges { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
