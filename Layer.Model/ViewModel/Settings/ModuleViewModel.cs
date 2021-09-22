using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Settings
{
    public class ModuleViewModel
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public ICollection<SubModuleViewModel> SubModuleViewModels { get; set; }
    }
}
