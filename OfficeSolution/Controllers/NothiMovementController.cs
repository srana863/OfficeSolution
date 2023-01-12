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
        public IActionResult Index()
        {
            return View();
        }
    }
}
