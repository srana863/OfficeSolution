using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("ProfessionalInterest", "Institute")]
    public partial class ProfessionalInterest
    {
        public ProfessionalInterest()
        {
            this.EmployeeProfessionalInterests = new HashSet<EmployeeProfessionalInterest>();
        }

        [PKey]
        public int ProfInterestId { get; set; }
        public int InstituteId { get; set; }
        public string ProfInterestTitle { get; set; }
        public string ProfInterestDetails { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<EmployeeProfessionalInterest> EmployeeProfessionalInterests { get; set; }
    }
}
