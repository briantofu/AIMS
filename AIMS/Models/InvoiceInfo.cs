using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class InvoiceInfo : Client
    {

        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoicePeriod { get; set; }
        public string AccountName { get; set; }
        public string USDAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string SwiftCode { get; set; }

        public string InvoiceDateString
        {
            get
            {
                return String.Format("{0:MMMM dd, yyyy}", InvoiceDate);

            }
        }
        public string DueDateString
        {
            get
            {
                return String.Format("{0:MMMM dd, yyyy}", DueDate);

            }
        }
    }
}