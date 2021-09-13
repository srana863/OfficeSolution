using Layer.Business.Common;
using Layer.Model.Common;
using Layer.Model.ViewModel;
using Layer.Model.HRMS.Settings;

namespace Layer.Business.Interfaces
{
    #region user.....
    public interface IUsersManager : ICommonBALMethods<Users>
    {
        UserInfoSession Login(string userName);
        ReturnMessage ChangePassword(UserViewModel userViewModel, AppSession session);
        UserViewModel GetUserProfile(string identityCode, string employeeID);
    }
  
    #endregion
}
