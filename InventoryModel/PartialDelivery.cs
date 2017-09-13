using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class PartialDelivery
    {
        public int PartialDeliveryId { get; set; }
        public int RequisitionId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string DeliveryReceiptNo { get; set; }

    }
}