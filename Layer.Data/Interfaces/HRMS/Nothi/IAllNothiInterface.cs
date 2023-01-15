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
    }
    public interface INothiMovementRepository : IGenericRepository<NothiMovement>
    {
    }
    public interface INothiDetailsRepository : IGenericRepository<NothiDetails>
    {
    }
}
