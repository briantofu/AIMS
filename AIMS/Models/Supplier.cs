using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Supplier
    {
        public int? SupplierID { get; set; }
        public string TinNumber { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool Vatable { get; set; }
        public List<SupplierInventoryItem> supplierItemList { get; set; }

        public List<RequisitionItem> RequisitionItem { get; set; }
    }
}