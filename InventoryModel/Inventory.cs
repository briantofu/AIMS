using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Inventory
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
    }
    public class Stocks
    {
        public int InventoryItemID { get; set; }
        public string ItemName { get; set; }
        public int TotalStock { get; set; }
        public int RequestedQuantity { get; set; }
        public string UnitOfDescription { get; set; }
        public int RemainingQuantity
        {
            get
            {
                return (TotalStock - RequestedQuantity);
            }
        }
        public string ItemCode { get; set; }
    }
}