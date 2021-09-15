using Layer.Data.Implementations.HRMS.Security;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly IUserRolesRepository _userRolesRepository;
        public SecurityController()
        {
            _userRolesRepository = new UserRolesRepository(_dbContext);
        }

        #region User Role....
        public IActionResult UserRolesSetup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUserRoles()
        {
            try
            {
                _dbContext.Open();
                var data = _userRolesRepository.GetAll(session.UserInfo.OrgId);
                return PartialView("_GetAllUserRoles", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllUserRoles", Enumerable.Empty<UserRoles>());
            }
            finally
            {
                _dbContext.Close();
            }
         
        }

        [HttpGet]
        public IActionResult GetUserRoles(int roleId)
        {
            try
            {
                _dbContext.Open();
                var data = _userRolesRepository.Get(roleId, session.UserInfo.OrgId);
                return Json(data);
            }
            catch (Exception)
            {
                return Json(new UserRoles());
            }
            finally
            {
                _dbContext.Close();
            }
          
           
        }
        [HttpPost]
        public IActionResult DeleteUserRoles(int roleId)
        {
            try
            {
                
                var oldData = _userRolesRepository.Get(roleId,session.UserInfo.OrgId);
                if (oldData != null)
                {
                    _returnId = _userRolesRepository.Delete(roleId, session.UserInfo.OrgId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else {
                    _vmReturn = ReturnMessage.SetInfoMessage("No User Role Data found!!");
                }
                
                return Json(_vmReturn);
            }
            catch (Exception)
            {
               return Json(ReturnMessage.SetErrorMessage());
            }
            finally
            {
                _dbContext.Close();
            }
            
        }

        [HttpPost]
        public IActionResult SaveUserRoles(UserRoles model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _userRolesRepository.Get(model.RoleId, model.OrgId);
                if (oldData == null)
                {
                    model.CreatedBy = session.UserInfo.UserId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.OrgId = session.UserInfo.OrgId;
                    _returnId = _userRolesRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.UpdatedBy = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _userRolesRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Updated!!") : ReturnMessage.SetErrorMessage();
                }
                return Json(_vmReturn);
            }
            catch (Exception)
            {
                return Json(ReturnMessage.SetErrorMessage());
            }
            finally {
                _dbContext.Close();
            }

        }
        #endregion user role...
    }
}
