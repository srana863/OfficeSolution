using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.ViewModel.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class SideNavController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public SideNavController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [HttpGet]
        public IActionResult GetSideNav()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.SubModuleSectionsRepository.GetSectionsWithScreen();
                return PartialView("_Sidenav", data);
            }
            catch (Exception)
            {
                return PartialView("_Sidenav", Enumerable.Empty<SectionViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }
        }
    }
}
