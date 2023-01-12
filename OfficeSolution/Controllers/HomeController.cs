using Layer.Data.Implementations;
using Layer.Data.Interfaces.Common;
using Layer.Model.Common;
using Layer.Model.HRMS.Institute;
using Layer.Model.ViewModel.Institute;
using Layer.Model.ViewModel.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeSolution.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController()
        {
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login, [FromQuery] string ReturnUrl)
        {
            try
            {

                _dbContext.Open();
                var hasUser = await _unitOfWork.UserRepository.GetUserByUserName(login.UserName);
                if (hasUser != null)
                {

                    if (login.Password == DataEncryptionUtilities.GenerateDecryptedString(hasUser.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, hasUser.UserFullName),
                            new Claim("UserId",hasUser.UserId.ToString()),
                            new Claim("UserName", hasUser.UserName),
                            new Claim("InstituteId", hasUser.InstituteId.ToString()),
                            new Claim("UserFullName", hasUser.UserFullName),
                            new Claim("DesignationName", hasUser.DesignationName),
                            new Claim("DepartmentName", hasUser.DesignationName),
                            new Claim("RoleName", hasUser.RoleName),
                            new Claim("InstituteName", hasUser.InstituteName),
                            new Claim("Image", hasUser.Image),
                            new Claim("RoleId", hasUser.RoleId.ToString())

                        };

                        var claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                        });

                    }
                    return Ok(ReturnUrl);
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
            finally
            {
                _dbContext.Close();

            }
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult GetModulesWithSub()
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ModulesRepository.GetModulesWithSub();
                return PartialView("_GetModuleWithSub", data);
            }
            catch (Exception)
            {
                return PartialView("_GetModuleWithSub", Enumerable.Empty<ModuleViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeProfile(int EmployeeId)
        {
            try
            {
                _dbContext.Open();
                var faViewModel = new EmployeeViewModel();
                var EmployeeWiseProfileSectionViewModel = Enumerable.Empty<EmployeeWiseProfileSectionViewModel>();

                faViewModel = _unitOfWork.EmployeeRepository.GetEmployeeProfile(EmployeeId, userinfo.InstituteId);
                if (faViewModel != null)
                {
                    EmployeeWiseProfileSectionViewModel = _unitOfWork.EmployeeWiseProfileSectionRepository.GetEmployeeWiseProfileSectionDetails(EmployeeId, userinfo.InstituteId);
                    if (EmployeeWiseProfileSectionViewModel.Any())
                    {
                        faViewModel.EmployeeWiseProfileSectionViewModel = EmployeeWiseProfileSectionViewModel;
                    }
                }

                return PartialView("_GetEmployeeProfile", faViewModel);
            }
            catch (Exception)
            {
                return PartialView("_GetEmployeeProfile", Enumerable.Empty<EmployeeViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }

        }

        [HttpGet]
        public IActionResult GetEmployeeProfiles(string serachTxt)
        {
            try
            {
                _dbContext.Open();
                var faViewModel = new EmployeeViewModel();

                List<EmployeeViewModel> listViewModel = new List<EmployeeViewModel>();

                var EmployeeExpertiseAreas = Enumerable.Empty<EmployeeExpertiseAreaViewModel>();
                var EmployeeProfessionalInterests = Enumerable.Empty<EmployeeProfessionalInterestViewModel>();

                var data = _unitOfWork.EmployeeRepository.GetAllEmployee(userinfo.InstituteId);

                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        faViewModel = new EmployeeViewModel();
                        faViewModel = item;

                        EmployeeExpertiseAreas = _unitOfWork.EmployeeExpertiseAreaRepository.GetAllEmployeeExpertiseArea(item.EmployeeId, item.InstituteId);
                        faViewModel.EmployeeExpertiseAreaViewModel = EmployeeExpertiseAreas;
                        EmployeeProfessionalInterests = _unitOfWork.EmployeeProfessionalInterestRepository.GetAllEmployeeProfessionalInterest(item.EmployeeId, item.InstituteId);
                        faViewModel.EmployeeProfessionalInterestViewModel = EmployeeProfessionalInterests;

                        listViewModel.Add(faViewModel);
                    }


                }
                IEnumerable<EmployeeViewModel> res = listViewModel;

                return PartialView("_GetEmployeeProfiles", res);
            }
            catch (Exception)
            {
                return PartialView("_GetEmployeeProfiles", Enumerable.Empty<EmployeeViewModel>());
            }
            finally
            {
                _dbContext.Close();
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
