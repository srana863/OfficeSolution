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
    internal class FacultyWiseProfileSectionRepository : DataCommon, IFacultyWiseProfileSectionRepository
    {
        public FacultyWiseProfileSectionRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(FacultyWiseProfileSection entity)
        {
            var query = CRUD<FacultyWiseProfileSection>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<FacultyWiseProfileSection>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public FacultyWiseProfileSection Get(int id, int instituteId)
        {
            var query = CRUD<FacultyWiseProfileSection>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyWiseProfileSection>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<FacultyWiseProfileSection> GetAll(int instituteId)
        {
            var query = CRUD<FacultyWiseProfileSection>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyWiseProfileSection>(query, new { InstituteId = instituteId });
        }

        public int Update(FacultyWiseProfileSection entity)
        {
            var query = CRUD<FacultyWiseProfileSection>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
