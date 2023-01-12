using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Institute;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.ViewModel.Institute;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Institute
{
    internal class EmployeeWiseProfileSectionRepository : DataCommon, IEmployeeWiseProfileSectionRepository
    {
        public EmployeeWiseProfileSectionRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(EmployeeWiseProfileSection entity)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int instituteId)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Delete(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { SL = id, InstituteId = instituteId }).Single();
        }

        public EmployeeWiseProfileSection Get(int id, int instituteId)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Select(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeWiseProfileSection>(query, new { SL = id, InstituteId = instituteId }).FirstOrDefault();
        }

        public IEnumerable<EmployeeWiseProfileSection> GetAll(int instituteId)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeWiseProfileSection>(query, new { InstituteId = instituteId });
        }

        public IEnumerable<EmployeeWiseProfileSectionViewModel> GetEmployeeWiseProfileSectionDetails(int EmployeeId, int instituteId)
        {
            int? id = 0;
            if (instituteId == 0)
            {
                id = null;
            }
            else
            {
                id = instituteId;
            }
            var query = @"SELECT FPS.SL,FPS.EmployeeId,FPS.ProfileSectionId,FPS.InstituteId,FPS.ProfileSectionDetails,FPS.IsActive,FPS.AddedByUserId,FPS.AddedDate,FPS.UpdatedByUserId,FPS.UpdatedDate,PS.ProfileSectionTitle
                FROM Institute.EmployeeWiseProfileSection FPS
                INNER JOIN Institute.ProfileSection PS ON PS.ProfileSectionId=FPS.ProfileSectionId AND PS.InstituteId=FPS.InstituteId
                WHERE FPS.EmployeeId=@EmployeeId AND FPS.InstituteId=ISNULL(@InstituteId,FPS.InstituteId) AND FPS.IsActive=1";
            return _dbContext._connection.Query<EmployeeWiseProfileSectionViewModel>(query, new { EmployeeId = EmployeeId, InstituteId = id });
        }

        public EmployeeWiseProfileSection GetProfileSectionDetails(int profileSectionId, int EmployeeId, int instituteId)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Select(o => o.ProfileSectionId == o.ProfileSectionId && o.EmployeeId == o.EmployeeId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<EmployeeWiseProfileSection>(query, new { ProfileSectionId = profileSectionId, EmployeeId = EmployeeId, InstituteId = instituteId }).FirstOrDefault();
        }

        public int Update(EmployeeWiseProfileSection entity)
        {
            var query = CRUD<EmployeeWiseProfileSection>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
