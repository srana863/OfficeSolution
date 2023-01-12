using Layer.Model.Enums;
using Layer.Model.HRMS.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Settings
{
    [Table("Users", "Setting")]
    public partial class User
    {
        [PKey]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
        public int InstituteId { get; set; }
        public int EmployeeId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
