//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Security
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("UserWiseOtherScreen", "Security")]
    public partial class UserWiseOtherScreen
    {
        [PKey]
        public int SL { get; set; }

        public long UserId { get; set; }

        public int OrgId { get; set; }

        public int ScreenCode { get; set; }

        public bool CanAdd { get; set; }

        public bool CanModify { get; set; }

        public bool CanView { get; set; }

        public bool IsActive { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
