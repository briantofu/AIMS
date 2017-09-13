using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Request : Account
    {
        //Request
        public int RequestID { get; set; }
        public DateTime RequisitionDate { get; set; }
        public DateTime RequiredDate { get; set; }

        public string RequisitionDateString { get; set; }
        public string RequiredDateString { get; set; }

        public string RequisitionType { get; set; }
        public string SpecialInstruction { get; set; }
        public string Status{ get; set; }

        public string ReasonForDeclined { get; set; }

        public int LocationID { get; set; }
        public string LocationName { get; set; }

        public List<RequestItem> RequestItems { get; set; }

    }
}