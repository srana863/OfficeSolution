using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("FacultyProfessionalInterest", "Institute")]
    public partial class FacultyProfessionalInterest
    {
        
        [PKey]
        public int SL { get; set; }
        public int InstituteId { get; set; }
        public int FacultyId { get; set; }
        public int ProfInterestId { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual ProfessionalInterest ProfessionalInterest { get; set; }
    }
}
