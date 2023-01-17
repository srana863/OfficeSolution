using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Enums;
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

        public JsonResult GetNothiTypeCombo(int? departmentId)
        {
            int deptid = 0;
            deptid = departmentId > 0 ? departmentId.Value : userinfo.DepartmentId;

            var data = _unitOfWork.NothiTypeRepository.GetAll(userinfo.InstituteId, deptid);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderByDescending(o => o.NothiTypeId);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.NothiTypeId.ToString(),
                Text = o.NothiTypeName.ToString() + "(" + o.NothiTypeNameBang.ToString() + ")"
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetGenderCombo()
        {
            var data = from Gender e in Enum.GetValues(typeof(Gender)) select new { Id = (int)e, Name = e.ToString() };
            var list = data.Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.Name.ToString()
            });
            return Json(list, new JsonSerializerOptions());
        }
        public JsonResult GetProfileSectionCombo()
        {
            var data = _unitOfWork.ProfileSectionRepository.GetAll(userinfo.InstituteId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.ProfileSectionTitle);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.ProfileSectionId.ToString(),
                Text = o.ProfileSectionTitle.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetDepartmentCombo()
        {
            var data = _unitOfWork.DepartmentRepository.GetAll(userinfo.InstituteId);
            var isAuthorize = false;
            switch (userinfo.RoleId)
            {
                case (int)UserRole.SuperAdmin:
                    isAuthorize = true;
                    break;
                case (int)UserRole.Admin:
                    isAuthorize = true;
                    break;
                default:
                    isAuthorize = false;
                    break;

            }
            if (!isAuthorize)
            {
                isAuthorize = userinfo.IsOfficeHead || userinfo.PAOfDeptHead ? true : false;
            }

            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.DeptName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.DepartmentId.ToString(),
                Text = o.DeptName.ToString(),
                Selected = o.DepartmentId == userinfo.DepartmentId,
                Disabled = isAuthorize ? false : true
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }
        public JsonResult GetDesignationCombo()
        {
            var data = _unitOfWork.DesignationRepository.GetAll(userinfo.InstituteId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.DesignationName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.DesignationId.ToString(),
                Text = o.DesignationName.ToString()
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetUserCombo()
        {
            var data = _unitOfWork.UserRepository.GetAll(userinfo.InstituteId);
            if (data != null)
                data = data.Where(o => o.IsActive).OrderBy(o => o.UserFullName);
            var list = data.Select(o => new SelectListItem
            {
                Value = o.RoleId.ToString(),
                Text = o.UserFullName.ToString() + '-' + o.UserName
            });

            return new JsonResult(list, new JsonSerializerOptions());
        }

        public JsonResult GetUserRoleCombo()
        {
            var data = _unitOfWork.UserRolesRepository.GetAll(userinfo.InstituteId);
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
            var data = _unitOfWork.ModulesRepository.GetAll(userinfo.InstituteId);
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
            var data = _unitOfWork.SubModulesRepository.GetAll(userinfo.InstituteId);
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
            var data = _unitOfWork.SubModuleSectionsRepository.GetAll(userinfo.InstituteId);
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
            var data = _unitOfWork.ScreenRepository.GetAll(userinfo.InstituteId);
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
