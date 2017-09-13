using AIMS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIMS.Models.Tables
{
   public class TableBasedModel
    {
        private string mCreatedBy { get; set; }
        private string mUpdatedBy { get; set; }
        private DateTime? mCreatedDate { get; set; }
        private DateTime? mUpdatedDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate
        {
            get
            {
                if (mCreatedDate.HasValue)
                {
                    return mCreatedDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                mCreatedDate = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                if (string.IsNullOrEmpty(mCreatedBy))
                {
                    return Int32.Parse(WindowsUser.UserID);
                }
                else
                {
                    return Int32.Parse(mCreatedBy);
                }
            }
            set
            {
                mCreatedBy = value.ToString();
            }
        }

        [Column(TypeName = "DateTime2")]
        public DateTime? UpdatedDate
        {
            get
            {
                if (mUpdatedDate.HasValue)
                {
                    return mUpdatedDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                mUpdatedDate = value;
            }
        }
        public int UpdatedBy
        {
            get
            {
                if (string.IsNullOrEmpty(mUpdatedBy))
                {
                    return Int32.Parse(WindowsUser.UserID);
                }
                else
                {
                    return Int32.Parse(mUpdatedBy);
                }
            }
            set
            {
                mUpdatedBy = value.ToString();
            }
        }
   } 
}