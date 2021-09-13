using Layer.Business.Common;
using Layer.Business.Interfaces;
using Layer.Data.HRMS.Settings;
using Layer.Data.Interfaces;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Layer.Business.HRMS.Settings
{
    public partial class UsersManager : BllCommon, IUsersManager
    {
        private readonly IUsersRepository _usersRepository;
        public UsersManager()
        {
            _usersRepository = new UsersRepository(_dbContext);
        }
        public ReturnMessage Save(Users user, AppSession appSession)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldUser = _usersRepository.Get(user.UserId);
                    if (oldUser == null)
                    {
                        var model = new Users
                        {
                        };
                        isSaved = _usersRepository.Create(model);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage() : ReturnMessage.SetErrorMessage();
                    }
                    else
                    {
                        var model = new Users
                        {
                        };
                        isSaved = _usersRepository.Update(model);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage() : ReturnMessage.SetErrorMessage();
                    }
                    if (isSaved > 0)
                    {
                        transactionScope.Complete();
                        transactionScope.Dispose();
                    }
                    return _vmReturn;
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    return _vmReturn;
                }
                finally
                {
                    _dbContext.Close();
                }
            }
        }
        public ReturnMessage Delete(int id, string identityCode)
        {
            throw new NotImplementedException();
        }
        public Users Get(int id, string identityCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetAll(string identityCode)
        {
            throw new NotImplementedException();
        }

        public UserInfoSession Login(string userName)
        {
            try
            {
                _dbContext.Open();
                var appSession = new AppSession();
                var hasUser = _usersRepository.GetUserByUserName(userName);
                return hasUser;
            }
            catch (Exception e)
            {
                return new UserInfoSession();
            }
            finally
            {
                _dbContext.Close();
            }
        }

        public ReturnMessage Delete(string id, string identityCode)
        {
            throw new NotImplementedException();
        }

        public Users Get(string id, string identityCode)
        {
            throw new NotImplementedException();
        }

        public ReturnMessage ChangePassword(UserViewModel userViewModel, AppSession session)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldUser = _usersRepository.Get(session.UserInfo.UserId, session.UserInfo.IdentityCode);
                    if (oldUser != null)
                    {
                        if (userViewModel.OldPassword == DataEncryptionUtilities.GenerateDecryptedString(oldUser.Password))
                        {
                            if (userViewModel.ConfirmNewPassword == userViewModel.Password)
                            {
                                oldUser.Password = DataEncryptionUtilities.GenerateEncryptedString(userViewModel.Password);
                                isSaved = _usersRepository.Update(oldUser);
                                _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Password has been changed! Please login again to continue!") : ReturnMessage.SetErrorMessage("Failed to change password!");
                            }
                            else
                            {
                                _vmReturn = ReturnMessage.SetWarningMessage("New password and confirm password doesn't match!");
                            }
                        }
                        else
                        {
                            _vmReturn = ReturnMessage.SetWarningMessage("Old password doesn't match!");
                        }
                    }
                    else
                    {
                        _vmReturn = ReturnMessage.SetWarningMessage("User not found!");
                    }
                    if (isSaved > 0)
                    {
                        transactionScope.Complete();
                        transactionScope.Dispose();
                    }
                    return _vmReturn;
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    return _vmReturn;
                }
                finally
                {
                    _dbContext.Close();
                }
            }
        }

        public UserViewModel GetUserProfile(string identityCode, string employeeID)
        {
            var userObj = new UserViewModel();
            try
            {
                _dbContext.Open();
                var data = _usersRepository.GetUserProfile(identityCode, employeeID);
                if (data != null)
                    return data;
                return userObj;
            }
            catch (Exception e)
            {
                return userObj;
            }
            finally
            {
                _dbContext.Close();
            }
        }
    }
}
