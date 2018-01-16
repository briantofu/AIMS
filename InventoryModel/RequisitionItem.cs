using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class RequisitionItem : Supplier
    {
        //Requisition
        public int RequisitionItemID { get; set; }
        public int InventoryItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public string LineTotal {
            get
            {
                return String.Format("{0:n}", (UnitPrice * Quantity));
            }
        }
        public int DeliveredQuantity { get; set; }
        public int BalanceQuantity { get; set; }
        public string RoundedUnitPrice
        {
            get
            {
                return String.Format("{0:n}", UnitPrice);
            }
        }
        public int PurchaseOrderId { get; set; }
        public bool isItemSelected { get; set; }
        public int RequisitionId { get; internal set; }
        public DateTime? CreatedDate { get; internal set; }
        public double DeliveryCharges { get; internal set; }
        public DateTime DeliveryDate { get; internal set; }
        //public string WholdingTax { get; set; }
    }

}