using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Settings
{
    [Table("Users", "Settings")]
    public class Users
    {
        [PKey]
        public int UserId { get; set; }

        public int OrgId { get; set; }

        public string Username { get; set; }

        public string UserFullName { get; set; }

        public string Designation { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public int? RoleId { get; set; }

        public bool IsActive { get; set; }

        public DateTime SetDate { get; set; }
    }
}
