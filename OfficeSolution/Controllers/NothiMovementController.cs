using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

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
    }
}
