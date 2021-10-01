using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class OrganizationController : BaseController
    {

        public IActionResult Profile(int subModuleId)
        {
            return View(subModuleId);
        }
    }
}
