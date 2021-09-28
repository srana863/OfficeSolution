using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Settings
{
    public class SectionViewModel
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string IconName { get; set; }
        public ICollection<ScreenViewModel> ScreenViewModels { get; set; }
    }
}
