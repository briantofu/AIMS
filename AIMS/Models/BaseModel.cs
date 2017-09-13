
using AIMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIMS.Models
{
    public class BaseModel
    {
        private string mCreatedBy { get; set; }
        private string mUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy
        {
            get
            {
                if (string.IsNullOrEmpty(mCreatedBy))
                {
                    return WindowsUser.Username;
                }
                else
                {
                    return mCreatedBy;
                }
            }
            set
            {
                mCreatedBy = value;
            }
        }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy
        {
            get
            {
                if (string.IsNullOrEmpty(mUpdatedBy))
                {
                    return WindowsUser.Username;
                }
                else
                {
                    return mUpdatedBy;
                }
            }
            set
            {
                mUpdatedBy = value;
            }
        }
    }
}