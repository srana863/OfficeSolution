using Layer.Model.ViewModel.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Settings
{
    public class SideNavViewModel
    {
        public SubModuleViewModel SubModuleViewModel { get; set; }
        public IEnumerable<SectionViewModel> SectionViewModels { get; set; }
    }
}
