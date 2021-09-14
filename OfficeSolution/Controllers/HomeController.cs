using Layer.Data.Implementations.HRMS.Settings;
using Layer.Data.Interfaces.HRMS.Settings;
using Layer.Model.Common;
using Layer.Model.HRMS.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeSolution.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSolution.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUsersRepository _usersRepository;
        public HomeController()
        {
            _usersRepository = new UsersRepository(_dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users user) 
        {
            try
            {
                _dbContext.Open();
                var appSession = new AppSession();
                var hasUser = _usersRepository.GetUserByUserName(user.Username);
                if (hasUser != null)
                {
                    if (user.Password == DataEncryptionUtilities.GenerateDecryptedString(hasUser.Password))
                    {
                        appSession.UserInfo = hasUser;
                        session = appSession;
                        HttpContext.Session.SetString("appSession", JsonConvert.SerializeObject(session));

                    }
                }

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
            finally
            {
                _dbContext.Close();
                
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.SetString("appSession", null);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
