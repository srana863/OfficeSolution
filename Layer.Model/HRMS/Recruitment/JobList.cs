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

    [Table("JobList", "Recruitment")]
    public partial class JobList
    {
        [PKey]
        public int JobId { get; set; }
        public int OrgId { get; set; }
        public string SOCCode { get; set; }
        public string JobCode { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public int JobTypeId { get; set; }
        public decimal WorkingHours { get; set; }
        public int JobExperienceYear { get; set; }
        public int JobExperienceMonth { get; set; }
        public decimal BasicSalaryMin { get; set; }
        public decimal BasicSalaryMax { get; set; }
        public Nullable<int> Period { get; set; }
        public Nullable<int> NumberOfVacancies { get; set; }
        public string JobLocation { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
