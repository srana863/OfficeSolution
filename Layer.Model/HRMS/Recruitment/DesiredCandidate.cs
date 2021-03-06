//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Recruitment
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("DesiredCandidate", "Recruitment")]
    public partial class DesiredCandidate
    {
        [PKey]
        public int JobId { get; set; }
        public int OrgId { get; set; }
        public string Qualifications { get; set; }
        public int SkillSetId { get; set; }
        public decimal AgeFrom { get; set; }
        public decimal AgeTo { get; set; }
        public int Gender { get; set; }
        public string AuthorisingOfficerId { get; set; }
        public Nullable<bool> IsThisNewRole { get; set; }
        public string LanguageRequirements { get; set; }
        public int JobStatusId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
