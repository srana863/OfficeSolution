using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.ViewModel.Recruitment
{
    public class JobListViewModel
    {
        public int JobId { get; set; }
        public int OrgId { get; set; }
        public string SOCCode { get; set; }
        public string JobCode { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int JobTypeId { get; set; }
        public decimal WorkingHour { get; set; }
        public int JobExperienceYear { get; set; }
        public int JobExperienceMonth { get; set; }
        public decimal BasicSalyMin { get; set; }
        public decimal BasicSalyMax { get; set; }
        public int Period { get; set; }
        public int NumberOfVacancies { get; set; }
        public string JobLocation { get; set; }
    }
}
