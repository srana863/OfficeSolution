using Layer.Data.Interfaces;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layer.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository)
        {
            Users = userRepository;
        }
        public IUserRepository Users { get; }
    }
}
