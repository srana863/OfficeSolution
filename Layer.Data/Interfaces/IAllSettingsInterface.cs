using Layer.Data.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Data.Interfaces
{
    public interface IUsersRepository : ICommonDataMethods<Users>
    {
        UserInfoSession GetUserByUserName(string userName);
        UserViewModel GetUserProfile(string identityCode, string employeeID);
        Users GetUserByUserName(string userName, string identityCode);
        string GetNewUserId();
    }
}
