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
            this.Faculties = new HashSet<Faculty>();
        }
        
        [PKey]
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public int InstituteId { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual Institutes Institute { get; set; }
    }
}
