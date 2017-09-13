using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class Account : BaseModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string FullName
        {
            get
            {
                string name = string.Empty;
                if (!string.IsNullOrEmpty(Lastname))
                {
                    name = Lastname;
                }
                if (!string.IsNullOrEmpty(Firstname))
                {
                    name += ((!string.IsNullOrEmpty(Lastname)) ? ", " : string.Empty) + Firstname;
                }
                if (!string.IsNullOrEmpty(Middlename))
                {
                    name += ((!string.IsNullOrEmpty(name)) ? " " : string.Empty) + Middlename;
                }
                return name;
            }
        }
        public string Department { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}