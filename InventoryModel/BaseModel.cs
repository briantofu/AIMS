using System;

namespace AIMS.Models
{
    public class BaseModel
    {
        private string mCreatedBy { get; set; }
        private string mUpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}