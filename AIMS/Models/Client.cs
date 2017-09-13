using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Client
    {
        public int ClientBaseID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string TinNo { get; set; }
        public List<ClientItem> ClientItemList { get; set; }
        public double Subtotal { get; set; }
        public double Salestax { get; set; }
        public double Total { get; set; }
    }
}