using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces.HRMS.Settings
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserInfoSession> GetUserByUserName(string UserName);
    }    
}
