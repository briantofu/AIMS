using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableInvoice : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { get; set; }
        public int ClientBaseID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoicePeriod { get; set; }
        public string AccountName { get; set; }
        public string USDAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string SwiftCode { get; set; }
    }
}