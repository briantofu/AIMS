using AIMS.Models.Context;
using AIMS.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class InventoryDbContextInitializer : CreateDatabaseIfNotExists<InventoryDbContext>
    {
        protected override void Seed(InventoryDbContext context)
        {

            TableInventoryItem inventortItem = new TableInventoryItem()
            {
                ItemName = "DefaultItem",
                UnitOfMeasurementID = 1
            };

            context.TblInventoryItem.Add(inventortItem);
            context.SaveChanges();
        }
    }
}