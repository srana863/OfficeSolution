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

namespace OfficeSolution.Controllers
{
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
        #endregion

        #region Create Nothi.......
        public IActionResult CreateNothi()
        {
            return View();
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
                    _vmReturn = ReturnMessage.SetInfoMessage("No Nothy type Data found!!");
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
                return PartialView("_GetAllNothiType", Enumerable.Empty<NothiType>());
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
                            DepartmentId = nothiTypeModel.DepartmentId,
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
