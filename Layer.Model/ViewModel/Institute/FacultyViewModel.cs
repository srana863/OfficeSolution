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
            this.FacultyWiseProfileSectionViewModel = new HashSet<FacultyWiseProfileSectionViewModel>();
        }
        public string DepartmentName { get; set; }
        public string InstituteName { get; set; }
        public string DesignationName { get; set; }

        public string ProfileSectionDetails { get; set; }
        public int ProfileSectionId { get; set; }


        public virtual IEnumerable<FacultyExpertiseAreaViewModel> FacultyExpertiseAreaViewModel { get; set; }
        public virtual IEnumerable<FacultyProfessionalInterestViewModel> FacultyProfessionalInterestViewModel { get; set; }
        public virtual IEnumerable<FacultyWiseProfileSectionViewModel> FacultyWiseProfileSectionViewModel { get; set; }
    }
}
