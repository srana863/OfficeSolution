using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Nothi;
using Layer.Model.ViewModel.Nothi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Data.Interfaces.HRMS.Nothi
{
    public interface INothiTypeRepository : IGenericRepository<NothiType>
    {
        IEnumerable<NothiTypeViewModel> GetAll(int InstituteId,int departmentId);
    }
    public interface INothiMovementDetailsRepository : IGenericRepository<NothiMovementDetails>
    {
        NothiMovementDetails GetNothiMovementDetailsByMovementId(int nothiMovementId, int instituteId);
    }
    public interface INothiMovementRepository : IGenericRepository<NothiMovement>
    {
        IEnumerable<NothiMovementViewModel> GetAll(int InstituteId, int deptId);
        NothiMovementViewModel GetLastNothiMovementByStatus(int InstituteId, string nothiId, int status,int nothiMovementId);
    }
    public interface INothiDetailsRepository : IGenericRepository<NothiDetails>
    {
        IEnumerable<NothiDetailsViewModel> GetAll(int InstituteId, int departmentId);
    }
}
