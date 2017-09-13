using BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryEntity
{
    [Table("PartialDelivery")]
    public class EPartialDelivery : EBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartialDeliveryId { get; set; }

        public int RequisitionId { get; set; }

        public int SupplierId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string SupplierInvoiceNo { get; set; }

        public string DeliveryReceiptNo { get; set; }

        public string Status { get; set; }

    }
}

