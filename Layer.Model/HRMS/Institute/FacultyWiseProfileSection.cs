using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("EmployeeWiseProfileSection", "Institute")]
    public partial class EmployeeWiseProfileSection
    {
        [PKey]
        public int SL { get; set; }

        public int EmployeeId { get; set; }

        public int ProfileSectionId { get; set; }

        public int InstituteId { get; set; }

        public string ProfileSectionDetails { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
