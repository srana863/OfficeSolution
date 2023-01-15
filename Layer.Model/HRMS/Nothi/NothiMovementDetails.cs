using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Nothi
{
    [Table("NothiMovementDetails", "Nothi")]
    public class NothiMovementDetails
    {
        [PKey]
        public int SL { get; set; }

        public int NothiMovementId { get; set; }

        public int ComingFromDepartmentId { get; set; }

        public int CurrentDepartmentId { get; set; }

        public int? SendToDepartmentId { get; set; }

        public int ComingFromEmployeeId { get; set; }

        public int CurrentEmployeeId { get; set; }

        public int? SendToEmployeeId { get; set; }

        public string Remarks { get; set; }

        public DateTime ComingDate { get; set; }

        public DateTime? SendDate { get; set; }

        public bool Status { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
