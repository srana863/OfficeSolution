using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("AreaOfExpertise", "Institute")]
    public partial class AreaOfExpertise
    {
        public AreaOfExpertise()
        {
            this.EmployeeExpertiseAreas = new HashSet<EmployeeExpertiseArea>();
        }

        [PKey]
        public int ExpertiseId { get; set; }
        public int InstituteId { get; set; }
        public string ExpertiseTitle { get; set; }
        public string ExpertiseDetails { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<EmployeeExpertiseArea> EmployeeExpertiseAreas { get; set; }
    }
}
