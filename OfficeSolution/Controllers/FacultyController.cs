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
    public class FacultyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public FacultyController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFaculty()
        {
            return View();
        }
        public JsonResult SaveProfileSection(FacultyWiseProfileSectionViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldData = new FacultyWiseProfileSection();

                    oldData = _unitOfWork.FacultyWiseProfileSectionRepository.GetProfileSectionDetails(model.ProfileSectionId, model.FacultyId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        oldData = new FacultyWiseProfileSection();

                        oldData.InstituteId = userinfo.InstituteId;
                        oldData.FacultyId = model.FacultyId;
                        oldData.ProfileSectionId = model.ProfileSectionId;
                        oldData.ProfileSectionDetails = model.ProfileSectionDetails;
                        oldData.AddedByUserId = userinfo.UserId;
                        oldData.AddedDate = DateTime.UtcNow.Date;
                        isSaved = _unitOfWork.FacultyWiseProfileSectionRepository.Create(oldData);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Profile section saved successfully") : ReturnMessage.SetErrorMessage("Failed to save profile section!");
                    }
                    else
                    {
                        oldData.ProfileSectionId = model.ProfileSectionId;
                        oldData.ProfileSectionDetails = model.ProfileSectionDetails;
                        oldData.UpdatedByUserId = userinfo.UserId;
                        oldData.UpdatedDate = DateTime.UtcNow;
                        isSaved = _unitOfWork.FacultyWiseProfileSectionRepository.Update(oldData);

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
        public JsonResult SaveFaculty(FacultyViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var isSavedPartial = 0;
                    var oldData = new Faculty();

                    oldData = _unitOfWork.FacultyRepository.Get(model.FacultyId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        oldData = new Faculty
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
                            FacultyFullName = model.FacultyFullName,
                            FirstName = model.FirstName,
                            Gender = model.Gender,
                            Image = model.Image,
                            InstituteId = userinfo.InstituteId,
                            IsActive = model.IsActive,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            Mobile = model.Mobile
                        };
                        isSaved = _unitOfWork.FacultyRepository.Create(oldData);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");
                    }
                    else
                    {
                        oldData = new Faculty
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
                            FacultyFullName = model.FacultyFullName,
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
                            FacultyId = oldData.FacultyId

                        };
                        isSaved = _unitOfWork.FacultyRepository.Update(oldData);

                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data updated successfully") : ReturnMessage.SetErrorMessage("Failed to update!");



                    }

                    if (isSaved > 0)
                    {
                        var profileSection = new FacultyWiseProfileSection();

                        profileSection = _unitOfWork.FacultyWiseProfileSectionRepository.GetProfileSectionDetails(model.ProfileSectionId, model.FacultyId, userinfo.InstituteId);
                        if (profileSection == null)
                        {
                            profileSection = new FacultyWiseProfileSection();

                            profileSection.InstituteId = userinfo.InstituteId;
                            profileSection.FacultyId = model.FacultyId;
                            profileSection.ProfileSectionId = model.ProfileSectionId;
                            profileSection.ProfileSectionDetails = model.ProfileSectionDetails;
                            profileSection.AddedByUserId = userinfo.UserId;
                            profileSection.AddedDate = DateTime.UtcNow.Date;
                            isSavedPartial = _unitOfWork.FacultyWiseProfileSectionRepository.Create(profileSection);
                        }
                        else
                        {
                            profileSection.ProfileSectionDetails = model.ProfileSectionDetails;
                            profileSection.UpdatedByUserId = userinfo.UserId;
                            profileSection.UpdatedDate = DateTime.UtcNow.Date;
                            isSavedPartial = _unitOfWork.FacultyWiseProfileSectionRepository.Update(profileSection);

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

        public JsonResult GetProfileSectionDetails(int profileSectionId, int facultyId)
        {
            var data = new FacultyWiseProfileSection();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.FacultyWiseProfileSectionRepository.GetProfileSectionDetails(profileSectionId, facultyId, userinfo.InstituteId);
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
        public IActionResult GetAllFaculty()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.FacultyRepository.GetAllFaculty(userinfo.InstituteId);
                return PartialView("_GetAllFaculty", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllFaculty", Enumerable.Empty<FacultyViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }
        }

        [HttpGet]
        public JsonResult GetFaculty(int facultyId)
        {
            var data = new Faculty();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.FacultyRepository.Get(facultyId, userinfo.InstituteId);
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
