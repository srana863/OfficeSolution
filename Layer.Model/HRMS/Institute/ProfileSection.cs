using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("ProfileSection", "Institute")]
    public partial class ProfileSection
    {
        [PKey]
        public int ProfileSectionId { get; set; }

        public int InstituteId { get; set; }

        public string ProfileSectionTitle { get; set; }

        public string ProfileSectionDetails { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

    }
}
