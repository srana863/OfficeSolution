using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("Institute", "Institute")]
    public partial class Institutes
    {
        public Institutes()
        {
            this.Faculties = new HashSet<Employee>();
            this.Departments = new HashSet<Department>();
            this.Designations = new HashSet<Designation>();
        }
        [PKey]
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public string InstituteCode { get; set; }
        public string Address { get; set; }
        public string InstituteLogo { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<Employee> Faculties { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
    }
}
