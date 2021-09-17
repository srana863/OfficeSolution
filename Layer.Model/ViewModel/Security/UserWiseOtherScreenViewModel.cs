using Layer.Model.HRMS.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Security
{
    public class UserWiseOtherScreenViewModel : UserWiseOtherScreen
    {
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int SectionId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string SectionName { get; set; }
        public string ScreenName { get; set; }
    }
}
