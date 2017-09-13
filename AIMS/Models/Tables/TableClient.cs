using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableClient : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientBaseID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string TinNo { get; set; }
    }
}