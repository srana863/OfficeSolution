﻿using Layer.Data.Implementations;
using Layer.Data.Implementations.HRMS.Security;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Security;
using Layer.Model.Common;
using Layer.Model.HRMS.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    [Authorize]
    public class SecurityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        public IActionResult Index()
        {
            return View();
        }

        #region ScreenSection....
        public IActionResult ScreenSetup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllScreen()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ScreenRepository.GetAllWithParent(userinfo.InstituteId);
                return PartialView("_GetAllScreen", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllScreen", Enumerable.Empty<Screen>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetScreen(int screenId)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ScreenRepository.Get(screenId, userinfo.InstituteId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new Screen(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteScreen(int screenId)
        {
            try
            {

                var oldData = _unitOfWork.ScreenRepository.Get(screenId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.ScreenRepository.Delete(screenId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Screen Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Screen Data found!!");
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
        public IActionResult SaveScreen(Screen model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.ScreenRepository.Get(model.ScreenCode, model.InstituteId);
                if (oldData == null)
                {
                    model.AddedByUserId = session.UserInfo.UserId;
                    model.AddedDate = DateTime.UtcNow;
                    model.InstituteId = userinfo.InstituteId;
                    _returnId = _unitOfWork.ScreenRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Screen Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.AddedByUserId = oldData.AddedByUserId;
                    model.AddedDate = oldData.AddedDate;
                    model.UpdatedByUserId = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.ScreenRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Screen Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion SubModuleSection...


        #region SubModuleSection....
        public IActionResult SubModuleSectionSetup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllSubModuleSections()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.SubModuleSectionsRepository.GetAllWithParent(userinfo.InstituteId);
                return PartialView("_GetAllSubModuleSections", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllSubModuleSections", Enumerable.Empty<SubModuleSections>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetSubModuleSections(int sectionId)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.SubModuleSectionsRepository.Get(sectionId, userinfo.InstituteId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new SubModuleSections(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteSubModuleSections(int sectionId)
        {
            try
            {

                var oldData = _unitOfWork.SubModuleSectionsRepository.Get(sectionId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.SubModuleSectionsRepository.Delete(sectionId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Section Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Sub Module Section Data found!!");
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
        public IActionResult SaveSubModuleSections(SubModuleSections model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.SubModuleSectionsRepository.Get(model.SectionId, model.InstituteId);
                if (oldData == null)
                {
                    model.AddedByUserId = session.UserInfo.UserId;
                    model.AddedDate = DateTime.UtcNow;
                    model.InstituteId = userinfo.InstituteId;
                    _returnId = _unitOfWork.SubModuleSectionsRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Section Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.AddedByUserId = oldData.AddedByUserId;
                    model.AddedDate = oldData.AddedDate;
                    model.UpdatedByUserId = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.SubModuleSectionsRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Section Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion SubModuleSection...


        #region SubModule....
        public IActionResult SubModuleSetup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllSubModules()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.SubModulesRepository.GetAllWithParent(userinfo.InstituteId);
                return PartialView("_GetAllSubModules", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllSubModules", Enumerable.Empty<SubModules>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetSubModules(int subModuleId)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.SubModulesRepository.Get(subModuleId, userinfo.InstituteId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new SubModules(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteSubModules(int subModuleId)
        {
            try
            {

                var oldData = _unitOfWork.SubModulesRepository.Get(subModuleId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.SubModulesRepository.Delete(subModuleId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Sub Module Data found!!");
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
        public IActionResult SaveSubModules(SubModules model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.SubModulesRepository.Get(model.SubModuleId, model.InstituteId);
                if (oldData == null)
                {
                    model.AddedByUserId = session.UserInfo.UserId;
                    model.AddedDate = DateTime.UtcNow;
                    model.InstituteId = userinfo.InstituteId;
                    _returnId = _unitOfWork.SubModulesRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.AddedByUserId = oldData.AddedByUserId;
                    model.AddedDate = oldData.AddedDate;
                    model.UpdatedByUserId = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.SubModulesRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Sub Module Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion SubModule...

        #region Module....
        public IActionResult ModuleSetup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllModules()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ModulesRepository.GetAll(userinfo.InstituteId);
                return PartialView("_GetAllModules", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllModules", Enumerable.Empty<Modules>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetModules(int moduleId)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ModulesRepository.Get(moduleId, userinfo.InstituteId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new Modules(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteModules(int moduleId)
        {
            try
            {

                var oldData = _unitOfWork.ModulesRepository.Get(moduleId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.ModulesRepository.Delete(moduleId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Module Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Module Data found!!");
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
        public IActionResult SaveModules(Modules model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.ModulesRepository.Get(model.ModuleId, model.InstituteId);
                if (oldData == null)
                {
                    model.AddedByUserId = session.UserInfo.UserId;
                    model.AddedDate = DateTime.UtcNow;
                    model.InstituteId = userinfo.InstituteId;
                    _returnId = _unitOfWork.ModulesRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Module Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.AddedByUserId = oldData.AddedByUserId;
                    model.AddedDate = oldData.AddedDate;
                    model.UpdatedByUserId = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.ModulesRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Module Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion Module...

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
                var data = _unitOfWork.UserRolesRepository.GetAll(userinfo.InstituteId);
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
                var data = _unitOfWork.UserRolesRepository.Get(roleId, userinfo.InstituteId);

                return new JsonResult(data, new JsonSerializerOptions ());
            }
            catch (Exception)
            {
                return new JsonResult(new UserRoles(), new JsonSerializerOptions());
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

                var oldData = _unitOfWork.UserRolesRepository.Get(roleId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.UserRolesRepository.Delete(roleId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No User Role Data found!!");
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
        public IActionResult SaveUserRoles(UserRoles model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.UserRolesRepository.Get(model.RoleId, model.InstituteId);
                if (oldData == null)
                {
                    model.AddedByUserId = session.UserInfo.UserId;
                    model.AddedDate = DateTime.UtcNow;
                    model.InstituteId = userinfo.InstituteId;
                    _returnId = _unitOfWork.UserRolesRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.AddedByUserId = oldData.AddedByUserId;
                    model.AddedDate = oldData.AddedDate;
                    model.UpdatedByUserId = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.UserRolesRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("User Role Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion user role...
    }
}
