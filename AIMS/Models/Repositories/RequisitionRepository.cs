using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models.Repositories
{
    public class RequisitionRepository : Repository<Requisition>
    {
        public List<Requisition> GetByInstruction(string instruction)
        {
            return DbSet.Where(a => a.SpecialInstruction.Contains(instruction)).ToList();
        }
    }
}