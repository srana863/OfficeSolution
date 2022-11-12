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
    public class CourseRepository : DataCommon, ICourseRepository
    {
        public CourseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Course entity)
        {
            var query = CRUD<Course>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<Course>.Delete(o => o.CourseId == o.CourseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { CourseId = id, InstituteId = instituteId }).Single();
        }

        public Course Get(int id, int instituteId)
        {
            var query = CRUD<Course>.Select(o => o.CourseId == o.CourseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Course>(query, new { CourseId = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<Course> GetAll(int instituteId)
        {
            var query = CRUD<Course>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<Course>(query, new { InstituteId = instituteId });
        }

        public int Update(Course entity)
        {
            var query = CRUD<Course>.Update(o => o.CourseId == o.CourseId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
