using Dapper;
using Layer.Data.Common;
using Layer.Data.Interfaces;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.HRMS.Settings
{
    public partial class UsersRepository : DataCommon, IUsersRepository
    {
        public UsersRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(Users model)
        {
            var query = CRUD<Users>.Insert();
            return _dbContext._connection.Query<int>(query, model).Single();
        }

        public int Delete(string userId, string identityCode)
        {
            var query = CRUD<Users>.Delete(o => o.UserId == o.UserId);
            return _dbContext._connection.Query<int>(query, new { UserId = userId }).Single();
        }

        public int Delete(int id, string identityCode)
        {
            throw new NotImplementedException();
        }

        public Users Get(string userId, string identityCode)
        {
            var query = CRUD<Users>.Select(o => o.UserId == o.UserId);
            return _dbContext._connection.Query<Users>(query, new { UserId = userId }).FirstOrDefault();
        }
        public Users GetUserByUserName(string userName, string identityCode)
        {
            var query = CRUD<Users>.Select(o => o.UserName == o.UserName);
            return _dbContext._connection.Query<Users>(query, new { UserName = userName }).FirstOrDefault();
        }


        public Users Get(int id, string identityCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetAll(string identityCode)
        {
            var query = CRUD<Users>.Select();
            return _dbContext._connection.Query<Users>(query);
        }


        public int Update(Users model)
        {
            var query = CRUD<Users>.Update(o => o.UserId == o.UserId);
            return _dbContext._connection.Query<int>(query, model).Single();
        }

        public UserInfoSession GetUserByUserName(string userName)
        {
            var query = CRUD<Users>.Select(o => o.UserName == o.UserName);
            return _dbContext._connection.Query<UserInfoSession>(query, new { UserName = userName }).FirstOrDefault();
        }

        public UserViewModel GetUserProfile(string identityCode, string employeeID)
        {
            var query = @"SELECT EMP.IdentityCode, EMP.EmployeeID, EMP.EmployeeFirstName, EMP.EmployeeLastName, EMP.EmployeeMiddleName, EMP.EmployeeFullName,EMP.EmployeeFullNameBangla
                          ,EMP.EmployeeSex, EMP.EmployeeDOB, EMP.EmployeeReligion, EMP.EmployeeMaritalStatus, EMP.EmployeeJoinDate, EMP.EmployeeNationality
                          ,EMP.EmployeePersonalFileNo ,EMP.DeptHeadYN ,EMP.EmployeeReference ,EMP.EmployeeOtherID ,EMP.EmployeeEnglishProficiency
                          ,EMP.EmployeeNationalID ,EMP.EmployeeBloodGroup ,EMP.SetDate ,EMP.VerifiedYN ,EMP.UserId1 ,EMP.UserId2
	                      ,EMPD.PresentAddress,EMPD.PresentDistrictID,EMPD.PresentThanaID
                          ,EMPD.PresentStateID,EMPD.PresentZipCode,EMPD.PresentPoliceStationID,EMPD.PresentCountry,EMPD.PresentHomeTelePhone
                          ,EMPD.PresentHomeWorkPhone,EMPD.PermanenetAddress,EMPD.PermanentDistrictID,EMPD.PermanentThanaID
                          ,EMPD.PermanentStateID,EMPD.PermanentZipCode,EMPD.PermanentPoliceStationID,EMPD.PermanentCountry
                          ,EMPD.PermanentHomeTelePhone,EMPD.PermanentHomeWorkPhone,EMPD.PersonalEmail,EMPD.WorkEmail
                          ,EMPD.MobileNo,EMPD.UserID
	                      ,EMPS.ConfirmationDate,EMPS.ReviewMonth,EMPS.JobStatusID,EMPS.JobStatusEDate
                          ,EMPS.SectionID,EMPS.DepartmentID,EMPS.DesignationID,EMPS.DesignationEDate,EMPS.EmployeeCategoryID
                          ,EMPS.CategoryEDate,EMPS.AreaID,EMPS.DivisionID,EMPS.TransferEDate
	                      ,A.AreaName, CD.DivisionName
	                      ,EMPDes.DesignationName
	                      ,ISNULL(EMPP.EmployeeImage,(CASE WHEN EMP.EmployeeSex='Male' THEN 'male.jpg' ELSE 'female.jpg' END))EmployeeImage, EMPP.EmployeeCompressImage, ISNULL(EMPP.ImageURL,(CASE WHEN EMP.EmployeeSex='Male' THEN 'Images\EmployeeImages\male.jpg' ELSE 'Images\EmployeeImages\female.jpg' END))ImageURL
						  ,U.UserId,U.AccessLevel,U.UserName,U.UserFullName,U.Password,U.PasswordSalt,U.RoleId,U.SelfServiceCategoryId,U.IsActive,U.IsLockedOut,U.EntryUserId,U.ChangePassword
						  ,UD.LastLogInDate,UD.LastPasswordChangedDate,UD.LastLockOutDate,UD.IsLoggedIn,UD.FailedPasswordAttemptCount,UD.FailedPasswordAttemptWindowStart,UD.FailedPasswordAnswerAttemptCount,UD.FailedPasswordAswerAttemptWindowStart,UD.EmailAddress,UD.CurrentTheme
					  FROM Employee.Employee EMP
                      LEFT JOIN Employee.EmployeeDetails EMPD ON EMPD.EmployeeID = EMP.EmployeeID
                      LEFT JOIN Employee.EmployeeStatus EMPS ON EMPS.EmployeeID = EMP.EmployeeID
                      LEFT JOIN Employee.Designation EMPDes ON EMPDes.DesignationID = EMPS.DesignationID
                      LEFT JOIN Employee.EmployeePicture EMPP ON EMPP.EmployeeID = EMP.EmployeeID
                      LEFT JOIN Employee.Area A ON A.AreaID = EMPS.AreaID
                      LEFT JOIN Employee.CompanyDivision CD ON CD.DivisionID = EMPS.DivisionID
					  LEFT JOIN Settings.Users U ON U.EmployeeID = EMP.EmployeeID
					  LEFT JOIN Settings.UserDetails UD ON UD.UserId = U.UserId
                      WHERE EMP.EmployeeID = @EmployeeID AND EMP.IdentityCode = @IdentityCode";
            return _dbContext._connection.Query<UserViewModel>(query, new { IdentityCode = identityCode, EmployeeID = employeeID }).FirstOrDefault();
        }

        public string GetNewUserId()
        {
            var query = @"SELECT RIGHT('000000000000'+CONVERT(VARCHAR(12),MAX(UserId)+1),12) UserId FROM Settings.Users";
            return _dbContext._connection.Query<string>(query).FirstOrDefault();
        }
    }
}
