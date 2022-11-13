using Layer.Model.HRMS.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Institute
{
    public partial class FacultyWiseProfileSectionViewModel: FacultyWiseProfileSection
    {
        [NotMapped]
        public string ProfileSectionTitle { get; set; }
    }
}
