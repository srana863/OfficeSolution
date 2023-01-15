using Layer.Model.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Model.Common
{
    public partial class UserInfoSession : User
    {
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string InstituteName { get; set; }

        public string RoleName { get; set; }
        public string Image { get; set; }

        public bool PAOfDeptHead { get; set; }
        public bool IsOfficeHead { get; set; }
        public int DepartmentId { get; set; }

    }
}
