using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class InventoryItem 
    {
        public int InventoryItemID { get; set; }
        public string ItemName { get; set; }
        public string Location { get; set; }
        public string NewItemName { get; set; }
        public int UnitOfMeasurementID{ get; set; }
        public string  Description { get; set; }
        public string ItemCode { get; set; }
        public string NewItemCode { get; set; }
        public string ItemBegBal { get; set; }
        public string NewBegBal { get; set; }

        public int LocationID { get; set; }
        public string Name { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public List<Location> LocationList { get; set; }

    }
}