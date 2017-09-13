using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableSupplier : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        [StringLength(150)]
        public string SupplierName { get; set; }
        public string Address { get; set; }
        [StringLength(150)]
        public string ContactPerson { get; set; }
        [StringLength(150)]
        public string ContactNo { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(150)]
        public string TinNumber { get; set; }

    }
}