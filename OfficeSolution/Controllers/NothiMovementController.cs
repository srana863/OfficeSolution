using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.HRMS.Security;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Layer.Model.Common;
using Layer.Model.HRMS.Nothi;
using Layer.Model.ViewModel.Institute;
using System.Text.Json;
using System.Transactions;
using Layer.Model.HRMS.Institute;
using Layer.Model.HRMS.Emp;
using Layer.Model.ViewModel.Nothi;
using Microsoft.AspNetCore.Authorization;
using Layer.Model.Enums;

namespace OfficeSolution.Controllers
{
    [Authorize]
    public class NothiMovementController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public NothiMovementController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        #region Nothi Dashboard.......       
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region NothiMovement.......       
        public IActionResult NothiMovement()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveNothiMovement(NothiMovementViewModel model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    model.DepartmentId = userinfo.DepartmentId;
                    var isSaved = 0;
                    var oldData = new NothiMovement();
                    var nothiMovement = new NothiMovementViewModel();
                    nothiMovement = _unitOfWork.NothiMovementRepository.GetLastNothiMovementByStatus(userinfo.InstituteId, model.NothiId, (int)NothiMovementStatus.Running, model.NothiMovementId);

                    if (nothiMovement == null)
                    {
                        oldData = _unitOfWork.NothiMovementRepository.Get(model.NothiMovementId, userinfo.InstituteId);

                        if (oldData == null)
                        {
                            oldData = new NothiMovement
                            {
                                Status = (int)NothiMovementStatus.Running,
                                IsActive = true,
                                CurrentPositionDepartmentId = model.SendToDepartmentId,
                                SendDate = DateTime.UtcNow.Date,
                                AddedDate = DateTime.UtcNow.Date,
                                AddedByUserId = userinfo.UserId,
                                InstituteId = userinfo.InstituteId,
                                SendToDepartmentId=model.SendToDepartmentId,
                                CommentsWhileSending=model.CommentsWhileSending,
                                DepartmentId=model.DepartmentId,
                                FinancialAmount=model.FinancialAmount,
                                IsFinancial=model.IsFinancial,
                                NothiId=model.NothiId
                            };

                            isSaved = _unitOfWork.NothiMovementRepository.Create(oldData);
                            if (isSaved > 0)
                                oldData.NothiMovementId = isSaved;
                            _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");


                        }
                        else
                        {
                            oldData.UpdatedByUserId = userinfo.UserId;
                            oldData.UpdatedDate = DateTime.UtcNow.Date;
                            oldData.Status = (int)NothiMovementStatus.Running;
                            oldData.CurrentPositionDepartmentId = model.SendToDepartmentId;
                            oldData.SendToDepartmentId = model.SendToDepartmentId;
                            oldData.CommentsWhileSending = model.CommentsWhileSending;
                            oldData.DepartmentId = model.DepartmentId;
                            oldData.FinancialAmount = model.FinancialAmount;
                            oldData.IsFinancial = model.IsFinancial;
                            oldData.NothiId = model.NothiId;
                            isSaved = _unitOfWork.NothiMovementRepository.Update(oldData);

                            _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data updated successfully") : ReturnMessage.SetErrorMessage("Failed to update!");
                        }
                    }
                    else
                    {
                        _vmReturn = ReturnMessage.SetWarningMessage("This Nothi Already Sent To- " + nothiMovement.SentToDeptName.ToString());
                    }

                    if (isSaved > 0) {
                        var nothiMovementDetails = new NothiMovementDetails();
                        nothiMovementDetails = _unitOfWork.NothiMovementDetailsRepository.GetNothiMovementDetailsByMovementId(oldData.NothiMovementId,userinfo.InstituteId);
                        if (nothiMovementDetails == null)
                        {
                            nothiMovementDetails = new NothiMovementDetails();
                            nothiMovementDetails.NothiMovementId=oldData.NothiMovementId;
                            nothiMovementDetails.ComingFromDepartmentId = oldData.DepartmentId;
                            nothiMovementDetails.CurrentDepartmentId= oldData.SendToDepartmentId;
                            nothiMovementDetails.ComingDate= oldData.SendDate;
                            nothiMovementDetails.ComingFromEmployeeId = userinfo.EmployeeId;
                            nothiMovementDetails.IsActive= true;
                            nothiMovementDetails.AddedDate = DateTime.UtcNow;
                            nothiMovementDetails.AddedByUserId= userinfo.UserId;
                            nothiMovementDetails.Status = (int)NothiMovementStatus.Running;
                            isSaved = _unitOfWork.NothiMovementDetailsRepository.Create(nothiMovementDetails);
                        }
                        else {
                            nothiMovementDetails.ComingFromEmployeeId = userinfo.EmployeeId;
                            nothiMovementDetails.IsActive = true;
                            nothiMovementDetails.SendDate = null;
                            nothiMovementDetails.UpdatedDate = DateTime.UtcNow;
                            nothiMovementDetails.UpdatedByUserId = userinfo.UserId;
                            nothiMovementDetails.Status = (int)NothiMovementStatus.Running;
                            isSaved = _unitOfWork.NothiMovementDetailsRepository.Update(nothiMovementDetails);
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


        [HttpGet]
        public JsonResult GetNothiMovement(int nothiMovementId)
        {
            var data = new NothiMovement();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.NothiMovementRepository.Get(nothiMovementId, userinfo.InstituteId);
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

        [HttpPost]
        public JsonResult DeleteNothiMovement(int nothiMovementId)
        {
            try
            {

                var oldData = _unitOfWork.NothiMovementRepository.Get(nothiMovementId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.NothiMovementRepository.Delete(nothiMovementId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Nothi Movement Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Nothi Movement Data found!!");
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


        [HttpGet]
        public IActionResult GetAllNothiMovement(int? departmentId)
        {
            try
            {
                _dbContext.Open();
                int deptid = 0;
                deptid = departmentId > 0 ? departmentId.Value : userinfo.DepartmentId;
                var data = _unitOfWork.NothiMovementRepository.GetAll(userinfo.InstituteId, deptid);
                return PartialView("_GetAllNothiMovement", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllNothiMovement", Enumerable.Empty<NothiMovementViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }

        }
        #endregion

        #region Create Nothi.......
        public IActionResult CreateNothi()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNothiDetails(int sl)
        {
            var data = new NothiDetails();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.NothiDetailsRepository.Get(sl, userinfo.InstituteId);
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

        [HttpPost]
        public JsonResult DeleteNothiDetails(int sl)
        {
            try
            {

                var oldData = _unitOfWork.NothiDetailsRepository.Get(sl, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.NothiDetailsRepository.Delete(sl, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Nothi Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Nothi Data found!!");
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


        [HttpGet]
        public IActionResult GetAllNothi(int? departmentId)
        {
            try
            {
                _dbContext.Open();
                int deptid = 0;
                deptid = departmentId > 0 ? departmentId.Value : userinfo.DepartmentId;
                var data = _unitOfWork.NothiDetailsRepository.GetAll(userinfo.InstituteId, deptid);
                return PartialView("_GetAllNothi", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllNothi", Enumerable.Empty<NothiDetailsViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }

        }
        [HttpPost]
        public JsonResult SaveNothiDetails(NothiDetails model)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldData = new NothiDetails();

                    oldData = _unitOfWork.NothiDetailsRepository.Get(model.SL, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        model.AddedDate = DateTime.UtcNow.Date;
                        model.NothiId = GetNothiId(model.DepartmentId);
                        model.AddedByUserId = userinfo.UserId;
                        model.InstituteId = userinfo.InstituteId;
                        model.NothiCreationDate = DateTime.UtcNow.Date;
                        isSaved = _unitOfWork.NothiDetailsRepository.Create(model);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");
                    }
                    else
                    {
                        oldData = new NothiDetails
                        {
                            AddedByUserId = oldData.AddedByUserId,
                            AddedDate = oldData.AddedDate,
                            InstituteId = oldData.InstituteId,
                            NothiTypeId = model.NothiTypeId,
                            DepartmentId = model.DepartmentId,
                            IsActive = model.IsActive,
                            UpdatedByUserId = userinfo.UserId,
                            UpdatedDate = DateTime.UtcNow.Date,
                            NothiCreationDate = oldData.NothiCreationDate,
                            NothiName = model.NothiName,
                            NothiNameBang = model.NothiNameBang,
                            NothiNumberBang = model.NothiNumberBang,
                            NothiNumber = model.NothiNumber,
                            NothiId = oldData.NothiId,
                            SL = oldData.SL

                        };
                        isSaved = _unitOfWork.NothiDetailsRepository.Update(oldData);

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

        public string GetNothiId(int departmentId)
        {
            var nothiId = string.Empty;
            var data = _unitOfWork.DepartmentRepository.GetNothiId(departmentId, userinfo.InstituteId);

            if (data != null)
            {
                data.DeptSl = data.DeptSl + 1;
                nothiId = data.DeptAnchorName + "-" + data.DeptSl.ToString().PadLeft(4, '0');
            }

            return nothiId;
        }
        #endregion

        #region Nothi Report.......
        public IActionResult NothiReport()
        {
            return View();
        }
        #endregion

        #region Nothi Type.......
        public IActionResult CreateNothiType()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetNothiType(int nothiTypeId)
        {
            var data = new NothiType();
            try
            {
                _dbContext.Open();
                data = _unitOfWork.NothiTypeRepository.Get(nothiTypeId, userinfo.InstituteId);
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

        [HttpPost]
        public JsonResult DeleteNothiType(int nothiTypeId)
        {
            try
            {

                var oldData = _unitOfWork.NothiTypeRepository.Get(nothiTypeId, userinfo.InstituteId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.NothiTypeRepository.Delete(nothiTypeId, userinfo.InstituteId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Nothi Type Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Nothi type Data found!!");
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


        [HttpGet]
        public IActionResult GetAllNothiType(int? departmentId)
        {
            try
            {
                _dbContext.Open();
                int deptid = 0;
                deptid = departmentId > 0 ? departmentId.Value : userinfo.DepartmentId;
                var data = _unitOfWork.NothiTypeRepository.GetAll(userinfo.InstituteId, deptid);
                return PartialView("_GetAllNothiType", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllNothiType", Enumerable.Empty<NothiTypeViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }

        }
        [HttpPost]
        public JsonResult SaveNothiType(NothiType nothiTypeModel)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    _dbContext.Open();
                    var isSaved = 0;
                    var oldData = new NothiType();

                    oldData = _unitOfWork.NothiTypeRepository.Get(nothiTypeModel.NothiTypeId, userinfo.InstituteId);
                    if (oldData == null)
                    {
                        nothiTypeModel.AddedDate = DateTime.UtcNow.Date;
                        nothiTypeModel.AddedByUserId = userinfo.UserId;
                        nothiTypeModel.InstituteId = userinfo.InstituteId;
                        isSaved = _unitOfWork.NothiTypeRepository.Create(nothiTypeModel);
                        _vmReturn = isSaved > 0 ? ReturnMessage.SetSuccessMessage("Data saved successfully") : ReturnMessage.SetErrorMessage("Failed to save!");
                    }
                    else
                    {
                        oldData = new NothiType
                        {
                            AddedByUserId = oldData.AddedByUserId,
                            AddedDate = oldData.AddedDate,
                            InstituteId = oldData.InstituteId,
                            NothiTypeId = oldData.NothiTypeId,
                            IsActive = nothiTypeModel.IsActive,
                            NothiTypeName = nothiTypeModel.NothiTypeName,
                            NothiTypeNameBang = nothiTypeModel.NothiTypeNameBang,
                            UpdatedByUserId = userinfo.UserId,
                            UpdatedDate = DateTime.UtcNow.Date

                        };
                        isSaved = _unitOfWork.NothiTypeRepository.Update(oldData);

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

        #endregion
    }
}
