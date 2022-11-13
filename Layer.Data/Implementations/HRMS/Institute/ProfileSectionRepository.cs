using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
{
    internal class ProfileSectionRepository : DataCommon, IProfileSectionRepository
    {
        public ProfileSectionRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(ProfileSection entity)
        {
            var query = CRUD<ProfileSection>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<ProfileSection>.Delete(o => o.ProfileSectionId == o.ProfileSectionId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { ProfileSectionId = id, InstituteId = instituteId }).Single();
        }

        public ProfileSection Get(int id, int instituteId)
        {
            var query = CRUD<ProfileSection>.Select(o => o.ProfileSectionId == o.ProfileSectionId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<ProfileSection>(query, new { ProfileSectionId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<ProfileSection> GetAll(int instituteId)
        {
            var query = CRUD<ProfileSection>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<ProfileSection>(query, new { InstituteId = instituteId });
        }

        public int Update(ProfileSection entity)
        {
            var query = CRUD<ProfileSection>.Update(o => o.ProfileSectionId == o.ProfileSectionId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
