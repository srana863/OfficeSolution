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
using System.Threading.Tasks;

namespace Layer.Data.Implementations.HRMS.Settings
{
    public class UserRepository : DataCommon, IUserRepository
    {
        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(User entity)
        {
            var query = CRUD<User>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<User>.Delete(o => o.UserId == o.UserId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { UserId = id, InstituteId = InstituteId }).Single();
        }

        public User Get(int id, int InstituteId)
        {
            var query = CRUD<User>.Select(o => o.UserId == o.UserId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<User>(query, new { UserId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<User> GetAll(int InstituteId)
        {
            var query = CRUD<User>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<User>(query, new { InstituteId = InstituteId });
        }

        public async Task<UserInfoSession> GetUserByUserName(string UserName)
        {
            var query = @"SELECT U.UserId,U.UserName,U.UserEmail,U.UserFullName,U.InstituteId,U.FacultyId,U.Password,U.RoleId,U.IsActive,U.AddedByUserId,U.AddedDate,U.UpdatedByUserId,U.UpdatedDate, D.DeptName DepartmentName, De.DesignationName,R.RoleName,I.InstituteName,ISNULL(F.Image, (CASE WHEN F.Gender=1 Then 'male.png' When F.Gender=2 then 'female.png' else 'other.png' end) )Image
                FROM Setting.Users U
                INNER JOIN Institute.Institute I ON I.InstituteId=U.InstituteId
                INNER JOIN Institute.Faculty F ON F.FacultyId=U.FacultyId AND F.InstituteId=U.InstituteId
                INNER JOIN Institute.Department D ON D.DepartmentId=F.DepartmentId AND D.InstituteId=U.InstituteId
                INNER JOIN Institute.Designation De ON De.DesignationId=F.DesignationId AND De.InstituteId=U.InstituteId
                INNER JOIN Security.UserRoles R ON R.RoleId=U.RoleId AND R.InstituteId=U.InstituteId
                WHERE U.UserName=@UserName";
            return await _dbContext._connection.QueryFirstOrDefaultAsync<UserInfoSession>(query, new { UserName = UserName });
        }

        public int Update(User entity)
        {
            var query = CRUD<User>.Update(o => o.UserId == o.UserId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
