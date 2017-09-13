using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class RequestItem 
    {
        //Request
        public int RequestItemID { get; set; }
        public int InventoryItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }

    }
}