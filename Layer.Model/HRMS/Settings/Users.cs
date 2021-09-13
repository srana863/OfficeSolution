using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Model.HRMS.Settings
{
    [Table("Users", "Settings")]
    public partial class Users
    {
        [PKey]
        public int UserId { get; set; }

        public string IdentityCode { get; set; }

        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string Designation { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int? RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime SetDate { get; set; }

    }
}
