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

    [Table("SubModuleSection", "Security")]
    public partial class SubModuleSection
    {
        [PKey]
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string IconName { get; set; }
    }
}