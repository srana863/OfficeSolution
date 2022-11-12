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

        public JsonResult SaveFaculty(FacultyViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldData = new Faculty();

                    oldData = _unitOfWork.FacultyRepository.Get(model.FacultyId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        model.InstituteId = userinfo.InstituteId;
                        model.AddedByUserId = userinfo.UserId;
                        model.AddedDate = DateTime.UtcNow.Date;
                        isSaved = _unitOfWork.FacultyRepository.Create(model);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");
                    }
                    else
                    {
                        model.InstituteId = oldData.InstituteId;
                        model.AddedByUserId = oldData.AddedByUserId;
                        model.AddedDate = oldData.AddedDate;
                        model.UpdatedByUserId = userinfo.UserId;
                        model.UpdatedDate = DateTime.UtcNow;
                        isSaved = _unitOfWork.FacultyRepository.Update(model);

                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data updated successfully") : ReturnMessage.SetErrorMessage("Failed to update!");

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
