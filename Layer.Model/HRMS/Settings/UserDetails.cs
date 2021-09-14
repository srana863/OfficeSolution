//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Settings
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("UserDetails", "Settings")]
    public partial class UserDetails
    {
        [PKey]
        public long SL { get; set; }
        public long UserId { get; set; }
        public Nullable<int> OrgId { get; set; }
        public Nullable<System.DateTime> LastLogInDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastLockOutDate { get; set; }
        public Nullable<bool> IsLoggedIn { get; set; }
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAttemptWindowStart { get; set; }
        public Nullable<int> FailedPasswordAnswerAttemptCount { get; set; }
        public Nullable<System.DateTime> FailedPasswordAswerAttemptWindowStart { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<byte> CurrentTheme { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
