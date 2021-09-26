using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Settings
{
    public class OrganizationProfileRepository : DataCommon, IOrganizationProfileRepository
    {
        public OrganizationProfileRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(OrganizationProfile entity)
        {
            var query = CRUD<OrganizationProfile>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int orgId)
        {
            var query = CRUD<OrganizationProfile>.Delete(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, new { OrgId = id }).Single();
        }

        public OrganizationProfile Get(int id, int orgId)
        {
            var query = CRUD<OrganizationProfile>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrganizationProfile>(query, new { OrgId = id }).FirstOrDefault();
        }

        public IEnumerable<OrganizationProfile> GetAll(int orgId)
        {
            var query = CRUD<OrganizationProfile>.Select(o => o.OrgId == o.OrgId);
            return _dbContext._connection.Query<OrganizationProfile>(query, new { OrgId = orgId });
        }

        public int Update(OrganizationProfile entity)
        {
            var query = CRUD<OrganizationProfile>.Update(o => o.OrgId == o.OrgId && o.OrgId == o.OrgId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}