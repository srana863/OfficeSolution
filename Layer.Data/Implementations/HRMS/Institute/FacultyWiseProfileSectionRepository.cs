﻿using Dapper;
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

        public IEnumerable<FacultyWiseProfileSectionViewModel> GetFacultyWiseProfileSectionDetails(int facultyid, int instituteId)
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
            var query = @"SELECT FPS.SL,FPS.FacultyId,FPS.ProfileSectionId,FPS.InstituteId,FPS.ProfileSectionDetails,FPS.IsActive,FPS.AddedByUserId,FPS.AddedDate,FPS.UpdatedByUserId,FPS.UpdatedDate,PS.ProfileSectionTitle
                FROM Institute.FacultyWiseProfileSection FPS
                INNER JOIN Institute.ProfileSection PS ON PS.ProfileSectionId=FPS.ProfileSectionId AND PS.InstituteId=FPS.InstituteId
                WHERE FPS.FacultyId=@FacultyId AND FPS.InstituteId=ISNULL(@InstituteId,FPS.InstituteId) AND FPS.IsActive=1";
            return _dbContext._connection.Query<FacultyWiseProfileSectionViewModel>(query, new { FacultyId=facultyid, InstituteId = id });
        }

        public FacultyWiseProfileSection GetProfileSectionDetails(int profileSectionId, int facultyId, int instituteId)
        {
            var query = CRUD<FacultyWiseProfileSection>.Select(o => o.ProfileSectionId == o.ProfileSectionId && o.FacultyId == o.FacultyId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<FacultyWiseProfileSection>(query, new { ProfileSectionId = profileSectionId, FacultyId = facultyId, InstituteId = instituteId }).FirstOrDefault();
        }

        public int Update(FacultyWiseProfileSection entity)
        {
            var query = CRUD<FacultyWiseProfileSection>.Update(o => o.SL == o.SL && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
