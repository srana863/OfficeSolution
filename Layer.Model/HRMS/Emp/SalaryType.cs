//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Emp
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("SalaryType", "Emp")]
    public partial class SalaryType
    {
        [PKey]
        public int SalaryTypeId { get; set; }
        public int InstituteId { get; set; }
        public string SalaryTypeName { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
