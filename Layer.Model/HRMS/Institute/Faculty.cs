using Layer.Model.HRMS.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("Employee", "Institute")]
    public partial class Employee
    {
        public Employee()
        {
            this.EmployeeExpertiseAreas = new HashSet<EmployeeExpertiseArea>();
            this.EmployeeProfessionalInterests = new HashSet<EmployeeProfessionalInterest>();
            this.Users = new HashSet<User>();
        }

        [PKey]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeFullName { get; set; }
        public int DepartmentId { get; set; }
        public int InstituteId { get; set; }
        public int DesignationId { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public bool PAOfDeptHead { get; set; }
        public bool IsOfficeHead { get; set; }
        public int Gender { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public Nullable<decimal> Experience { get; set; }
        public string About { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Institutes Institute { get; set; }
        public virtual ICollection<EmployeeExpertiseArea> EmployeeExpertiseAreas { get; set; }      
        public virtual ICollection<EmployeeProfessionalInterest> EmployeeProfessionalInterests { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
