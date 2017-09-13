using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class SupplierInventoryItem 
    {
      
        public int SupplierItemID { get; set; }
        public int SupplierID { get; set; }

        public int InventoryItemID { get; set; }
        public string ItemName { get; set; }
        public string UomDescription { get; set; }
        public string ItemCode { get; set; }
        public double UnitPrice { get; set; }
        public string RoundedUnitPrice
        {
            get
            {
                return String.Format("{0:0.00}",UnitPrice);
            }
        }
    }
}