using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data.Models;
using SimpleAds.Services.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAds.Web.Controllers
{
    [Authorize]
    public class AdsController : BaseController
    {
        public AdsController(UserManager<SimpleAdsUser> userManager)
            : base(userManager)
        {
        }

        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Create(CreateAdInputModel inputModel)
        {
            return this.View();
        }
    }
}
