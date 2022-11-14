using Layer.Model.HRMS.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Security
{
    public class RoleWiseScreenPermissionViewModel : RoleWiseScreenPermission
    {
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int SectionId { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string SectionName { get; set; }
        public string ScreenName { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string URL { get; set; }
        public string IconName { get; set; }
        public string ScreenOrder { get; set; }

    }
}
