using Layer.Data.Implementations;
using Layer.Data.Implementations.HRMS.Settings;
using Layer.Data.Interfaces.Common;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Layer.Model.ViewModel.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        [Authorize]
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
        public async Task<IActionResult> Login([FromBody] LoginViewModel login, [FromQuery]string ReturnUrl) 
        {
            try
            {

                _dbContext.Open();
                var hasUser =await _unitOfWork.UsersRepository.GetUserByUserName(login.Username);
                if (hasUser != null)
                {
                    if (login.Password == DataEncryptionUtilities.GenerateDecryptedString(hasUser.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, hasUser.UserFullName),
                            new Claim("UserId",hasUser.UserId.ToString()),
                            new Claim("Username", hasUser.Username),
                            new Claim("OrgId", hasUser.OrgId.ToString())
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

            return RedirectToAction("Login", "Home");
        }


        [HttpGet]
        public IActionResult GetModulesWithSub() {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
