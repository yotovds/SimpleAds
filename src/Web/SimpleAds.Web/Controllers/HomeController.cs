using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data.Models;
using SimpleAds.Services.ViewModels;

namespace SimpleAds.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<SimpleAdsUser> userManager) 
            : base(userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
