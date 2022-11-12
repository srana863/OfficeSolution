using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Institute
{
    public partial class FacultyViewModel: Faculty
    {
        public FacultyViewModel()
        {
            this.FacultyExpertiseAreaViewModel = new HashSet<FacultyExpertiseAreaViewModel>();
            this.FacultyProfessionalInterestViewModel = new HashSet<FacultyProfessionalInterestViewModel>();
        }
        public string DepartmentName { get; set; }
        public string InstituteName { get; set; }
        public string DesignationName { get; set; }

        public virtual IEnumerable<FacultyExpertiseAreaViewModel> FacultyExpertiseAreaViewModel { get; set; }
        public virtual IEnumerable<FacultyProfessionalInterestViewModel> FacultyProfessionalInterestViewModel { get; set; }
    }
}
