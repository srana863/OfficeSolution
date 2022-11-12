using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("FacultyExpertiseArea", "Institute")]
    public partial class FacultyExpertiseArea
    {
        [PKey]
        public int SL { get; set; }
        public int InstituteId { get; set; }
        public int FacultyId { get; set; }
        public int ExpertiseId { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual AreaOfExpertise AreaOfExpertise { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
