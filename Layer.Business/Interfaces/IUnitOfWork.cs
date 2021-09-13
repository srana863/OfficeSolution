using Layer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Business.Interfaces
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
    }
}
