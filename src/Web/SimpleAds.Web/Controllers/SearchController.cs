using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalConstans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleAds.Data.Models;
using SimpleAds.Services.Contracs;

namespace SimpleAds.Web.Controllers
{
    [Authorize]
    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchController(UserManager<SimpleAdsUser> userManager, ISearchService searchService)
            : base(userManager)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        [Authorize(Roles = StringConstants.UserRole)]
        public IActionResult Search(string adName)
        {
            var results = this.searchService.Search(adName);

            this.ViewData["results"] = results;

            return this.View();
        }
    }
}
