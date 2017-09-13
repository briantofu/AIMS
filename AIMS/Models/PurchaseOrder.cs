using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class PurchaseOrder : Account
    {
        public int SupplierID { get; set; }
        public string TinNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string SupplierEmail { get; set; }
        public bool Vatable { get; set; }
        public double DeliveryCharge { get; set; }
        public string LocationAddress { get; set; }
        public string LocationName { get; set; }

        public int RequisitionID { get; set; }
        public DateTime RequiredDate { get; set; }

        public List<RequisitionItem> RequisitionItems { get; set; }



    }
}