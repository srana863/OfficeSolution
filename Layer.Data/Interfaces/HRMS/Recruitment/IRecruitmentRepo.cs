using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Interfaces.HRMS.Recruitment
{
    public interface IRecruitmentRepo : IGenericRepository<JobList>
    {
        IEnumerable<string> GetSOC(int orgId);
    }
}
