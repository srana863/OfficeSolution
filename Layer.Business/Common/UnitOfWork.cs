using Layer.Business.Interfaces;
using Layer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Business.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUsersRepository userRepository)
        {
            Users = userRepository;
        }
        public IUsersRepository Users { get; }
    }
}
