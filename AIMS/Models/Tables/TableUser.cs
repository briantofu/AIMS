using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
    public class TableUser : TableBasedModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [StringLength(150)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Lastname { get; set; }
        [StringLength(150)]
        public string Firstname { get; set; }
        [StringLength(150)]
        public string Middlename { get; set; }
        [StringLength(150)]
        public string Department { get; set; }

        [StringLength(150)]
        public string Contact { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public int Status { get; set; }
    }
}