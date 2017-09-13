using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Page
    {
        public int PositionId { get; set; }
        public int PageNumber { get; set; }
        public bool PageStatus { get; set; }
        public int itemPerPage { get { return 50; } set { } }
    }
}