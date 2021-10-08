using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Recruitment;
using Layer.Model.Common;
using Layer.Model.HRMS.Recruitment;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Recruitment
{
    public class RecruitmentRepo : DataCommon, IRecruitmentRepo
    {
        public RecruitmentRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(JobList entity)
        {
            var query = CRUD<JobList>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }

        public int Delete(int id, int orgId)
        {
            var query = CRUD<JobList>.Delete(o => o.JobId == o.JobId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { JobId = id, OrgId = orgId }).Single();
        }

        public JobList Get(int id, int orgId)
        {
            var query = CRUD<JobList>.Select(o => o.JobId == o.JobId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<JobList>(query, new { JobId = id, OrgId = orgId }).FirstOrDefault();
        }

        public IEnumerable<JobList> GetAll(int orgId)
        {
            var query = CRUD<JobList>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<JobList>(query, new { OrgId = orgId });
        }

        public IEnumerable<string> GetSOC(int orgId)
        {
            var query = @"SELECT SOCCode FROM Recruitment.JobList WHERE OrgId = @OrgId";

            return _dbContext._connection.Query<String>(query, new { OrgId = orgId });
        }

        public int Update(JobList entity)
        {
            var query = CRUD<JobList>.Update(o => o.JobId == o.JobId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
