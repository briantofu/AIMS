using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class ClientItem 
    {
        public int ClientItemID { get; set; }
        public int ClientBaseID { get; set; }
        public int Quantity { get; set; }
        public int ItemNo { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double LineTotal 
        {
            get;
            set;

        }
        public double Subtotal { get; set; }

        public double SalesTax {
            get
            {
                return Subtotal * .12;
            }
        }

        public double Total
        {
            get
            {
                return Subtotal + SalesTax;
            }
        }
    }
}