using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Nothi
{
    [Table("NothiMovement", "Nothi")]
    public class NothiMovement
    {
        [PKey]
        public int NothiMovementId { get; set; }

        public int InstituteId { get; set; }

        public string NothiId { get; set; }
        public int DepartmentId { get; set; }
        public int SendToDepartmentId { get; set; }

        public int? CurrentPositionDepartmentId { get; set; }

        public int? ReturnFromDepartmentId { get; set; }

        public string CommentsWhileSending { get; set; }

        public string CommentsWhileReceiving { get; set; }

        public bool IsFinancial { get; set; }
        public decimal FinancialAmount { get; set; }
        public DateTime SendDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int Status { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
