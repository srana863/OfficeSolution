using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        public JsonResult GetUserCombo()
        {
            var data = _unitOfWork.UsersRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.UserFullName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.RoleId.ToString(),
                Text = o.UserFullName.ToString() + '-' +o.Username + '(' + o.Designation + ')'
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetUserRoleCombo()
        {
            var data = _unitOfWork.UserRolesRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.RoleName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.RoleId.ToString(),
                Text = o.RoleName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }
        public JsonResult GetModuleCombo()
        {
            var data = _unitOfWork.ModulesRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.ModuleName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.ModuleId.ToString(),
                Text = o.ModuleName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }
        public JsonResult GetSubModuleCombo(int? moduleId = 0)
        {
            var data = _unitOfWork.SubModulesRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
            {
                if (moduleId > 0)
                {
                    data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId).OrderBy(o => o.SubModuleName);
                }
                else
                {
                    data = data.Where(o => o.IsActive).OrderBy(o => o.SubModuleName);
                }
            }
            var list = data.Select(o => new SelectListItem
            {
                Value = o.SubModuleId.ToString(),
                Text = o.SubModuleName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetSubModuleSectionsCombo(int? moduleId = 0, int? subModuleId = 0)
        {
            var data = _unitOfWork.SubModuleSectionsRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
            {
                if (moduleId > 0)
                {
                    if (subModuleId > 0)
                    {
                        data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId && o.SubModuleId == subModuleId).OrderBy(o => o.SectionName);
                    }
                    else
                    {
                        data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId).OrderBy(o => o.SectionName);
                    }
                }
            }
            var list = data.Select(o => new SelectListItem
            {
                Value = o.SectionId.ToString(),
                Text = o.SectionName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }
        public JsonResult GetScreenCombo(int? moduleId = 0, int? subModuleId = 0, int? sectionId = 0)
        {
            var data = _unitOfWork.ScreenRepository.GetAll(session.UserInfo.OrgId);
            if (data != null)
            {
                if (moduleId > 0)
                {
                    if (subModuleId > 0)
                    {
                        if (sectionId > 0)
                        {
                            data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId && o.SubModuleId == subModuleId && o.SectionId == sectionId).OrderBy(o => o.ScreenName);
                        }
                        else
                        {
                            data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId && o.SubModuleId == subModuleId).OrderBy(o => o.ScreenName);
                        }
                    }
                    else
                    {
                        data = data.Where(o => o.IsActive == true && o.ModuleId == moduleId).OrderBy(o => o.ScreenName);
                    }
                }
            }

            var list = data.Select(o => new SelectListItem
            {
                Value = o.ScreenCode.ToString(),
                Text = o.ScreenName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

    }
}
