using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrganizationController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        public IActionResult Profile()
        {
            return View();
        }

        #region DocumentType....
        public IActionResult DocumentType()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllDocumentType()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.DocumentTypeRepository.GetAllWithParent(session.UserInfo.OrgId);
                return PartialView("_GetAllDocumentType", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllDocumentType", Enumerable.Empty<DocumentType>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetDocumentType(int docTypeId)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.DocumentTypeRepository.Get(docTypeId, session.UserInfo.OrgId);

                return new JsonResult(data, new JsonSerializerOptions());
            }
            catch (Exception)
            {
                return new JsonResult(new DocumentType(), new JsonSerializerOptions());
            }
            finally
            {
                _dbContext.Close();
            }


        }
        [HttpPost]
        public IActionResult DeleteDocumentType(int docTypeId)
        {
            try
            {

                var oldData = _unitOfWork.DocumentTypeRepository.Get(docTypeId, session.UserInfo.OrgId);
                if (oldData != null)
                {
                    _returnId = _unitOfWork.DocumentTypeRepository.Delete(docTypeId, session.UserInfo.OrgId);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Document Type Deleted Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    _vmReturn = ReturnMessage.SetInfoMessage("No Document Type Data found!!");
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
        public IActionResult SaveDocumentType(DocumentType model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.DocumentTypeRepository.Get(model.DocTypeId, model.OrgId);
                if (oldData == null)
                {
                    model.CreatedBy = session.UserInfo.UserId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.OrgId = session.UserInfo.OrgId;
                    _returnId = _unitOfWork.DocumentTypeRepository.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Document Type Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.CreatedBy = oldData.CreatedBy;
                    model.CreatedDate = oldData.CreatedDate;
                    model.UpdatedBy = session.UserInfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.DocumentTypeRepository.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Document Type Updated!!") : ReturnMessage.SetErrorMessage();
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
        #endregion DocumentType...
    }
}
