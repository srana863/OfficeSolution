using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Recruitment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class RecruitmentController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecruitmentController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JobList()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult GetAllJobList()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.RecruitmentRepo.GetAll(userinfo.OrgId);
                return PartialView("_GetAllJobList", data);
            }
            catch (Exception)
            {
                return PartialView("_GetAllJobList", Enumerable.Empty<JobList>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetJob(int id)
        {
            try
            {
                _dbContext.Open();

                var data = _unitOfWork.RecruitmentRepo.Get(id,userinfo.OrgId);

                return Ok(data);
            }
            catch (Exception)
            {
                return NotFound();
            }
            finally
            {
                _dbContext.Close();
            }
        }

        [HttpPost]
        public IActionResult SaveJobList(JobList model)
        {
            try
            {
                _dbContext.Open();
                var oldData = _unitOfWork.RecruitmentRepo.Get(model.JobId, model.OrgId);
                if (oldData == null)
                {
                    model.CreatedBy = userinfo.UserId;
                    model.CreatedDate = DateTime.UtcNow;
                    model.OrgId = userinfo.OrgId;
                    _returnId = _unitOfWork.RecruitmentRepo.Create(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Job Saved Successfully!") : ReturnMessage.SetErrorMessage();
                }
                else
                {
                    model.CreatedBy = oldData.CreatedBy;
                    model.CreatedDate = oldData.CreatedDate;
                    model.UpdatedBy = userinfo.UserId;
                    model.UpdatedDate = DateTime.UtcNow;
                    _returnId = _unitOfWork.RecruitmentRepo.Update(model);
                    _vmReturn = _returnId > 0 ? ReturnMessage.SetSuccessMessage("Job Updated!!") : ReturnMessage.SetErrorMessage();
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
        public IActionResult GetAllSOC()
        {
            try
            {
                _dbContext.Open();

                IEnumerable<String> data = _unitOfWork.RecruitmentRepo.GetSOC(userinfo.OrgId);
                
                return Ok(data);
            }
            catch (Exception)
            {
                return NotFound();
            }
            finally
            {
                _dbContext.Close();
            }
        }
        
    }
}
