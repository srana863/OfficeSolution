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

    [Table("SubModule", "Security")]
    public partial class SubModule
    {
        [PKey]
        public int SubModuleId { get; set; }
        public int ModuleId { get; set; }
        public string SubModuleName { get; set; }
        public string AreaName { get; set; }
        public Nullable<System.DateTime> SetDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IconName { get; set; }
    }
}