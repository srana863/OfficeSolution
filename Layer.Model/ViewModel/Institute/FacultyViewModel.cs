using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Institute
{
    public partial class EmployeeViewModel: Employee
    {
        public EmployeeViewModel()
        {
            this.EmployeeExpertiseAreaViewModel = new HashSet<EmployeeExpertiseAreaViewModel>();
            this.EmployeeProfessionalInterestViewModel = new HashSet<EmployeeProfessionalInterestViewModel>();
            this.EmployeeWiseProfileSectionViewModel = new HashSet<EmployeeWiseProfileSectionViewModel>();
        }
        public string DepartmentName { get; set; }
        public string InstituteName { get; set; }
        public string DesignationName { get; set; }

        public string ProfileSectionDetails { get; set; }
        public int ProfileSectionId { get; set; }


        public virtual IEnumerable<EmployeeExpertiseAreaViewModel> EmployeeExpertiseAreaViewModel { get; set; }
        public virtual IEnumerable<EmployeeProfessionalInterestViewModel> EmployeeProfessionalInterestViewModel { get; set; }
        public virtual IEnumerable<EmployeeWiseProfileSectionViewModel> EmployeeWiseProfileSectionViewModel { get; set; }
    }
}
