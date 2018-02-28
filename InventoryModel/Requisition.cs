using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Requisition : Account
    {
        //Request
        public int RequisitionID { get; set; }
        public int PartialDeliveryID { get; set; }
        public DateTime RequisitionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string RequisitionDateString { get; set; }
        public string DeliveryDateString { get; set; }
        public string RequiredDateString { get; set; }
        public string RequisitionType { get; set; }
        public string SpecialInstruction { get; set; }
        public string Status {get; set; }
  
        public string Reason { get; set; }
        public int LocationID { get; set; }
        public double DeliveryCharges { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public int SupplierID { get; set; }
        public int PurchaseOrderID { get; set; }
        public List<RequisitionItem> RequisitionItems { get; set; }
        public string SupplierInvoiceNo { get; set; }
        public string DeliveryReceiptNo { get; set; }

    }

}