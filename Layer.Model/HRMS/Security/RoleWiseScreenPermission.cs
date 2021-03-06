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

    [Table("RoleWiseScreenPermission", "Security")]
    public partial class RoleWiseScreenPermission
    {
        [PKey]
        public int PermissionSL { get; set; }
        public int RoleId { get; set; }
        public int OrgId { get; set; }
        public int ScreenCode { get; set; }
        public bool CanAdd { get; set; }
        public bool CanModify { get; set; }
        public bool CanView { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        public virtual Screen Screen { get; set; }
        public virtual UserRoles UserRole { get; set; }
    }
}
