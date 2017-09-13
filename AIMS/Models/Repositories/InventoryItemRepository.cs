using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models.Repositories
{
    public class InventoryItemRepository : Repository<InventoryItem>
    {
        public List<InventoryItem> GetByInstruction(string itemName)
        {
            return DbSet.Where(a => a.ItemName.Contains(itemName)).ToList();
        }
    }
}