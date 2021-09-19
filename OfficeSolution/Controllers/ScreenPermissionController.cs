using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace OfficeSolution.Controllers
{
    public class ScreenPermissionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public ScreenPermissionController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        #region RoleWiseScreenPermission...
        public IActionResult RoleWiseScreenPermission()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllRoleWiseScreenPermission(int roleId, int? moduleId = null, int? subModuleId = null)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.RoleWiseScreenPermissionRepository.GetAllWithParent(session.UserInfo.OrgId,roleId,moduleId,subModuleId);
                return PartialView("_GetAllRoleWiseScreenPermission", data);
            }
            catch (Exception e)
            {
                return PartialView("_GetAllRoleWiseScreenPermission", Enumerable.Empty<RoleWiseScreenPermission>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpPost]
        public IActionResult SaveRoleWiseScreenPermission(IEnumerable<RoleWiseScreenPermission> roleScreenDetailsViewModel)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    if (roleScreenDetailsViewModel.Any())
                    {
                        _dbContext.Open();
                        var oldData = new RoleWiseScreenPermission();
                        foreach (var item in roleScreenDetailsViewModel)
                        {
                            _returnId = 0;
                            oldData = _unitOfWork.RoleWiseScreenPermissionRepository.Get(item.PermissionSL, session.UserInfo.OrgId);
                            if (oldData == null)
                            {
                                item.CreatedBy = session.UserInfo.UserId;
                                item.CreatedDate = DateTime.UtcNow;
                                item.OrgId = session.UserInfo.OrgId;
                                _returnId = _unitOfWork.RoleWiseScreenPermissionRepository.Create(item);

                            }
                            else
                            {
                                item.OrgId = oldData.OrgId;
                                item.CreatedBy = oldData.CreatedBy;
                                item.CreatedDate = oldData.CreatedDate;
                                item.UpdatedBy = session.UserInfo.UserId;
                                item.UpdatedDate = DateTime.UtcNow;
                                _returnId = _unitOfWork.RoleWiseScreenPermissionRepository.Update(item);
                            }

                        }

                        if (_returnId > 0)
                        {
                            transactionScope.Complete();
                            transactionScope.Dispose();
                            _vmReturn = ReturnMessage.SetSuccessMessage("Operation Completed Successfully!");
                        }

                    }
                    else {
                        _vmReturn = ReturnMessage.SetInfoMessage("No Data Selected!!");
                    }
                    return new JsonResult(_vmReturn, new JsonSerializerOptions());
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    return new JsonResult(ReturnMessage.SetErrorMessage(), new JsonSerializerOptions());
                }
                finally
                {
                    _dbContext.Close();
                }
            }
        }

        [HttpPost]
        public IActionResult DeleteRoleWiseScreenPermissio(int sl)
        {
            try
            {

                var oldData = _unitOfWork.RoleWiseScreenPermissionRepository.Get(sl, session.UserInfo.OrgId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.RoleWiseScreenPermissionRepository.Delete(sl, session.UserInfo.OrgId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Role Wise Screen Permission Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Role Wise Screen Permission Data found!!");
                }

                return new JsonResult(_vmReturn, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(ReturnMessage.SetErrorMessage(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }

        }


        #endregion RoleWiseScreenPermission...


        #region UserWiseOtherScreen...
        public IActionResult UserWiseOtherScreen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUserWiseOtherScreen(int userId, int? moduleId = null, int? subModuleId = null)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.UserWiseOtherScreenRepository.GetAllWithParent(session.UserInfo.OrgId, userId, moduleId, subModuleId);
                return PartialView("_GetAllUserWiseOtherScreen", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllUserWiseOtherScreen", Enumerable.Empty<UserWiseOtherScreen>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetUserWiseOtherScreen(int sl)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.UserWiseOtherScreenRepository.Get(sl, session.UserInfo.OrgId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new UserWiseOtherScreen(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteUserWiseOtherScreen(int sl)
        {
            try
            {

                var oldData = _unitOfWork.UserWiseOtherScreenRepository.Get(sl, session.UserInfo.OrgId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.UserWiseOtherScreenRepository.Delete(sl, session.UserInfo.OrgId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Wise Screen Permission Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No User Wise Screen Permission Data found!!");
                }

                return new JsonResult(_vmReturn, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(ReturnMessage.SetErrorMessage(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpPost]
        public IActionResult SaveUserWiseOtherScreen(IEnumerable<UserWiseOtherScreen> userScreenDetailsViewModel)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    if (userScreenDetailsViewModel.Any())
                    {
                        _dbContext.Open();
                        var oldData = new UserWiseOtherScreen();
                        foreach (var item in userScreenDetailsViewModel)
                        {
                            _returnId = 0;
                            oldData = _unitOfWork.UserWiseOtherScreenRepository.Get(item.SL, session.UserInfo.OrgId);
                            if (oldData == null)
                            {
                                item.CreatedBy = session.UserInfo.UserId;
                                item.CreatedDate = DateTime.UtcNow;
                                item.OrgId = session.UserInfo.OrgId;
                                _returnId = _unitOfWork.UserWiseOtherScreenRepository.Create(item);

                            }
                            else
                            {
                                item.OrgId = oldData.OrgId;
                                item.CreatedBy = oldData.CreatedBy;
                                item.CreatedDate = oldData.CreatedDate;
                                item.UpdatedBy = session.UserInfo.UserId;
                                item.UpdatedDate = DateTime.UtcNow;
                                _returnId = _unitOfWork.UserWiseOtherScreenRepository.Update(item);
                            }

                        }

                        if (_returnId > 0)
                        {
                            transactionScope.Complete();
                            transactionScope.Dispose();
                            _vmReturn = ReturnMessage.SetSuccessMessage("Operation Completed Successfully!");
                        }

                    }
                    else
                    {
                        _vmReturn = ReturnMessage.SetInfoMessage("No Data Selected!!");
                    }
                    return new JsonResult(_vmReturn, new JsonSerializerOptions());
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    return new JsonResult(ReturnMessage.SetErrorMessage(), new JsonSerializerOptions());
                }
                finally
                {
                    _dbContext.Close();
                }
            }

        }
        #endregion UserWiseOtherScreen...
    }
}
