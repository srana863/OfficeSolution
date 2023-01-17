using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("Department", "Institute")]
    public partial class Department
    {
        public Department()
        {
            this.Faculties = new HashSet<Employee>();
        }
        
        [PKey]
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string DeptNameBangla { get; set; }
        public string DeptAnchorName { get; set; }

        public int InstituteId { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<Employee> Faculties { get; set; }
        public virtual Institutes Institute { get; set; }
    }
}
