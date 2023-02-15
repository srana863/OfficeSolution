using Layer.Model.HRMS.Nothi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Nothi
{
    public class NothiMovementViewModel: NothiMovement
    {
        public string DeptName { get; set; }
        public string DeptNameBangla { get; set; }
        public string NothiTypeName { get; set; }

        public string NothiTypeNameBang { get; set; }
        public string SentToDeptName { get; set; }
        public string CurrentDeptName { get; set; }
        public string ReturnFromDeptName { get; set; }

        public string NothiName { get; set; }
        public string NothiNameBang { get; set; }
        public string NothiNumber { get; set; }
        public string NothiNumberBang { get; set; }

    }
}

//NT.NothiTypeName,ND.NothiName,ND.NothiNameBang