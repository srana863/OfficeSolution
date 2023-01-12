using Layer.Model.HRMS.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.ViewModel.Institute;
using Layer.Model.HRMS.Institute;
using System.Collections.Generic;
using System.Text.Json;
using Layer.Model.Common;
using System.Transactions;
using System.Reflection;

namespace OfficeSolution.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveProfileSection(EmployeeWiseProfileSectionViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldData = new EmployeeWiseProfileSection();

                    oldData = _unitOfWork.EmployeeWiseProfileSectionRepository.GetProfileSectionDetails(model.ProfileSectionId, model.EmployeeId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        oldData = new EmployeeWiseProfileSection();

                        oldData.InstituteId = userinfo.InstituteId;
                        oldData.EmployeeId = model.EmployeeId;
                        oldData.ProfileSectionId = model.ProfileSectionId;
                        oldData.ProfileSectionDetails = model.ProfileSectionDetails;
                        oldData.IsActive = true;
                        oldData.AddedByUserId = userinfo.UserId;
                        oldData.AddedDate = DateTime.UtcNow.Date;
                        isSaved = _unitOfWork.EmployeeWiseProfileSectionRepository.Create(oldData);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Profile section saved successfully") : ReturnMessage.SetErrorMessage("Failed to save profile section!");
                    }
                    else
                    {
                        oldData.ProfileSectionId = model.ProfileSectionId;
                        oldData.ProfileSectionDetails = model.ProfileSectionDetails;
                        oldData.UpdatedByUserId = userinfo.UserId;
                        oldData.UpdatedDate = DateTime.UtcNow;
                        isSaved = _unitOfWork.EmployeeWiseProfileSectionRepository.Update(oldData);

                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Profile section updated successfully") : ReturnMessage.SetErrorMessage("Failed to update profile section!");

                    }

                    if (isSaved > 0)
                    {
                        transactionScope.Complete();
                        transactionScope.Dispose();
                    }
                    return new JsonResult(_vmReturn, new JsonSerializerOptions());

                }
                catch (Exception e)
                {
                    _vmReturn = ReturnMessage.SetErrorMessage(e.Message);
                    transactionScope.Dispose();
                    return new JsonResult(_vmReturn, new JsonSerializerOptions()); ;
                }
                finally
                {
                    _dbContext.Close();
                }
            }
        }

        [HttpPost]
        public JsonResult SaveEmployee(EmployeeViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var isSavedPartial = 0;
                    var oldData = new Employee();

                    oldData = _unitOfWork.EmployeeRepository.Get(model.EmployeeId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        oldData = new Employee
                        {
                            Email = model.Email,
                            About = model.About,
                            AddedByUserId = userinfo.UserId,
                            AddedDate = DateTime.UtcNow.Date,
                            Address = model.Address,
                            DateOfBirth = model.DateOfBirth,
                            DepartmentId = model.DepartmentId,
                            DesignationId = model.DesignationId,
                            Education = model.Education,
                            Experience = model.Experience,
                            EmployeeFullName = model.EmployeeFullName,
                            FirstName = model.FirstName,
                            Gender = model.Gender,
                            Image = model.Image,
                            InstituteId = userinfo.InstituteId,
                            IsActive = model.IsActive,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            Mobile = model.Mobile,
                            IsOfficeHead=model.IsOfficeHead,
                            PAOfDeptHead=model.PAOfDeptHead
                        };
                        isSaved = _unitOfWork.EmployeeRepository.Create(oldData);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");
                    }
                    else
                    {
                        oldData = new Employee
                        {
                            Email = model.Email,
                            About = model.About,
                            AddedByUserId = oldData.AddedByUserId,
                            AddedDate = oldData.AddedDate,
                            Address = model.Address,
                            DateOfBirth = model.DateOfBirth,
                            DepartmentId = model.DepartmentId,
                            DesignationId = model.DesignationId,
                            Education = model.Education,
                            Experience = model.Experience,
                            EmployeeFullName = model.EmployeeFullName,
                            FirstName = model.FirstName,
                            Gender = model.Gender,
                            Image = model.Image,
                            InstituteId = oldData.InstituteId,
                            IsActive = model.IsActive,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            Mobile = model.Mobile,
                            UpdatedByUserId = userinfo.UserId,
                            UpdatedDate = DateTime.UtcNow,
                            EmployeeId = oldData.EmployeeId,
                            IsOfficeHead = model.IsOfficeHead,
                            PAOfDeptHead = model.PAOfDeptHead

                        };
                        isSaved = _unitOfWork.EmployeeRepository.Update(oldData);

                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data updated successfully") : ReturnMessage.SetErrorMessage("Failed to update!");



                    }

                    if (isSaved > 0)
                    {
                        var profileSection = new EmployeeWiseProfileSection();

                        profileSection = _unitOfWork.EmployeeWiseProfileSectionRepository.GetProfileSectionDetails(model.ProfileSectionId, model.EmployeeId, userinfo.InstituteId);
                        if (profileSection == null)
                        {
                            profileSection = new EmployeeWiseProfileSection();

                            profileSection.InstituteId = userinfo.InstituteId;
                            profileSection.EmployeeId = model.EmployeeId;
                            profileSection.ProfileSectionId = model.ProfileSectionId;
                            profileSection.ProfileSectionDetails = model.ProfileSectionDetails;
                            profileSection.AddedByUserId = userinfo.UserId;
                            profileSection.AddedDate = DateTime.UtcNow.Date;
                            isSavedPartial = _unitOfWork.EmployeeWiseProfileSectionRepository.Create(profileSection);
                        }
                        else
                        {
                            profileSection.ProfileSectionDetails = model.ProfileSectionDetails;
                            profileSection.UpdatedByUserId = userinfo.UserId;
                            profileSection.UpdatedDate = DateTime.UtcNow.Date;
                            isSavedPartial = _unitOfWork.EmployeeWiseProfileSectionRepository.Update(profileSection);

                        }
                    }
                    if (isSaved > 0)
                    {
                        transactionScope.Complete();
                        transactionScope.Dispose();
                    }
                    return new JsonResult(_vmReturn, new JsonSerializerOptions());

                }
                catch (Exception e)
                {
                    _vmReturn = ReturnMessage.SetErrorMessage(e.Message);
                    transactionScope.Dispose();
                    return new JsonResult(_vmReturn, new JsonSerializerOptions()); ;
                }
                finally
                {
                    _dbContext.Close();
                }
            }
        }

        public JsonResult GetProfileSectionDetails(int profileSectionId, int EmployeeId)
        {
            var data = new EmployeeWiseProfileSection();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.EmployeeWiseProfileSectionRepository.GetProfileSectionDetails(profileSectionId, EmployeeId, userinfo.InstituteId);
                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(data, new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }
        }


        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.EmployeeRepository.GetAllEmployee(userinfo.InstituteId);
                return PartialView("_GetAllEmployee", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllEmployee", Enumerable.Empty<EmployeeViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }
        }

        [HttpGet]
        public JsonResult GetEmployee(int EmployeeId)
        {
            var data = new Employee();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.EmployeeRepository.Get(EmployeeId, userinfo.InstituteId);
                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(data, new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }
        }


    }
}
