using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string NewItemLimit { get; set; }
        public string UnitOfDescription { get; set; }
        public DateTime?  LastRequestedDate { get; set; }

        public string LastRequestedDateString => (LastRequestedDate.Value == default(DateTime)) ?
            "No Transaction yet":
            LastRequestedDate.Value.ToString("MMMM dd, yyyy");

        public string LastRequestedName { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
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