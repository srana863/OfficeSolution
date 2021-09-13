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

        public int OrgId { get; set; }

        public string DeptName { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
