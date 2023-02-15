using Dapper;
using Layer.Data.Helpers;
using Layer.Data.Interfaces.HRMS.Nothi;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Nothi;
using Layer.Model.HRMS.Security;
using Layer.Model.ViewModel.Nothi;
using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Layer.Data.Implementations.HRMS.Nothi
{
    public class NothiMovementRepository : DataCommon, INothiMovementRepository
    {
        public NothiMovementRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NothiMovement entity)
        {
            var query = CRUD<NothiMovement>.Insert();
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
        public int Delete(int id, int InstituteId)
        {
            var query = CRUD<NothiMovement>.Delete(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, new { NothiMovementId = id, InstituteId = InstituteId }).Single();
        }
        
        public NothiMovementViewModel GetLastNothiMovementByStatus(int InstituteId, string nothiId, int status,int nothiMovementId)
        {
            var query = @"SELECT NM.NothiMovementId,NM.InstituteId,NM.NothiId,NM.DepartmentId, NM.SendToDepartmentId, NM.CurrentPositionDepartmentId,NM.ReturnFromDepartmentId,NM.CommentsWhileSending,NM.CommentsWhileReceiving,NM.IsFinancial,NM.FinancialAmount,NM.SendDate,NM.ReturnDate,NM.Status,NM.IsActive,NM.AddedByUserId,NM.AddedDate,NM.UpdatedByUserId,NM.UpdatedDate,
            NT.NothiTypeName,ND.NothiName,ND.NothiNameBang,ND.NothiNumber,ND.NothiNumberBang,D.DeptName,D.DeptNameBangla,D1.DeptName SentToDeptName,D2.DeptName CurrentDeptName,D3.DeptName ReturnFromDeptName
            FROM Nothi.NothiMovement NM
            INNER JOIN Nothi.NothiDetails ND ON ND.NothiId=NM.NothiId AND ND.InstituteId=NM.InstituteId
            INNER JOIN Nothi.NothiType NT ON NT.NothiTypeId=ND.NothiTypeId
            LEFT JOIN Institute.Department D ON D.DepartmentId=NM.DepartmentId
            LEFT JOIN Institute.Department D1 ON D1.DepartmentId=NM.SendToDepartmentId
            LEFT JOIN Institute.Department D2 ON D2.DepartmentId=NM.CurrentPositionDepartmentId
            LEFT JOIN Institute.Department D3 ON D3.DepartmentId=NM.ReturnFromDepartmentId
            WHERE NM.NothiMovementId!=@NothiMovementId
            AND NM.NothiId=@NothiId
            AND NM.Status=@Status
            AND NM.InstituteId=@InstituteId";
            return _dbContext._connection.Query<NothiMovementViewModel>(query, new { NothiMovementId= nothiMovementId, NothiId=nothiId, Status = status, InstituteId = InstituteId }).FirstOrDefault();
        }
        public NothiMovement Get(int id, int InstituteId)
        {
            var query = CRUD<NothiMovement>.Select(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiMovement>(query, new { NothiMovementId = id, InstituteId = InstituteId }).FirstOrDefault();
        }

        public IEnumerable<NothiMovement> GetAll(int InstituteId)
        {
            var query = CRUD<NothiMovement>.Select(o => o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<NothiMovement>(query, new { InstituteId = InstituteId });
        }

        public IEnumerable<NothiMovementViewModel> GetAll(int InstituteId,int deptId)
        {
            var query = @"SELECT NM.NothiMovementId,NM.InstituteId,NM.NothiId,NM.DepartmentId, NM.SendToDepartmentId, NM.CurrentPositionDepartmentId,NM.ReturnFromDepartmentId,NM.CommentsWhileSending,NM.CommentsWhileReceiving,NM.IsFinancial,NM.FinancialAmount,NM.SendDate,NM.ReturnDate,NM.Status,NM.IsActive,NM.AddedByUserId,NM.AddedDate,NM.UpdatedByUserId,NM.UpdatedDate,
            NT.NothiTypeName,ND.NothiName,ND.NothiNameBang,ND.NothiNumber,ND.NothiNumberBang,D.DeptName,D.DeptNameBangla,D1.DeptName SentToDeptName,D2.DeptName CurrentDeptName,D3.DeptName ReturnFromDeptName
            FROM Nothi.NothiMovement NM
            INNER JOIN Nothi.NothiDetails ND ON ND.NothiId=NM.NothiId AND ND.InstituteId=NM.InstituteId
            INNER JOIN Nothi.NothiType NT ON NT.NothiTypeId=ND.NothiTypeId
            LEFT JOIN Institute.Department D ON D.DepartmentId=NM.DepartmentId
            LEFT JOIN Institute.Department D1 ON D1.DepartmentId=NM.SendToDepartmentId
            LEFT JOIN Institute.Department D2 ON D2.DepartmentId=NM.CurrentPositionDepartmentId
            LEFT JOIN Institute.Department D3 ON D3.DepartmentId=NM.ReturnFromDepartmentId
            WHERE NM.DepartmentId=@DepartmentId
            AND NM.InstituteId=@InstituteId";
            return _dbContext._connection.Query<NothiMovementViewModel>(query, new { InstituteId = InstituteId, DepartmentId = deptId });
        }

        public int Update(NothiMovement entity)
        {
            var query = CRUD<NothiMovement>.Update(o => o.NothiMovementId == o.NothiMovementId && o.InstituteId == o.InstituteId);
            return _dbContext._connection.Query<int>(query, entity).Single();
        }
    }
}
