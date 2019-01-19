using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAds.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<SimpleAdsUser> userManager;

        protected BaseController(UserManager<SimpleAdsUser> userManager)
        {
            this.userManager = userManager;
        }

        protected SimpleAdsUser CurrentUser
        {
            get
            {
                return this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            }
        }
    }
}
