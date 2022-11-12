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
        public IActionResult GetFacultyProfile(int facultyid)
        {
            try
            {
                _dbContext.Open();
                var data = _unitOfWork.ModulesRepository.GetModulesWithSub();
                return PartialView("_GetFacultyProfile", data);
            }
            catch (Exception)
            {
                return PartialView("_GetFacultyProfile", new ModuleViewModel());
            }
            finally
            {
                _dbContext.Close();
            }
        }

        [HttpGet]
        public IActionResult GetFacultyProfiles(string serachTxt)
        {
            try
            {
                _dbContext.Open();
                var faViewModel = new FacultyViewModel();

                List<FacultyViewModel> listViewModel = new List<FacultyViewModel>();

                var facultyExpertiseAreas = Enumerable.Empty<FacultyExpertiseAreaViewModel>();
                var facultyProfessionalInterests = Enumerable.Empty<FacultyProfessionalInterestViewModel>();

                var data = _unitOfWork.FacultyRepository.GetAllFaculty(userinfo.InstituteId);

                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        faViewModel = new FacultyViewModel();
                        faViewModel = item;

                        facultyExpertiseAreas = _unitOfWork.FacultyExpertiseAreaRepository.GetAllFacultyExpertiseArea(item.FacultyId, item.InstituteId);
                        faViewModel.FacultyExpertiseAreaViewModel = facultyExpertiseAreas;
                        facultyProfessionalInterests = _unitOfWork.FacultyProfessionalInterestRepository.GetAllFacultyProfessionalInterest(item.FacultyId, item.InstituteId);
                        faViewModel.FacultyProfessionalInterestViewModel = facultyProfessionalInterests;

                        listViewModel.Add(faViewModel);
                    }

                    
                }
                IEnumerable<FacultyViewModel> res = listViewModel;

                return PartialView("_GetFacultyProfiles", res);
            }
            catch (Exception)
            {
                return PartialView("_GetFacultyProfiles", Enumerable.Empty<FacultyViewModel>());
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
