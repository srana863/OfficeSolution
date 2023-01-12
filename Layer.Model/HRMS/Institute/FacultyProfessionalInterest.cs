using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("EmployeeProfessionalInterest", "Institute")]
    public partial class EmployeeProfessionalInterest
    {
        
        [PKey]
        public int SL { get; set; }
        public int InstituteId { get; set; }
        public int EmployeeId { get; set; }
        public int ProfInterestId { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ProfessionalInterest ProfessionalInterest { get; set; }
    }
}
