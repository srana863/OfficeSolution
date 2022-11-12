using Layer.Model.HRMS.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("Faculty", "Institute")]
    public partial class Faculty
    {
        public Faculty()
        {
            this.FacultyExpertiseAreas = new HashSet<FacultyExpertiseArea>();
            this.FacultyProfessionalInterests = new HashSet<FacultyProfessionalInterest>();
            this.Users = new HashSet<User>();
        }

        [PKey]
        public int FacultyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FacultyFullName { get; set; }
        public int DepartmentId { get; set; }
        public int InstituteId { get; set; }
        public int DesignationId { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string Education { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public Nullable<decimal> Experience { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Institutes Institute { get; set; }
        public virtual ICollection<FacultyExpertiseArea> FacultyExpertiseAreas { get; set; }      
        public virtual ICollection<FacultyProfessionalInterest> FacultyProfessionalInterests { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
