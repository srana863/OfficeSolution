using Layer.Data.Interfaces.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.Common
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
    }
}
