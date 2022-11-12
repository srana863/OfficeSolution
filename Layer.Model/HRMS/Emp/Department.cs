using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Emp
{
    [Table("Department", "Emp")]
    public class Department
    {
        [PKey]
        public int DeptId { get; set; }

        public int InstituteId { get; set; }

        public string DeptName { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
