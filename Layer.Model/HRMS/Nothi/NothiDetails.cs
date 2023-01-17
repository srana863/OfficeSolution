using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Nothi
{
    [Table("NothiDetails", "Nothi")]
    public class NothiDetails
    {
        
        [PKey]
        public int SL { get; set; }

        public int InstituteId { get; set; }

        public int DepartmentId { get; set; }
        public int DeptSl { get; set; }

        public string NothiId { get; set; }

        public int NothiTypeId { get; set; }

        public string NothiName { get; set; }

        public string NothiNameBang { get; set; }

        public string NothiNumber { get; set; }

        public string NothiNumberBang { get; set; }

        public DateTime? NothiCreationDate { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
