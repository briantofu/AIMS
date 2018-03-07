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
        public string ItemDescription { get; set; }
        public string NewItemDescription { get; set; }

        public int LocationID { get; set; }
        public string Name { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public List<Location> LocationList { get; set; }

    }
}